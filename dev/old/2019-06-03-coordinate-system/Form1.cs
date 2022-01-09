using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrawCoordinates
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            CreateData();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InitializeBitmapAndGraphics();
            PlotData();
        }

        private Bitmap bmp;
        private Graphics gfx;

        private void InitializeBitmapAndGraphics()
        {
            bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            gfx = Graphics.FromImage(bmp);
            gfx.Clear(Color.White);
            gfx.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            Console.WriteLine($"Created new bitmap {bmp.Size}");
            pictureBox1.Image = bmp;
        }

        double[] xs;
        double[] ys;
        private void CreateData()
        {
            // create some data simply (not effeciently)
            List<double> xsList = new List<double>();
            List<double> ysList = new List<double>();
            for (double x = -3; x <= 3; x += 0.25)
            {
                xsList.Add(x);
                ysList.Add(Math.Pow(x, 3) - 5 * x);
            }
            xs = xsList.ToArray();
            ys = ysList.ToArray();
        }

        double[] axisLimits = new double[] { -3, 3, -10, 10 }; // x1, x2, y1, y2
        public Point GetPixelFromLocation(double x, double y)
        {
            double pxPerUnitX = bmp.Width / (axisLimits[1] - axisLimits[0]);
            double pxPerUnitY = bmp.Height / (axisLimits[3] - axisLimits[2]);
            int xPx = (int)((x - axisLimits[0]) * pxPerUnitX);
            int yPx = bmp.Height - (int)((y - axisLimits[2]) * pxPerUnitY);
            return new Point(xPx, yPx);
        }

        private void PlotData()
        {
            // draw lines at zero intercepts
            Point origin = GetPixelFromLocation(0, 0);
            gfx.DrawLine(Pens.LightGray, 0, origin.Y, bmp.Width, origin.Y);
            gfx.DrawLine(Pens.LightGray, origin.X, 0, origin.X, bmp.Height);

            // plot the data points as lines
            Point[] points = new Point[xs.Length];
            for (int i = 0; i < xs.Length; i++)
                points[i] = GetPixelFromLocation(xs[i], ys[i]);
            gfx.DrawLines(Pens.Blue, points);
            Console.WriteLine($"plotted lines connecting {points.Length} points");
            pictureBox1.Image = bmp;

            // draw dots at each point
            float markerSize = 4;
            foreach (Point point in points)
            {
                gfx.DrawEllipse(Pens.Red, 
                    point.X - markerSize, point.Y - markerSize, 
                    markerSize * 2, markerSize * 2);
            }
        }

        private void PictureBox1_SizeChanged(object sender, EventArgs e)
        {
            InitializeBitmapAndGraphics();
            PlotData();
        }
    }
}
