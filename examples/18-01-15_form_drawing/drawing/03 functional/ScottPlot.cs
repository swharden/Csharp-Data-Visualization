/*
 * 
 * ScottPlot - a portable C# library to create interactive graphs from X/Y data.
 *
 * https://github.com/swharden/ScottPlot
 * https://github.com/swharden/Csharp-Data-Visualization
 *
 */

using System;
using System.Collections.Generic;

using System.Drawing;
using System.Linq; // lets us take min and max of lists

namespace _03_functional
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
        public double axis_X1 = -100;
        public double axis_X2 = 100;
        public double axis_Y1 = -100;
        public double axis_Y2 = 100;

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

        // markers
        // TODO: make a marker struct
        public int[] markers_px = { 0,0,0,0 };
        public double[] markers_units = { 0, 0, 0, 0 };
        public bool[] markers_visible = { false, false, false, false };

        // actively zooming shaded areas
        public int mouse_zooming_Y1px;
        public int mouse_zooming_Y2px;
        public int mouse_zooming_X1px;
        public int mouse_zooming_X2px;

        // graphics objects
        private Bitmap bmpFrame;
        private Bitmap bmpData;
        private Graphics gfxData;

        /// <summary>
        /// Create a new ScottPlot figure.
        /// The figure size can be changed later with Resize() and data padding with Pad()
        /// </summary>
        /// <param name="figure_width">width (px) of the entire plot image</param>
        /// <param name="figure_height">width (px) of the entire plot image</param>
        public ScottPlot(int figure_width, int figure_height)
        {
            Pad(40, 0, 1, 20);
            Resize(figure_width, figure_height);
            Marker_bring_offscreen_markers(true);
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

            // recalculate marker positions
            Markers_update_px_from_units();
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

            this.bmpFrame = new Bitmap(this.figure_width, this.figure_height);

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
        
        public Bitmap GetBitmap(bool redraw_frame = true, bool draw_data = true)
        {
            if (redraw_frame) Render_frame();
            Bitmap bmp_axis_frame = this.bmpFrame;
            if (draw_data)
            {
                // add data area on top of bitmap
                Graphics gfx = Graphics.FromImage(bmp_axis_frame);
                gfx.DrawImage(this.bmpData, new Point(data_pos_left, data_pos_top));
            }
            return bmp_axis_frame;
        }

        public void Render_data(List<double> valuesY, double spacingY = 1.0 / 20000, double offsetX = 0, double offsetY = 0)
        {

            // METHOD 2: BIN DATA AND PLOT MIN/MAX ONLY (complex, faster for large datasets)
            double iPerPixel = units_per_pixel_X / spacingY;
            double nDataPixels = spacingY * valuesY.Count / units_per_pixel_X;
            double offsetPixels = -(axis_X1 - offsetX) / units_per_pixel_X;
            List<Point> points = new List<Point>();
            for (int x = 0; x < data_width; x++)
            {
                int iLeft = (int)((iPerPixel * ((x + 0) - offsetPixels)));
                int iRight = (int)((iPerPixel * ((x + 1) - offsetPixels)));
                if ((iLeft < 0) || (iRight <= 0)) continue;
                if (iRight > valuesY.Count) continue;
                if (iRight - iLeft == 0) continue;
                double colMin = valuesY.GetRange(iLeft, iRight - iLeft).Min() + offsetY;
                double colMax = valuesY.GetRange(iLeft, iRight - iLeft).Max() + offsetY;
                colMin = data_height - (colMin - axis_Y1) * pixels_per_unit_Y;
                colMax = data_height - (colMax - axis_Y1) * pixels_per_unit_Y;
                points.Add(new Point(x, (int)colMin));
                if ((int)colMin != (int)colMax) points.Add(new Point(x, (int)colMax));
            }
            if (points.Count > 1)
            {
                Pen pen = new Pen(new SolidBrush(Color.Red));
                gfxData.DrawLines(new Pen(Color.Red, 1), points.ToArray());
            }
            

        }

        public void Render_frame()
        {

            // prepare colors and fonts
            Font font_axis_labels = new Font("arial", 9, FontStyle.Regular);
            StringFormat string_format_center = new StringFormat();
            string_format_center.Alignment = StringAlignment.Center;
            StringFormat string_format_right = new StringFormat();
            string_format_right.Alignment = StringAlignment.Far;

            // prepare pens
            Pen penAxis = new Pen(color_axis_text);
            Pen penGrid = new Pen(color_grid);
            Pen penMarkers = new Pen(Color.Gray);
            penGrid.DashPattern = new float[] { 4, 4 };
            Brush brush_zoom = new SolidBrush(Color.FromArgb(100, 255, 0, 0));

            // prepare a graphics object from the bitmap
            Graphics gfx = Graphics.FromImage(this.bmpFrame);

            // fill the whole canvas with the default background color
            gfx.Clear(color_figure_background);

            // fill the background of the data area
            gfx.FillRectangle(new SolidBrush(color_data_background), data_rectangle);

            int minor_tick_size = 2;
            int major_tick_size = 5;
            int minor_tick_density = 12;
            int major_tick_density = 4;
                        
            // draw shaded areas behind an axis when zooming
            int mouze_zooming_shaded_width = 8;
            
            if (mouse_zooming_Y2px != mouse_zooming_Y1px){
                int height = Math.Abs(mouse_zooming_Y2px - mouse_zooming_Y1px);
                int pxTop = Math.Min(mouse_zooming_Y2px, mouse_zooming_Y1px);
                int pxBot = Math.Max(mouse_zooming_Y2px, mouse_zooming_Y1px);
                Rectangle rect = new Rectangle(data_pad_left - mouze_zooming_shaded_width, 
                                               pxTop, mouze_zooming_shaded_width, pxBot - pxTop);
                gfx.FillRectangle(brush_zoom, rect);
            }

            if (mouse_zooming_X1px != mouse_zooming_X2px)
            {
                System.Console.WriteLine("DRAWING H");
                int width = Math.Abs(mouse_zooming_X2px - mouse_zooming_X1px);
                int pxLeft = Math.Min(mouse_zooming_X2px, mouse_zooming_X1px);
                int pxRight = Math.Max(mouse_zooming_X2px, mouse_zooming_X1px);
                Rectangle rect = new Rectangle(pxLeft + data_pad_left, data_pos_bottom, 
                                               width, mouze_zooming_shaded_width);
                gfx.FillRectangle(brush_zoom, rect);
            }

            // horizontal axis
            foreach (double tickValX in TickGen(axis_X1, axis_X2, data_width, minor_tick_density))
            {
                // minor ticks
                int tickPx = (int)((tickValX - axis_X1) * (double)this.pixels_per_unit_X) + data_pos_left;
                gfx.DrawLine(penAxis, new Point(tickPx, data_pos_bottom), new Point(tickPx, data_pos_bottom + minor_tick_size));
            }
            foreach (double tickValX in TickGen(axis_X1, axis_X2, data_width, major_tick_density))
            {
                // major ticks
                int tickPx = (int)((tickValX - axis_X1) * (double)this.pixels_per_unit_X) + data_pos_left;
                gfx.DrawLine(penGrid, new Point(tickPx, data_pos_top), new Point(tickPx, data_pos_bottom));
                gfx.DrawLine(penAxis, new Point(tickPx, data_pos_bottom), new Point(tickPx, data_pos_bottom + major_tick_size));
                string tickLabel = TickString(tickValX, this.axis_X2 - this.axis_X1);
                gfx.DrawString(tickLabel, font_axis_labels, new SolidBrush(color_axis_text), new Point(tickPx, data_pos_bottom + 8), string_format_center);
            }

            // vertical axis
            foreach (double tickValY in TickGen(axis_Y1, axis_Y2, data_height, minor_tick_density))
            {
                int tickPx = data_pos_bottom - (int)((tickValY - axis_Y1) * (double)this.pixels_per_unit_Y);
                gfx.DrawLine(penAxis, new Point(data_pos_left - minor_tick_size, tickPx), new Point(data_pos_left, tickPx));
            }
            foreach (double tickValY in TickGen(axis_Y1, axis_Y2, data_height, major_tick_density))
            {
                int tickPx = data_pos_bottom - (int)((tickValY - axis_Y1) * (double)this.pixels_per_unit_Y);
                gfx.DrawLine(penGrid, new Point(data_pos_left, tickPx), new Point(data_pos_right, tickPx));
                gfx.DrawLine(penAxis, new Point(data_pos_left - major_tick_size - 1, tickPx), new Point(data_pos_left, tickPx));
                string tickLabel = TickString(tickValY, this.axis_Y2 - this.axis_Y1);
                gfx.DrawString(tickLabel, font_axis_labels, new SolidBrush(color_axis_text), new Point(data_pos_left - major_tick_size - 1, tickPx - 8), string_format_right);
            }

            // draw a black line around the data area
            gfx.DrawRectangle(penAxis, data_pos_left, data_pos_top, data_width, data_height);

            // draw markers if they exist
            for (int i=0; i<markers_px.Length; i++)
            {
                // only draw markers flagged as visible and if their position is in the data area
                if (this.markers_visible[i]==true && this.markers_px[i] > data_pos_left && this.markers_px[i] < data_pos_right)
                {
                    gfx.DrawLine(penMarkers, markers_px[i], 0, markers_px[i], data_pos_bottom);
                }                
            }

            // load the new frame into the data bitmap
            bmpData = new Bitmap(this.data_width, this.data_height);
            gfxData = Graphics.FromImage(bmpData);
        }

        public void Marker_bring_offscreen_markers(bool resetEveryMarker=false)
        {
            for (int i = 0; i < markers_px.Length; i++)
            {
                if (resetEveryMarker==false && markers_px[i] > data_pos_left && markers_px[i] < data_pos_right) continue;
                int spacingPx = (data_pos_right - data_pos_left) / (markers_px.Length + 1);
                markers_px[i] = (i + 1) * spacingPx + data_pos_left;
                if (resetEveryMarker==false) markers_visible[i] = true;
            }
            Markers_update_px_to_units();
        }
        public void MarkerSet(int markerNumber, int pixelLocation, bool visible)
        {
            if (markerNumber == 0) return;
            this.markers_px[markerNumber - 1] = pixelLocation;
            this.markers_visible[markerNumber - 1] = visible;
            Markers_update_px_to_units();
        }

        // return the pixel position ON THE GRAPH (not relative to the data box) given graph pixel value
        public double Position_X_units_from_px(int px) { return (px - data_pos_left) * units_per_pixel_X + axis_X1; }
        public double Position_Y_units_from_px(int px) { return (data_pos_bottom - px) * units_per_pixel_Y + axis_Y1; }

        // return the pixel position ON THE GRAPH (not relative to the data box) given an axis unit value
        public int Position_X_px_from_unit(double unit) { return (int)((unit - axis_X1) * pixels_per_unit_X); }
        public int Position_Y_px_from_unit(double unit) { return (int)((axis_Y2 - unit) * pixels_per_unit_Y); }

        public void Markers_update_px_from_units()
        {
            // call this after resizing
            for (int i = 0; i < markers_px.Length; i++)
            {
                markers_px[i] = Position_X_px_from_unit(markers_units[i]);
            }
        }

        public void Markers_update_px_to_units()
        {
            // call this after markers have been moved manually
            for (int i=0; i<markers_px.Length; i++)
            {
                markers_units[i] = Position_X_units_from_px(markers_px[i]);
            }
        }


        /// <summary>
        /// return the bitmap we last rendered
        /// </summary>
        /// <returns></returns>
        public Bitmap RenderedLast()
        {
            return this.bmpFrame;
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
 