using System;
using System.Collections.Generic;

// ADD THE REFERENCE: "reference manager" > "assemblies" > "framework" > "system.drawing"
using System.Drawing; // for Bitmap and Graphics
using System.Linq;

namespace ScottPlot2
{
    public class ScottPlot
    {
        public Axis AX = new Axis();
        public int figureSizeX, figureSizeY;

        // graphics stuff (figure is the axis + graph, graph is just the data)
        private Bitmap bitmapFigure, bitmapData;

        private System.Drawing.Graphics graphicsFigure, graphicsData;

        // size info
        private int figurePadLeft = 40;

        private int figurePadRight = 10;
        private int figurePadTop = 10;
        private int figurePadBottom = 20;
        public int dataSizeX, dataSizeY;

        // performance monitoring
        public System.Diagnostics.Stopwatch stopwatch = System.Diagnostics.Stopwatch.StartNew();

        public string Info()
        {
            double ms = (double)this.stopwatch.ElapsedTicks * 1000 / System.Diagnostics.Stopwatch.Frequency;
            double hz = (double)System.Diagnostics.Stopwatch.Frequency / this.stopwatch.ElapsedTicks;
            string info = "### ScottPlot Core ###\n";
            info += String.Format("Last plot took {0:0.00} ms ({1:0.00} Hz)\n", ms, hz);
            info += String.Format("figure size: ({0}, {1})\n", figureSizeX, figureSizeY);
            info += String.Format("data size: ({0}, {1})\n", dataSizeX, dataSizeY);
            info += AX.Info();
            return info;
        }

        /// <summary>
        /// re-initialize the buffers to accomodate a specific size
        /// </summary>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        public void SetSize(int X, int Y)
        {
            //todo: force size to be at least some minimum value
            figureSizeX = X;
            figureSizeY = Y;
            dataSizeX = figureSizeX - figurePadLeft - figurePadRight;
            dataSizeY = figureSizeY - figurePadTop - figurePadBottom;
            AX.SetSize(dataSizeX, dataSizeY); // update our scale to reflect this

            // prepare the figure bitmap (axis with a blank spot for data)
            bitmapFigure = new Bitmap(figureSizeX, figureSizeY);
            graphicsFigure = Graphics.FromImage(bitmapFigure);

            // prepare the data bitmap
            bitmapData = new Bitmap(dataSizeX, dataSizeY);
            graphicsData = Graphics.FromImage(bitmapData);
            ClearData();
        }

        public void AddLine(List<double> Xs, List<double> Ys, bool drawGrid = true)
        {
            if (Ys == null)
            {
                System.Console.WriteLine("ERROR: Ys is null.");
                return;
            }
            if (!(Xs.Count == Ys.Count))
            {
                System.Console.WriteLine("ERROR: Xs and Ys must be the same length.");
                return;
            }
            if (AX.GetAxis()[0] == 0 && AX.GetAxis()[1] == 0)
            {
                AX.Auto(Xs, Ys);
            }
            if (drawGrid) DrawGrid();
            List<Point> points = new List<Point>();
            for (int i = 0; i < Xs.Count; i++)
            {
                int xPx = (int)((Xs[i] - AX.GetAxis()[0]) * AX.pxPerUnitX);
                int yPx = (int)((Ys[i] - AX.GetAxis()[2]) * AX.pxPerUnitY);
                points.Add(new Point((int)xPx, (int)yPx));
            }
            graphicsData.DrawLines(new Pen(Color.Blue, 1), points.ToArray()); //todo: is there a faster way to make an array?
        }

        /// <summary>
        /// clear the data window
        /// </summary>
        public void ClearData()
        {
            //graphicsData.Clear(SystemColors.Control);
            graphicsData.Clear(Color.White);
        }

