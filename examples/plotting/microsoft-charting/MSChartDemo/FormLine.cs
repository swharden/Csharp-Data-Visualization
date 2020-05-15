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
    public partial class FormLine : Form
    {
        Random rand = new Random();
        int PointCount = 10_000;

        public FormLine()
        {
            InitializeComponent();

            chart1.Titles.Clear();
            chart1.Titles.Add($"Line Plots with {PointCount} Points");

            chart1.Series.Clear();
            chart1.Series.Add("series1");
            chart1.Series.Add("series2");
            chart1.Series["series1"].ChartType = SeriesChartType.Line;
            chart1.Series["series2"].ChartType = SeriesChartType.Line;

            button1_Click(null, null);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            chart1.Series["series1"].Points.DataBindY(DataGen.Random(PointCount, rand));
            chart1.Series["series2"].Points.DataBindY(DataGen.Random(PointCount, rand));
        }
    }
}
