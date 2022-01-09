using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace ChartingQuickstart
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            BarButton_Click(null, null);
        }

        private Random rand = new Random(0);
        private double[] RandomWalk(int pointCount = 5, double firstValue = 100, double multiplier = 50)
        {
            // return an array of difting random numbers
            double[] values = new double[pointCount];
            values[0] = firstValue;
            for (int i = 1; i < pointCount; i++)
                values[i] = values[i - 1] + (rand.NextDouble() - .5) * multiplier;
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

            // create a series for each line
            Series series1 = new Series("Group A");
            series1.Points.DataBindY(ys1);
            series1.ChartType = SeriesChartType.Column;

            Series series2 = new Series("Group B");
            series2.Points.DataBindY(ys2);
            series2.ChartType = SeriesChartType.Column;
            
            // add each series to the chart
            chart1.Series.Clear();
            chart1.Series.Add(series1);
            chart1.Series.Add(series2);

            // additional styling
            chart1.ResetAutoValues();
            chart1.Titles.Clear();
            chart1.Titles.Add($"Column Chart ({pointCount} points per series)");
            chart1.ChartAreas[0].AxisX.Title = "Horizontal Axis Label";
            chart1.ChartAreas[0].AxisY.Title = "Vertical Axis Label";
            chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.LightGray;
            chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.LightGray;
        }

        private void ScatterButton_Click(object sender, EventArgs e)
        {
            // generate some random XY data
            int pointCount = 1_000;
            double[] xs1 = RandomWalk(pointCount);
            double[] ys1 = RandomWalk(pointCount);
            double[] xs2 = RandomWalk(pointCount);
            double[] ys2 = RandomWalk(pointCount);

            // create a series for each line
            Series series1 = new Series("Group A");
            series1.Points.DataBindXY(xs1, ys1);
            series1.ChartType = SeriesChartType.Line;
            series1.MarkerStyle = MarkerStyle.Circle;

            Series series2 = new Series("Group B");
            series2.Points.DataBindXY(xs2, ys2);
            series2.ChartType = SeriesChartType.Line;
            series2.MarkerStyle = MarkerStyle.Circle;

            // add each series to the chart
            chart1.Series.Clear();
            chart1.Series.Add(series1);
            chart1.Series.Add(series2);

            // additional styling
            chart1.ResetAutoValues();
            chart1.Titles.Clear();
            chart1.Titles.Add($"Scatter Plot ({pointCount:N0} points per series)");
            chart1.ChartAreas[0].AxisX.Title = "Horizontal Axis Label";
            chart1.ChartAreas[0].AxisY.Title = "Vertical Axis Label";
            chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.LightGray;
            chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.LightGray;
        }

        private void LineButton_Click(object sender, EventArgs e)
        {
            // generate some random Y data
            int pointCount = 100_000;
            double[] ys1 = RandomWalk(pointCount);
            double[] ys2 = RandomWalk(pointCount);

            // create a series for each line
            Series series1 = new Series("Group A");
            series1.Points.DataBindY(ys1);
            series1.ChartType = SeriesChartType.FastLine;

            Series series2 = new Series("Group B");
            series2.Points.DataBindY(ys2);
            series2.ChartType = SeriesChartType.FastLine;

            // add each series to the chart
            chart1.Series.Clear();
            chart1.Series.Add(series1);
            chart1.Series.Add(series2);

            // additional styling
            chart1.ResetAutoValues();
            chart1.Titles.Clear();
            chart1.Titles.Add($"Fast Line Plot ({pointCount:N0} points per series)");
            chart1.ChartAreas[0].AxisX.Title = "Horizontal Axis Label";
            chart1.ChartAreas[0].AxisY.Title = "Vertical Axis Label";
            chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.LightGray;
            chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.LightGray;
        }
    }
}
