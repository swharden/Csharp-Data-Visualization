using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScottPlotDemo
{
    public partial class Form1 : Form
    {
        double[] xs, ys;

        public Form1()
        {
            InitializeComponent();

            // RANDOM WALK
            int pointCount = 5_000_000;
            xs = new double[pointCount];
            ys = new double[pointCount];
            Random rand = new Random(0);
            for (int i = 1; i < pointCount; i++)
            {
                xs[i] = i;
                ys[i] = ys[i - 1] + rand.NextDouble() - .5;
            }

            formsPlot1.plt.PlotScatter(xs, ys);
            formsPlot1.Render();
        }
    }
}
