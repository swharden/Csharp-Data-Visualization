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
    public partial class FormBar : Form
    {
        Random rand = new Random();
        int pointCount = 20;

        public FormBar()
        {
            InitializeComponent();

            chart1.Titles.Clear();
            chart1.Titles.Add($"Bar Chart with {pointCount} Points");

            chart1.Series.Clear();
            Series series1 = chart1.Series.Add("series1");
            Series series2 = chart1.Series.Add("series2");

            button1_Click(null, null);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            chart1.Series["series1"].Points.DataBindY(DataGen.Random(pointCount, rand));
            chart1.Series["series1"].Points.DataBindY(DataGen.Random(pointCount, rand));
        }
    }
}
