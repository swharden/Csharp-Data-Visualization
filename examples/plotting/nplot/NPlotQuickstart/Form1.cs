using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace NPlotQuickstart
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            ScatterButton_Click(null, null);
            SaveFromConsoleApplication();
            plotSurface2D1.BackColor = SystemColors.Control;
        }

        private void SaveFromConsoleApplication()
        {
            // simulate plotting from a console application
            var linePlot = new NPlot.PointPlot { DataSource = RandomWalk(20) };
            var surface = new NPlot.Bitmap.PlotSurface2D(400, 300);
            surface.BackColor = Color.White;
            surface.Add(linePlot);
            surface.Title = $"Scatter Plot from a Console Application";
            surface.YAxis1.Label = "Vertical Axis Label";
            surface.XAxis1.Label = "Horizontal Axis Label";
            surface.Refresh();
            surface.Bitmap.Save("nplot-console-quickstart.png");
        }

        private Random rand = new Random(0);
        private double[] RandomWalk(int points = 5, double start = 100, double mult = 50)
        {
            // return an array of difting random numbers
            double[] values = new double[points];
            values[0] = start;
            for (int i = 1; i < points; i++)
                values[i] = values[i - 1] + (rand.NextDouble() - .5) * mult;
            return values;
        }

        private void ScatterButton_Click(object sender, EventArgs e)
        {
            // generate some random Y data
            int pointCount = 20;
            double[] ys = RandomWalk(pointCount);

            // create a line plot containing the data
            var linePlot = new NPlot.PointPlot { DataSource = ys };

            // add the line plot to the plot surface (user control)
            plotSurface2D1.Clear();
            plotSurface2D1.Add(linePlot);
            plotSurface2D1.Title = $"Point Plot ({pointCount:n0} points)";
            plotSurface2D1.YAxis1.Label = "Vertical Axis Label";
            plotSurface2D1.XAxis1.Label = "Horizontal Axis Label";
            plotSurface2D1.Refresh();

            // allow the plot to be mouse-interactive
            plotSurface2D1.AddInteraction(new NPlot.Windows.PlotSurface2D.Interactions.HorizontalDrag());
            plotSurface2D1.AddInteraction(new NPlot.Windows.PlotSurface2D.Interactions.VerticalDrag());
            plotSurface2D1.AddInteraction(new NPlot.Windows.PlotSurface2D.Interactions.AxisDrag(true));
        }

        private void LineButton_Click(object sender, EventArgs e)
        {
            // generate some random Y data
            int pointCount = 10_000;
            double[] ys1 = RandomWalk(pointCount);
            double[] ys2 = RandomWalk(pointCount);
            double[] ys3 = RandomWalk(pointCount);

            // create a line plot containing the data
            var linePlot1 = new NPlot.LinePlot { DataSource = ys1, Color = Color.Red };
            var linePlot2 = new NPlot.LinePlot { DataSource = ys2, Color = Color.Green };
            var linePlot3 = new NPlot.LinePlot { DataSource = ys3, Color = Color.Blue };

            // add the line plot to the plot surface (user control)
            plotSurface2D1.Clear();
            plotSurface2D1.Add(linePlot1);
            plotSurface2D1.Add(linePlot2);
            plotSurface2D1.Add(linePlot3);
            plotSurface2D1.Title = $"Line Plot ({pointCount:n0} points each)";
            plotSurface2D1.YAxis1.Label = "Vertical Axis Label";
            plotSurface2D1.XAxis1.Label = "Horizontal Axis Label";
            plotSurface2D1.Refresh();

            // allow the plot to be mouse-interactive
            plotSurface2D1.AddInteraction(new NPlot.Windows.PlotSurface2D.Interactions.HorizontalDrag());
            plotSurface2D1.AddInteraction(new NPlot.Windows.PlotSurface2D.Interactions.VerticalDrag());
            plotSurface2D1.AddInteraction(new NPlot.Windows.PlotSurface2D.Interactions.AxisDrag(true));
        }
    }
}
