using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZedGraphQuickstart
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            ScatterButton_Click(null, null);
            SaveFromConsoleApplication();
        }

        private void SaveFromConsoleApplication()
        {
            // simulate plotting from a console application
            var pane = new ZedGraph.GraphPane();
            var curve1 = pane.AddCurve(
                label: "demo",
                x: new double[] { 1, 2, 3, 4, 5 },
                y: new double[] { 1, 4, 9, 16, 25 },
                color:Color.Blue);
            curve1.Line.IsAntiAlias = true;
            pane.AxisChange();
            Bitmap bmp = pane.GetImage(400, 300, dpi: 100, isAntiAlias: true);
            bmp.Save("zedgraph-console-quickstart.png", ImageFormat.Png);
        }

        private Random rand = new Random(0);
        private double[] RandomWalk(int points, double start = 100, double mult = 50)
        {
            // return an array of difting random numbers
            double[] values = new double[points];
            values[0] = start;
            for (int i = 1; i < points; i++)
                values[i] = values[i - 1] + (rand.NextDouble() - .5) * mult;
            return values;
        }

        private double[] Consecutive(int points)
        {
            // return an array of ascending numbers starting at 1
            double[] values = new double[points];
            for (int i = 0; i < points; i++)
                values[i] = i + 1;
            return values;
        }

        private void BarButton_Click(object sender, EventArgs e)
        {
            // generate some random Y data
            int pointCount = 5;
            double[] xs = Consecutive(pointCount);
            double[] ys1 = RandomWalk(pointCount);
            double[] ys2 = RandomWalk(pointCount);

            // clear old curves
            zedGraphControl1.GraphPane.CurveList.Clear();

            // plot the data as bars
            zedGraphControl1.GraphPane.AddBar("Group A", xs, ys1, Color.Blue);
            zedGraphControl1.GraphPane.AddBar("Group B", xs, ys2, Color.Red);

            // style the plot
            zedGraphControl1.GraphPane.Title.Text = $"Bar Plot ({pointCount:n0} points)";
            zedGraphControl1.GraphPane.XAxis.Title.Text = "Horizontal Axis Label";
            zedGraphControl1.GraphPane.YAxis.Title.Text = "Vertical Axis Label";

            // auto-axis and update the display
            zedGraphControl1.GraphPane.XAxis.ResetAutoScale(zedGraphControl1.GraphPane, CreateGraphics());
            zedGraphControl1.GraphPane.YAxis.ResetAutoScale(zedGraphControl1.GraphPane, CreateGraphics());
            zedGraphControl1.Refresh();
        }

        private void ScatterButton_Click(object sender, EventArgs e)
        {
            // generate some random Y data
            int pointCount = 100;
            double[] xs1 = RandomWalk(pointCount);
            double[] ys1 = RandomWalk(pointCount);
            double[] xs2 = RandomWalk(pointCount);
            double[] ys2 = RandomWalk(pointCount);

            // clear old curves
            zedGraphControl1.GraphPane.CurveList.Clear();

            // plot the data as curves
            var curve1 = zedGraphControl1.GraphPane.AddCurve("Series A", xs1, ys1, Color.Blue);
            curve1.Line.IsAntiAlias = true;

            var curve2 = zedGraphControl1.GraphPane.AddCurve("Series B", xs2, ys2, Color.Red);
            curve2.Line.IsAntiAlias = true;

            // style the plot
            zedGraphControl1.GraphPane.Title.Text = $"Scatter Plot ({pointCount:n0} points)";
            zedGraphControl1.GraphPane.XAxis.Title.Text = "Horizontal Axis Label";
            zedGraphControl1.GraphPane.YAxis.Title.Text = "Vertical Axis Label";

            // auto-axis and update the display
            zedGraphControl1.GraphPane.XAxis.ResetAutoScale(zedGraphControl1.GraphPane, CreateGraphics());
            zedGraphControl1.GraphPane.YAxis.ResetAutoScale(zedGraphControl1.GraphPane, CreateGraphics());
            zedGraphControl1.Refresh();
        }

        private void LineButton_Click(object sender, EventArgs e)
        {
            // generate some random Y data
            int pointCount = 100_000;
            double[] xs = Consecutive(pointCount);
            double[] ys1 = RandomWalk(pointCount);
            double[] ys2 = RandomWalk(pointCount);

            // clear old curves
            zedGraphControl1.GraphPane.CurveList.Clear();

            // plot the data as curves
            var curve1 = zedGraphControl1.GraphPane.AddCurve("Series A", xs, ys1, Color.Blue);
            curve1.Line.IsAntiAlias = true;
            curve1.Symbol.IsVisible = false;

            var curve2 = zedGraphControl1.GraphPane.AddCurve("Series B", xs, ys2, Color.Red);
            curve2.Line.IsAntiAlias = true;
            curve2.Symbol.IsVisible = false;

            // style the plot
            zedGraphControl1.GraphPane.Title.Text = $"Scatter Plot ({pointCount:n0} points)";
            zedGraphControl1.GraphPane.XAxis.Title.Text = "Horizontal Axis Label";
            zedGraphControl1.GraphPane.YAxis.Title.Text = "Vertical Axis Label";

            // auto-axis and update the display
            zedGraphControl1.GraphPane.XAxis.ResetAutoScale(zedGraphControl1.GraphPane, CreateGraphics());
            zedGraphControl1.GraphPane.YAxis.ResetAutoScale(zedGraphControl1.GraphPane, CreateGraphics());
            zedGraphControl1.Refresh();
        }
    }
}
