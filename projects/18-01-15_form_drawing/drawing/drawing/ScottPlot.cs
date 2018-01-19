/*
 * ScottPlot - a portable C# library to create interactive graphs from X/Y data.
 *
 * https://github.com/swharden/ScottPlot
 * https://github.com/swharden/Csharp-Data-Visualization
 *
 */

using System;
using System.Collections.Generic;

using System.Drawing;

namespace drawing
{
    /// <summary>
    /// ScottPlot graphs X/Y data and generates figures as bitmaps.
    /// </summary>
    internal class ScottPlot
    {
        // figure dimensions
        private int figure_width;

        private int figure_height;

        // padding, position, and size of data area
        private int data_pad_left;

        private int data_pad_top;
        private int data_pad_right;
        private int data_pad_bottom;

        public int data_pos_left;
        public int data_pos_right;
        public int data_pos_top;
        public int data_pos_bottom;

        private int data_width;
        private int data_height;

        private Rectangle data_rectangle;

        // figure and data colors
        public Color color_figure_background = SystemColors.Control;

        public Color color_axis_text = Color.Black;
        public Color color_data_background = Color.White;
        public Color color_data_highlight = Color.DarkGray;
        public Color color_data = Color.Red;
        public Color color_grid = Color.LightGray;

        // axis edges (the initial view of data)
        private double axis_X1 = -100;

        private double axis_X2 = 100;
        private double axis_Y1 = -100;
        private double axis_Y2 = 100;

        // data limits (how far you can scroll around)
        public bool axis_constrain = true;

        public double axis_limit_X1 = -1000;
        public double axis_limit_X2 = 1000;
        public double axis_limit_Y1 = -1000;
        public double axis_limit_Y2 = 1000;
        public double axis_center_X;
        public double axis_center_Y;
        public double axis_visible_frac_X;
        public double axis_visible_frac_Y;
        public double axis_position_frac_X;
        public double axis_position_frac_Y;

        // axis scale
        private double pixels_per_unit_X;

        private double pixels_per_unit_Y;
        private double units_per_pixel_X;
        private double units_per_pixel_Y;

        // graphics objects
        private Bitmap bitmap;

        private System.Drawing.Graphics gfx;

        /// <summary>
        /// Create a new ScottPlot figure.
        /// The figure size can be changed later with Resize() and data padding with Pad()
        /// </summary>
        /// <param name="figure_width">width (px) of the entire plot image</param>
        /// <param name="figure_height">width (px) of the entire plot image</param>
        public ScottPlot(int figure_width, int figure_height)
        {
            Pad(100, 50, 50, 100); // default padding is fixed
            Resize(figure_width, figure_height);
        }

        /// <summary>
        /// Modify padding on all sides of the data area
        /// </summary>
        /// <param name="data_pad_left">padding (px) from the edge of the plot to the data area</param>
        /// <param name="data_pad_top">padding (px) from the edge of the plot to the data area</param>
        /// <param name="data_pad_right">padding (px) from the edge of the plot to the data area</param>
        /// <param name="data_pad_bottom">padding (px) from the edge of the plot to the data area</param>
        public void Pad(int data_pad_left, int data_pad_top, int data_pad_right, int data_pad_bottom)
        {
            this.data_pad_left = data_pad_left;
            this.data_pad_top = data_pad_top;
            this.data_pad_right = data_pad_right;
            this.data_pad_bottom = data_pad_bottom;
        }

        /// <summary>
        /// Center the data view on a specific (X, Y) point in space.
        /// This does not change scale/zoom.
        /// </summary>
        /// <param name="position_units_x">location in graph units</param>
        /// <param name="position_units_y">location in graph units</param>
        public void PanTo(double position_units_x, double position_units_y)
        {
            double axis_span_x = (axis_X2 - axis_X1);
            double axis_span_y = (axis_Y2 - axis_Y1);

            axis_X1 = position_units_x - axis_span_x / 2;
            axis_X2 = position_units_x + axis_span_x / 2;
            axis_Y1 = position_units_y - axis_span_y / 2;
            axis_Y2 = position_units_y + axis_span_y / 2;
            Zoom();
        }

