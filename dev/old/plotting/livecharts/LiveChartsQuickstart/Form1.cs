using System;
using System.Windows.Forms;

namespace LiveChartsQuickstart
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
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

            // create series and populate them with data
            var series1 = new LiveCharts.Wpf.ColumnSeries
            {
                Title = "Group A",
                Values = new LiveCharts.ChartValues<double>(ys1)
            };

            var series2 = new LiveCharts.Wpf.ColumnSeries()
            {
                Title = "Group B",
                Values = new LiveCharts.ChartValues<double>(ys2)
            };

            // display the series in the chart control
            cartesianChart1.Series.Clear();
            cartesianChart1.Series.Add(series1);
            cartesianChart1.Series.Add(series2);
            //cartesianChart1.Zoom = LiveCharts.ZoomingOptions.Xy;
        }

        private void ScatterButton_Click(object sender, EventArgs e)
        {
            // generate some random XY data
            int pointCount = 100;
            double[] xs1 = RandomWalk(pointCount);
            double[] ys1 = RandomWalk(pointCount);
            double[] xs2 = RandomWalk(pointCount);
            double[] ys2 = RandomWalk(pointCount);

            // create series and populate them with data
            var series1 = new LiveCharts.Wpf.ScatterSeries
            {
                Title = "Group A",
                Values = new LiveCharts.ChartValues<LiveCharts.Defaults.ObservablePoint>(),
                PointGeometry = LiveCharts.Wpf.DefaultGeometries.Circle
            };

            var series2 = new LiveCharts.Wpf.ScatterSeries()
            {
                Title = "Group B",
                Values = new LiveCharts.ChartValues<LiveCharts.Defaults.ObservablePoint>(),
                PointGeometry = LiveCharts.Wpf.DefaultGeometries.Circle
            };

            for (int i = 0; i < pointCount; i++)
            {
                series1.Values.Add(new LiveCharts.Defaults.ObservablePoint(xs1[i], ys1[i]));
                series2.Values.Add(new LiveCharts.Defaults.ObservablePoint(xs2[i], ys2[i]));
            }

            // display the series in the chart control
            cartesianChart1.Series.Clear();
            cartesianChart1.Series.Add(series1);
            cartesianChart1.Series.Add(series2);
        }

        private void LineButton_Click(object sender, EventArgs e)
        {
            // generate some random Y data
            int pointCount = 200;
            double[] ys1 = RandomWalk(pointCount);
            double[] ys2 = RandomWalk(pointCount);

            // create series and populate them with data
            var series1 = new LiveCharts.Wpf.LineSeries()
            {
                Title = "Group A",
                Values = new LiveCharts.ChartValues<double>(ys1),
            };

            var series2 = new LiveCharts.Wpf.LineSeries()
            {
                Title = "Group B",
                Values = new LiveCharts.ChartValues<double>(ys2),
            };

            // display the series in the chart control
            cartesianChart1.Series.Clear();
            cartesianChart1.Series.Add(series1);
            cartesianChart1.Series.Add(series2);
        }
    }
}
