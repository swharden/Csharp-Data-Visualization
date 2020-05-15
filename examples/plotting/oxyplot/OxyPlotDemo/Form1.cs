using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OxyPlotDemo
{
    public partial class Form1 : Form
    {
        double[] xs, ys;

        public Form1()
        {
            InitializeComponent();

            // RANDOM WALK
            int pointCount = 1_000_000;
            xs = new double[pointCount];
            ys = new double[pointCount];
            Random rand = new Random(0);
            for (int i=1; i<pointCount; i++)
            {
                xs[i] = i;
                ys[i] = ys[i - 1] + rand.NextDouble() - .5;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Replot();
        }

        private void plotView1_SizeChanged(object sender, EventArgs e)
        {
            Replot();
        }

        private void Replot()
        {

            var model = new OxyPlot.PlotModel();

            model.Title = "OxyPlot Demo";

            var lineSeries = new OxyPlot.Series.LineSeries();
            lineSeries.Title = $"Random Walk ({xs.Length.ToString("N0")} points)";
            
            for (int i=0; i<xs.Length; i++)
            {
                var point = new OxyPlot.DataPoint(xs[i], ys[i]);
                lineSeries.Points.Add(point);
            }
            model.Series.Add(lineSeries);

            plotView1.Model = model;
        }
    }
}