        public void PanToFrac(double frac_x, double frac_y)
        {
            double view_width = axis_X2 - axis_X1;
            double view_height = axis_Y2 - axis_Y1;
            double limit_width = axis_limit_X2 - axis_limit_X1;
            double limit_height = axis_limit_Y2 - axis_limit_Y1;

            System.Console.WriteLine(view_width);

            axis_X1 = frac_x * limit_width + axis_limit_X1 - view_width / 2;
            axis_X2 = frac_x * limit_width + axis_limit_X1 + view_width / 2;
            axis_Y1 = frac_y * limit_height + axis_limit_Y1 - view_height / 2;
            axis_Y2 = frac_y * limit_height + axis_limit_Y1 + view_height / 2;
            Zoom();
        }

        public void AxisSet(double X1, double X2, double Y1, double Y2)
        {
            axis_X1 = X1;
            axis_X2 = X2;
            axis_Y1 = Y1;
            axis_Y2 = Y2;
            Zoom();
        }

        /// <summary>
        /// Shift the center of the data view by a specific (X, Y) offset
        /// </summary>
        /// <param name="panX">shift in graph units</param>
        /// <param name="panY">shift in graph units</param>
        public void Pan(double panX = 0, double panY = 0)
        {
            axis_X1 += panX;
            axis_X2 += panX;
            axis_Y1 += panY;
            axis_Y2 += panY;
            Zoom();
        }

        /// <summary>
        /// Zoom in (scale greater than 1) or zoom out (scale less than 1).
        /// Call this after panning, even though scale is (1,1).
        /// </summary>
        /// <param name="scale"></param>
        public void Zoom(double scaleX = 1.0, double scaleY = 1.0)
        {
            // recalculate the center point (which will not change by zoom)
            axis_center_X = (axis_X2 + axis_X1) / 2;
            axis_center_Y = (axis_Y2 + axis_Y1) / 2;

            // zoom only if necessary to reduce floating point errors
            if (!(scaleX == 1.0))
            {
                double axis_X_pad = (axis_X2 - axis_X1) / 2;
                axis_X1 = axis_center_X - axis_X_pad * scaleX;
                axis_X2 = axis_center_X + axis_X_pad * scaleX;
            }

            if (!(scaleY == 1.0))
            {
                double axis_Y_pad = (axis_Y2 - axis_Y1) / 2;
                axis_Y1 = axis_center_Y - axis_Y_pad * scaleY;
                axis_Y2 = axis_center_Y + axis_Y_pad * scaleY;
            }

            // limit the axis values constraining them to the field area
            if (axis_constrain)
            {
                double xShift = 0;
                double yShift = 0;

                if (axis_X1 < axis_limit_X1) xShift = +Math.Abs(axis_limit_X1 - axis_X1);
                if (axis_X2 > axis_limit_X2) xShift = -Math.Abs(axis_limit_X2 - axis_X2);
                if (axis_Y1 < axis_limit_Y1) yShift = +Math.Abs(axis_limit_Y1 - axis_Y1);
                if (axis_Y2 > axis_limit_Y2) yShift = -Math.Abs(axis_limit_Y2 - axis_Y2);

                axis_X1 += xShift;
                axis_X2 += xShift;
                axis_Y1 += yShift;
                axis_Y2 += yShift;

                axis_X1 = Math.Max(axis_X1, axis_limit_X1);
                axis_X2 = Math.Min(axis_X2, axis_limit_X2);
                axis_Y1 = Math.Max(axis_Y1, axis_limit_Y1);
                axis_Y2 = Math.Min(axis_Y2, axis_limit_Y2);
            }

            // recalculate visible fractions (useful for scrollbar widths)
            axis_visible_frac_X = (axis_X2 - axis_X1) / (axis_limit_X2 - axis_limit_X1);
            axis_visible_frac_Y = (axis_Y2 - axis_Y1) / (axis_limit_Y2 - axis_limit_Y1);

            // calculate the position (distance from left to right)
            axis_position_frac_X = (axis_center_X - axis_limit_X1) / (axis_limit_X2 - axis_limit_X1);
            axis_position_frac_Y = (axis_center_Y - axis_limit_Y1) / (axis_limit_Y2 - axis_limit_Y1);

            // recalculate pixel grid
            pixels_per_unit_X = data_width / (axis_X2 - axis_X1);
            units_per_pixel_X = (axis_X2 - axis_X1) / data_width;
            pixels_per_unit_Y = data_height / (axis_Y2 - axis_Y1);
            units_per_pixel_Y = (axis_Y2 - axis_Y1) / data_height;
        }