        public void DrawGrid()
        {
            // horizontal axis / vertical lines
            List<double> ticksX = AX.TickGen(AX.GetAxis()[0], AX.GetAxis()[1], dataSizeX, dataSizeX / 50);
            for (int i = 0; i < ticksX.Count; i++)
            {
                int x = (int)((ticksX[i] - AX.GetAxis()[0]) * AX.pxPerUnitX);
                graphicsData.DrawLine(new Pen(Color.LightGray, 1), new Point(x, 0), new Point(x, dataSizeY));
            }

            // vertical axis / horizontal lines
            List<double> ticksY = AX.TickGen(AX.GetAxis()[2], AX.GetAxis()[3], dataSizeY, dataSizeY / 50);
            for (int i = 0; i < ticksY.Count; i++)
            {
                int y = (int)((ticksY[i] - AX.GetAxis()[2]) * AX.pxPerUnitY);
                graphicsData.DrawLine(new Pen(Color.LightGray, 1), new Point(0, y), new Point(dataSizeX, y));
            }
        }

        public void DrawAxis()
        {
            // prepare colors and fonts
            int fontSize = 8;
            Font tickFont = new Font(FontFamily.GenericSansSerif, fontSize, FontStyle.Regular);
            Brush tickBrush = new SolidBrush(Color.Black);
            Pen tickPen = new Pen(Color.Black);
            StringFormat formatX = new StringFormat();
            formatX.LineAlignment = StringAlignment.Near;
            formatX.Alignment = StringAlignment.Center;
            StringFormat formatY = new StringFormat();
            formatY.LineAlignment = StringAlignment.Center;
            formatY.Alignment = StringAlignment.Far;

            // clear the figure
            graphicsFigure.Clear(SystemColors.Control);
            //graphicsFigure.Clear(Color.White);

            // make a frame around the data area
            graphicsFigure.DrawRectangle(new Pen(Color.Black), new Rectangle(figurePadLeft - 1, figurePadTop - 1, dataSizeX + 1, dataSizeY + 1));

            // add X ticks and labels
            List<double> ticksX = AX.TickGen(AX.GetAxis()[0], AX.GetAxis()[1], dataSizeX, dataSizeX / 100);
            for (int i = 0; i < ticksX.Count; i++)
            {
                int x = (int)((ticksX[i] - AX.GetAxis()[0]) * AX.pxPerUnitX);
                string s = AX.TickString(ticksX[i], AX.GetAxis()[1] - AX.GetAxis()[0]);
                graphicsFigure.DrawLine(new Pen(Color.Black), new Point(x + figurePadLeft, figurePadTop + dataSizeY), new Point(x + figurePadLeft, figurePadTop + dataSizeY + 5));
                graphicsFigure.DrawString(s, tickFont, tickBrush, new Point(x + figurePadLeft, figurePadTop + dataSizeY + 5 + 2), formatX);
            }

            // add Y ticks and labels
            List<double> ticksY = AX.TickGen(AX.GetAxis()[2], AX.GetAxis()[3], dataSizeY, dataSizeY / 50);
            for (int i = 0; i < ticksY.Count; i++)
            {
                int y = (int)((ticksY[i] - AX.GetAxis()[2]) * AX.pxPerUnitY);
                string s = AX.TickString(ticksY[i], AX.GetAxis()[3] - AX.GetAxis()[2]);
                graphicsFigure.DrawLine(new Pen(Color.Black), new Point(figurePadLeft - 5, y + figurePadTop), new Point(figurePadLeft, y + figurePadTop));
                graphicsFigure.DrawString(s, tickFont, tickBrush, new Point(figurePadLeft - 5, y + figurePadTop), formatY);
            }
        }

        /// <summary>
        /// redraw the axis and return a bitmap with axis+data
        /// </summary>
        /// <returns></returns>
        public Bitmap Render()
        {
            DrawAxis();
            graphicsFigure.DrawImage(bitmapData, figurePadLeft, figurePadTop);
            return bitmapFigure;
        }

        public void SaveFig(string filename)
        {
            bitmapFigure.Save(filename);
        }
    }

    public class Generate
    {
        private System.Random rand = new System.Random();

