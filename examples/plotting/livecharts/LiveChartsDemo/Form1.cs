using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LiveChartsDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            // generate data
            int pointCount = 1_000;
            Random rand = new Random(0);
            double[] randomWalkData = new double[pointCount];
            for (int i = 1; i < pointCount; i++)
                randomWalkData[i] = randomWalkData[i - 1] + rand.NextDouble() - .5;


            cartesianChart1.Series.Add(new LiveCharts.Wpf.LineSeries
            {
                Values = new LiveCharts.ChartValues<double>(randomWalkData),
                LineSmoothness = 0, // 0 for straight lines, 1 for smooth lines
                PointGeometrySize = 0,
                PointForeground = System.Windows.Media.Brushes.Gray,
            });

            cartesianChart1.AxisY.Add(new LiveCharts.Wpf.Axis
            {
                Title = "value",
                LabelFormatter = x => x.ToString()
            });

            cartesianChart1.AxisX.Add(new LiveCharts.Wpf.Axis
            {
                Title = "array index",
                LabelFormatter = x => $"index: {x}"
            });
        }
    }
}
