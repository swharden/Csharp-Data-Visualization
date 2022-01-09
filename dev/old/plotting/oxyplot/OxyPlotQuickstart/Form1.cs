using OxyPlot.WindowsForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OxyPlotQuickstart
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            ScatterButton_Click(null, null);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // simulate making a plot from a console

            // generate some random XY data
            int pointCount = 1_000;
            double[] xs1 = RandomWalk(pointCount);
            double[] ys1 = RandomWalk(pointCount);
            double[] xs2 = RandomWalk(pointCount);
            double[] ys2 = RandomWalk(pointCount);

            // create lines and fill them with data points
            var line1 = new OxyPlot.Series.LineSeries()
            {
                Title = $"Series 1",
                Color = OxyPlot.OxyColors.Blue,
                StrokeThickness = 1,
                MarkerSize = 2,
                MarkerType = OxyPlot.MarkerType.Circle
            };

            var line2 = new OxyPlot.Series.LineSeries()
            {
                Title = $"Series 2",
                Color = OxyPlot.OxyColors.Red,
                StrokeThickness = 1,
                MarkerSize = 2,
                MarkerType = OxyPlot.MarkerType.Circle
            };

            for (int i = 0; i < pointCount; i++)
            {
                line1.Points.Add(new OxyPlot.DataPoint(xs1[i], ys1[i]));
                line2.Points.Add(new OxyPlot.DataPoint(xs2[i], ys2[i]));
            }

            // create the model and add the lines to it
            var model = new OxyPlot.PlotModel
            {
                Title = $"Scatter Plot ({pointCount:N0} points each)"
            };
            model.Series.Add(line1);
            model.Series.Add(line2);

            // save the file as a PNG
            string filePath = System.IO.Path.GetFullPath("oxyplot-console-quickstart.png");
            OxyPlot.WindowsForms.PngExporter.Export(model, filePath, 400, 300, OxyPlot.OxyColors.White);
            Console.WriteLine($"Saved: {filePath}");
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

        private double[] Consecutive(int pointCount = 5)
        {
            // return an array of consecutive numbers starting at 1
            double[] values = new double[pointCount];
            for (int i = 0; i < pointCount; i++)
                values[i] = i + 1;
            return values;
        }

        private void BarButton_Click(object sender, EventArgs e)
        {
            // generate some random Y data
            int pointCount = 5;
            double[] ys1 = RandomWalk(pointCount);
            double[] ys2 = RandomWalk(pointCount);

            // create a series of bars and populate them with data
            var seriesA = new OxyPlot.Series.ColumnSeries()
            {
                Title = "Series A",
                StrokeColor = OxyPlot.OxyColors.Black,
                FillColor = OxyPlot.OxyColors.Red,
                StrokeThickness = 1
            };

            var seriesB = new OxyPlot.Series.ColumnSeries()
            {
                Title = "Series B",
                StrokeColor = OxyPlot.OxyColors.Black,
                FillColor = OxyPlot.OxyColors.Blue,
                StrokeThickness = 1
            };

            for (int i = 0; i < pointCount; i++)
            {
                seriesA.Items.Add(new OxyPlot.Series.ColumnItem(ys1[i], i));
                seriesB.Items.Add(new OxyPlot.Series.ColumnItem(ys2[i], i));
            }

            // create a model and add the bars into it
            var model = new OxyPlot.PlotModel
            {
                Title = "Bar Graph (Column Series)"
            };
            model.Axes.Add(new OxyPlot.Axes.CategoryAxis());
            model.Series.Add(seriesA);
            model.Series.Add(seriesB);

            // load the model into the user control
            plotView1.Model = model;
        }

        private void ScatterButton_Click(object sender, EventArgs e)
        {
            // generate some random XY data
            int pointCount = 1_000;
            double[] xs1 = RandomWalk(pointCount);
            double[] ys1 = RandomWalk(pointCount);
            double[] xs2 = RandomWalk(pointCount);
            double[] ys2 = RandomWalk(pointCount);

            // create lines and fill them with data points
            var line1 = new OxyPlot.Series.LineSeries()
            {
                Title = $"Series 1",
                Color = OxyPlot.OxyColors.Blue,
                StrokeThickness = 1,
                MarkerSize = 2,
                MarkerType = OxyPlot.MarkerType.Circle
            };

            var line2 = new OxyPlot.Series.LineSeries()
            {
                Title = $"Series 2",
                Color = OxyPlot.OxyColors.Red,
                StrokeThickness = 1,
                MarkerSize = 2,
                MarkerType = OxyPlot.MarkerType.Circle
            };

            for (int i = 0; i < pointCount; i++)
            {
                line1.Points.Add(new OxyPlot.DataPoint(xs1[i], ys1[i]));
                line2.Points.Add(new OxyPlot.DataPoint(xs2[i], ys2[i]));
            }

            // create the model and add the lines to it
            var model = new OxyPlot.PlotModel
            {
                Title = $"Scatter Plot ({pointCount:N0} points each)"
            };
            model.Series.Add(line1);
            model.Series.Add(line2);

            // load the model into the user control
            plotView1.Model = model;
        }

        private void LineButton_Click(object sender, EventArgs e)
        {
            // generate some random Y data
            int pointCount = 1000_000;
            double[] xs = Consecutive(pointCount);
            double[] ys1 = RandomWalk(pointCount);
            double[] ys2 = RandomWalk(pointCount);

            // create lines and fill them with data points
            var line1 = new OxyPlot.Series.LineSeries()
            {
                Title = $"Series 1",
                Color = OxyPlot.OxyColors.Blue,
                StrokeThickness = 1,
            };

            var line2 = new OxyPlot.Series.LineSeries()
            {
                Title = $"Series 2",
                Color = OxyPlot.OxyColors.Red,
                StrokeThickness = 1,
            };

            for (int i = 0; i < pointCount; i++)
            {
                line1.Points.Add(new OxyPlot.DataPoint(xs[i], ys1[i]));
                line2.Points.Add(new OxyPlot.DataPoint(xs[i], ys2[i]));
            }

            // create the model and add the lines to it
            var model = new OxyPlot.PlotModel
            {
                Title = $"Line Plot ({pointCount:N0} points each)"
            };
            model.Series.Add(line1);
            model.Series.Add(line2);

            // load the model into the user control
            plotView1.Model = model;
        }
    }
}
