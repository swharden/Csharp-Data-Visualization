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
    public partial class FormScatter : Form
    {
        Random rand = new Random();
        int PointCount = 10_000;

        public FormScatter()
        {
            InitializeComponent();

            chart1.Titles.Clear();
            chart1.Titles.Add($"Scatter Plots with {PointCount} Points");

            chart1.Series.Clear();
            chart1.Series.Add("series1");
            chart1.Series.Add("series2");
            chart1.Series["series1"].ChartType = SeriesChartType.Point;
            chart1.Series["series2"].ChartType = SeriesChartType.Point;

            button1_Click(null, null);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            chart1.Series["series1"].Points.DataBindXY(DataGen.Random(PointCount, rand), DataGen.Random(PointCount, rand));
            chart1.Series["series2"].Points.DataBindXY(DataGen.Random(PointCount, rand), DataGen.Random(PointCount, rand));
        }
    }
}