        /// <summary>
        /// generate a list of sine wave numbers offset by the system time
        /// </summary>
        /// <param name="nPoints"></param>
        /// <param name="scale"></param>
        /// <param name="offset"></param>
        /// <param name="period"></param>
        /// <returns></returns>
        public List<double> Sine(int nPoints, double scale = 1, double offset = 0, double period = 30)
        {
            List<double> list = new List<double>();
            period = nPoints / period;
            double timeOffset = DateTime.Now.Ticks / System.TimeSpan.TicksPerMillisecond / period / 20;
            for (int i = 0; i < nPoints; i++)
            {
                list.Add((double)((scale * System.Math.Sin((i + timeOffset) / period)) + offset));
            }
            return list;
        }

        /// <summary>
        /// generate a list of random numbers
        /// </summary>
        /// <param name="nPoints"></param>
        /// <param name="scale"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public List<double> Random(int nPoints, double scale = 1, double offset = 0)
        {
            List<double> list = new List<double>();
            for (int i = 0; i < nPoints; i++)
            {
                list.Add((double)((scale * (double)rand.NextDouble()) + offset));
            }
            return list;
        }

        /// <summary>
        /// generate a list of consecutive numbers
        /// </summary>
        /// <param name="nPoints"></param>
        /// <param name="scale"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public List<double> Sequence(int nPoints, double scale = 1, double offset = 0)
        {
            List<double> list = new List<double>();
            for (int i = 0; i < nPoints; i++)
            {
                list.Add((scale * i) + offset);
            }
            return list;
        }
    }

    public class Axis
    {
        // bitmap dimensions
        private int sizeX = 0, sizeY = 0;

        // axis and view limits
        private double AX1 = 0, AX2 = 0, AY1 = 0, AY2 = 0;

        private double VX1 = 0, VX2 = 0, VY1 = 0, VY2 = 0;

        // audomatically-calculated pixel/unit scaling
        public double pxPerUnitX = 0, pxPerUnitY = 0;

        public double UnitsPerPxX = 0, UnitsPerPxY = 0;

        public string Info()
        {
            string info = "### ScottPlot Axis ###\n";
            info += string.Format("size (x,y) = {0}, {1}\n", sizeX, sizeY);
            info += string.Format("axis (X1, X2, Y1, Y2) = {0:0.000}, {1:0.000}, {2:0.000}, {3:0.000}\n", AX1, AX2, AY1, AY2);
            info += string.Format("view (X1, X2, Y1, Y2) = {0:0.000}, {1:0.000}, {2:0.000}, {3:0.000}\n", VX1, VX2, VY1, VY2);
            info += string.Format("px per unit (x, y) = {0:0.000}, {1:0.000}\n", pxPerUnitX, pxPerUnitY);
            info += string.Format("units per px (x, y) = {0:0.000}, {1:0.000}\n", UnitsPerPxX, UnitsPerPxY);

            return info;
        }

        /// <summary>
        /// automatically adjust axis to fit the data
        /// </summary>
        /// <param name="Xs"></param>
        /// <param name="Ys"></param>
        /// <param name="scaleX"></param>
        /// <param name="scaleY"></param>
        public void Auto(List<double> Xs, List<double> Ys, double scaleX = 1, double scaleY = 1)
        {
            if (Xs == null || Ys == null)
            {
                Console.WriteLine("ERROR: Xs and Ys cannot be null");
                return;
            }
            SetAxis(Xs.Min(), Xs.Max(), Ys.Min(), Ys.Max());
            //todo: zoom
        }

        /// <summary>
        /// set the size of the graph area (in pixels)
        /// </summary>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        public void SetSize(int X, int Y)
        {
            sizeX = Math.Max(1, X);
            sizeY = Math.Max(1, Y);
            CalculatePixelConversions();
        }

        /// <summary>
        /// return the size (in pixels) [x, y]
        /// </summary>
        /// <returns></returns>
        public int[] GetSize()
        {
            int[] size = new int[2];
            size[0] = sizeX;
            size[1] = sizeY;
            return size;
        }