        /// <summary>
        /// Resize the ScottPlot figure
        /// </summary>
        /// <param name="figure_width">width (px) of the entire plot image</param>
        /// <param name="figure_height">width (px) of the entire plot image</param>
        public void Resize(int figure_width, int figure_height)
        {
            this.figure_width = figure_width;
            this.figure_height = figure_height;

            this.data_pos_left = data_pad_left;
            this.data_pos_top = data_pad_top;
            this.data_pos_right = figure_width - data_pad_right;
            this.data_pos_bottom = figure_height - data_pad_bottom;
            this.data_height = figure_height - data_pad_top - data_pad_bottom;
            this.data_width = figure_width - data_pad_left - data_pad_right;
            this.data_rectangle = new Rectangle(data_pos_left, data_pos_top, data_width, data_height);

            this.bitmap = new Bitmap(this.figure_width, this.figure_height);
            this.gfx = Graphics.FromImage(this.bitmap);

            Zoom();
        }

        /// <summary>
        /// Display information about the figure and its data.
        /// </summary>
        public void ShowInfo()
        {
            string msg = "";
            msg += string.Format("\nDIMENSIONS:");
            msg += string.Format("\nFigure shape: ({0}, {1})", figure_width, figure_height);
            msg += string.Format("\nData padding: L={0}, T={1}, R={2}, B={3})", data_pad_left, data_pad_top, data_pad_right, data_pad_bottom);
            msg += string.Format("\nData position: [({0},{1}), ({2},{3})]", data_pos_left, data_pos_top, data_pos_right, data_pos_bottom);

            msg += string.Format("\nSCROLL BARS:");
            msg += string.Format("\nHorizontal Fraction: {0}", axis_visible_frac_X);
            msg += string.Format("\nVertical Fraction: {0}", axis_visible_frac_Y);
            msg += string.Format("\nHorizontal Position: {0}", axis_position_frac_X);
            msg += string.Format("\nVertical Position: {0}", axis_position_frac_Y);

            System.Console.WriteLine(msg);
        }

        /// <summary>
        /// Draw the figure frame with axis labels, ticks, etc.
        /// Style it similar to how ClampFit displays ABF files.
        /// </summary>
        public Bitmap Render()
        {
            // prepare colors and fonts
            Font font_axis_labels = new Font("arial", 9, FontStyle.Regular);
            Font font_title = new Font("arial", 12, FontStyle.Bold);
            StringFormat string_format_center = new StringFormat();
            string_format_center.Alignment = StringAlignment.Center;
            StringFormat string_format_right = new StringFormat();
            string_format_right.Alignment = StringAlignment.Far;

            // prepare pens
            Pen penAxis = new Pen(color_axis_text);
            Pen penGrid = new Pen(color_grid);
            penGrid.DashPattern = new float[] { 4, 4 };

            // fill the whole canvas with the default background color
            gfx.Clear(color_figure_background);

            // fill the background of the data area
            gfx.FillRectangle(new SolidBrush(color_data_background), data_rectangle);

            // draw a highlight color on the far left
            gfx.FillRectangle(new SolidBrush(color_data_highlight), new Rectangle(0, 0, 30, figure_height - data_pad_bottom));

            // vertical axis label (complicated becasue it's rotated)
            string axis_label_y = "Analog Input 0\n(pA)";
            SizeF axis_label_y_size = gfx.MeasureString(axis_label_y, font_axis_labels);
            gfx.RotateTransform(-90);
            gfx.DrawString(axis_label_y, font_axis_labels, new SolidBrush(Color.Black), new Point(-(figure_height - data_pad_bottom) / 2, 1), string_format_center);
            gfx.ResetTransform();

            // title
            gfx.DrawString("ScottPlot Does Amazing Things", font_title, new SolidBrush(color_axis_text), new Point(figure_width / 2, data_pad_top / 2 - 8), string_format_center);

            // horizontal axis label
            gfx.DrawString("Time (ms)", font_axis_labels, new SolidBrush(color_axis_text), new Point(figure_width / 2, figure_height - data_pad_bottom / 2), string_format_center);

            // horizontal axis
            foreach (double tickValX in TickGen(axis_X1, axis_X2, data_width))
            {
                int tickPx = (int)((tickValX - axis_X1) * (double)this.pixels_per_unit_X) + data_pos_left;
                gfx.DrawLine(penGrid, new Point(tickPx, data_pos_top), new Point(tickPx, data_pos_bottom));
                gfx.DrawLine(penAxis, new Point(tickPx, data_pos_bottom), new Point(tickPx, data_pos_bottom + 3));
                string tickLabel = TickString(tickValX, this.axis_X2 - this.axis_X1);
                gfx.DrawString(tickLabel, font_axis_labels, new SolidBrush(color_axis_text), new Point(tickPx, data_pos_bottom + 8), string_format_center);
            }

            // vertical axis
            foreach (double tickValY in TickGen(axis_Y1, axis_Y2, data_height))
            {
                int tickPx = data_pos_bottom - (int)((tickValY - axis_Y1) * (double)this.pixels_per_unit_Y);
                gfx.DrawLine(penGrid, new Point(data_pos_left, tickPx), new Point(data_pos_right, tickPx));
                gfx.DrawLine(penAxis, new Point(data_pos_left - 3, tickPx), new Point(data_pos_left, tickPx));
                string tickLabel = TickString(tickValY, this.axis_Y2 - this.axis_Y1);
                gfx.DrawString(tickLabel, font_axis_labels, new SolidBrush(color_axis_text), new Point(data_pos_left - 3, tickPx - 8), string_format_right);
            }

            // draw a black line around the data area
            gfx.DrawRectangle(penAxis, data_pos_left, data_pos_top, data_width, data_height);

            return this.bitmap;
        }

