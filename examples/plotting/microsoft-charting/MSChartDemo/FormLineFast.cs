using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace MSChartDemo
{
    public partial class FormLineFast : Form
    {
        Random rand = new Random();
        int PointCount = 10_000;

        public FormLineFast()
        {
            InitializeComponent();

            chart1.Titles.Clear();
            chart1.Titles.Add($"FastLine Plots with {PointCount} Points");

            chart1.Series.Clear();
            chart1.Series.Add("series1");
            chart1.Series.Add("series2");
            chart1.Series["series1"].ChartType = SeriesChartType.FastLine;
            chart1.Series["series2"].ChartType = SeriesChartType.FastLine;

            // https://docs.microsoft.com/en-us/previous-versions/dd489249(v=vs.140)?redirectedfrom=MSDN
            // Some charting features are omitted from the FastLine chart to improve performance. 
            // The features omitted include control of point level visual attributes, markers, data point labels, and shadows.

            button1_Click(null, null);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            chart1.Series["series1"].Points.DataBindY(DataGen.Random(PointCount, rand));
            chart1.Series["series2"].Points.DataBindY(DataGen.Random(PointCount, rand));
        }
    }
}