        /// <summary>
        /// set axis (and view) with error checking
        /// </summary>
        /// <param name="X1"></param>
        /// <param name="X2"></param>
        /// <param name="Y1"></param>
        /// <param name="Y2"></param>
        public void SetAxis(double? X1, double? X2, double? Y1, double? Y2)
        {
            // disallow things that are too zoomed in
            if ((X2 - X1) < .001) return;
            if ((Y2 - Y1) < .001) return;

            // replcae null values with current axis values
            if (!X1.HasValue) X1 = AX1;
            if (!X2.HasValue) X2 = AX2;
            if (!Y1.HasValue) Y1 = AY1;
            if (!Y2.HasValue) Y2 = AY2;

            // ensure axis limits aren't flipped or zero
            if (X1 < X2)
            {
                AX1 = (double)X1;
                AX2 = (double)X2;
            }
            if (Y1 < Y2)
            {
                AY1 = (double)Y1;
                AY2 = (double)Y2;
            }

            // apply limits to axis
            VX1 = AX1;
            VX2 = AX2;
            VY1 = AY1;
            VY2 = AY2;

            // recalculate axis/pixel scaling
            CalculatePixelConversions();
        }

        public void CalculatePixelConversions()
        {
            // recalculate axis/pixel scaling
            pxPerUnitX = (double)sizeX / (VX2 - VX1);
            pxPerUnitY = (double)sizeY / (VY2 - VY1);
            UnitsPerPxX = (VX2 - VX1) / (double)sizeX;
            UnitsPerPxY = (VY2 - VY1) / (double)sizeY;
        }

        /// <summary>
        /// return the current axis [X1, X2, Y1, Y2]
        /// </summary>
        /// <returns></returns>
        public double[] GetAxis()
        {
            double[] axis = new double[4];
            axis[0] = AX1;
            axis[1] = AX2;
            axis[2] = AY1;
            axis[3] = AY2;
            return axis;
        }

        /// <summary>
        /// return the current view [X1, X2, Y1, Y2]
        /// </summary>
        /// <returns></returns>
        public double[] GetView()
        {
            double[] view = new double[4];
            view[0] = VX1;
            view[1] = VX2;
            view[2] = VY1;
            view[3] = VY2;
            return view;
        }

        // can be used for Xs or Ys
        public List<double> TickGen(double X1, double X2, int widthPx, int nTicks = 10)
        {
            List<double> values = new List<double>();
            List<int> pixels = new List<int>();
            List<string> labels = new List<string>();
            double dataSpan = X2 - X1;
            double unitsPerPx = dataSpan / widthPx;
            double PxPerUnit = widthPx / dataSpan;
            double tickSize = RoundNumberNear((dataSpan / nTicks) * 1.5);

            int lastTick = 123456789;
            for (int i = 0; i < widthPx; i++)
            {
                double thisPosition = i * unitsPerPx + X1;
                int thisTick = (int)(thisPosition / tickSize);
                if (thisTick != lastTick)
                {
                    lastTick = thisTick;
                    double thisPositionRounded = (double)((int)(thisPosition / tickSize) * tickSize);
                    if (thisPositionRounded > X1 && thisPositionRounded < X2)
                    {
                        values.Add(thisPositionRounded);
                        pixels.Add(i);
                        labels.Add(string.Format("{0}", thisPosition));
                    }
                }
            }
            return values;
        }

        /// <summary>
        /// given a tick value (and the distance between the first two ticks) return it in a way that can be easily displayed as text
        /// </summary>
        /// <param name="val"></param>
        /// <param name="valDistance"></param>
        /// <returns></returns>
        public string TickString(double val, double valDistance)
        {
            if (valDistance < .01) return string.Format("{0:0.0000}", val);
            if (valDistance < .1) return string.Format("{0:0.000}", val);
            if (valDistance < 1) return string.Format("{0:0.00}", val);
            if (valDistance < 10) return string.Format("{0:0.0}", val);
            return string.Format("{0:0}", val);
        }

        /// <summary>
        /// Find a whole number or whole decimal which is within one-fold of the target.
        /// Numbers returned will always start with 1 or 5.
        /// (i.e., 1000, 500, 100, 50, 10, 5, 1, .5, .1, .05, .01)
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
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
    }
}