        /// <summary>
        /// return the bitmap we last rendered
        /// </summary>
        /// <returns></returns>
        public Bitmap RenderedLast()
        {
            return this.bitmap;
        }

        /// <summary>
        /// format a number for a tick label by limiting its precision. axisSpan is X2-X1.
        /// </summary>
        public string TickString(double value, double axisSpan)
        {
            if (axisSpan < .01) return string.Format("{0:0.0000}", value);
            if (axisSpan < .1) return string.Format("{0:0.000}", value);
            if (axisSpan < 1) return string.Format("{0:0.00}", value);
            if (axisSpan < 10) return string.Format("{0:0.0}", value);
            return string.Format("{0:0}", value);
        }

        /// <summary>
        /// given an arbitrary number, return the nearerest round number
        /// (i.e., 1000, 500, 100, 50, 10, 5, 1, .5, .1, .05, .01)
        /// </summary>
        private double RoundNumberNear(double target)
        {
            target = Math.Abs(target);
            int lastDivision = 2;
            double round = 1000000000000;
            while (round > 0.00000000001)
            {
                if (round <= target) return round;
                round /= lastDivision;
                if (lastDivision == 2) lastDivision = 5;
                else lastDivision = 2;
            }
            return 0;
        }

        /// <summary>
        /// return an array of good tick values for an axis given a range
        /// </summary>
        public double[] TickGen(double axisValueLower, double axisValueUpper, int graphWidthPx, int nTicks = 4)
        {
            List<double> values = new List<double>();
            List<int> pixels = new List<int>();
            List<string> labels = new List<string>();
            double dataSpan = axisValueUpper - axisValueLower;
            double unitsPerPx = dataSpan / graphWidthPx;
            double PxPerUnit = graphWidthPx / dataSpan;
            double tickSize = RoundNumberNear((dataSpan / nTicks) * 1.5);

            int lastTick = 123456789;
            for (int i = 0; i < graphWidthPx; i++)
            {
                double thisPosition = i * unitsPerPx + axisValueLower;
                int thisTick = (int)(thisPosition / tickSize);
                if (thisTick != lastTick)
                {
                    lastTick = thisTick;
                    double thisPositionRounded = (double)((int)(thisPosition / tickSize) * tickSize);
                    if (thisPositionRounded > axisValueLower && thisPositionRounded < axisValueUpper)
                    {
                        values.Add(thisPositionRounded);
                        pixels.Add(i);
                        labels.Add(string.Format("{0}", thisPosition));
                    }
                }
            }
            return values.ToArray();
        }
    }
}