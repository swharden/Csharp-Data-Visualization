using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScottPlotQuickstart
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Random rand = new Random(0);

        private void BarButton_Click(object sender, EventArgs e)
        {
            // generate some random Y data
            int pointCount = 5;
            double[] ys1 = ScottPlot.DataGen.RandomWalk(rand, pointCount, mult: 50, offset: 100);
            double[] ys2 = ScottPlot.DataGen.RandomWalk(rand, pointCount, mult: 50, offset: 100);

            // collect the data into groups
            string[] groupLabels = { "One", "Two", "Three", "Four", "Five" };
            string[] seriesLabels = { "Group A", "Group B" };
            double[][] barData = { ys1, ys2 };

            // plot the data
            formsPlot1.Reset();
            formsPlot1.plt.PlotBarGroups(groupLabels, seriesLabels, barData);

            // additional styling
            formsPlot1.plt.Title("Bar Graph");
            formsPlot1.plt.XLabel("Horizontal Axis Label");
            formsPlot1.plt.YLabel("Vertical Axis Label");
            formsPlot1.plt.Legend(location: ScottPlot.legendLocation.upperLeft);
            formsPlot1.plt.Axis(y1: 0);
            formsPlot1.Render();
        }

        private void ScatterButton_Click(object sender, EventArgs e)
        {
            // generate some random X/Y data
            int pointCount = 500;
            double[] xs1 = ScottPlot.DataGen.RandomWalk(rand, pointCount);
            double[] ys1 = ScottPlot.DataGen.RandomWalk(rand, pointCount);
            double[] xs2 = ScottPlot.DataGen.RandomWalk(rand, pointCount);
            double[] ys2 = ScottPlot.DataGen.RandomWalk(rand, pointCount);

            // plot the data
            formsPlot1.Reset();
            formsPlot1.plt.PlotScatter(xs1, ys1);
            formsPlot1.plt.PlotScatter(xs2, ys2);

            // additional styling
            formsPlot1.plt.Title($"Scatter Plot ({pointCount} points per group)");
            formsPlot1.plt.XLabel("Horizontal Axis Label");
            formsPlot1.plt.YLabel("Vertical Axis Label");
            formsPlot1.Render();
        }

        private void LineButton_Click(object sender, EventArgs e)
        {
            // generate some random Y data
            int pointCount = 10_000;
            double[] ys1 = ScottPlot.DataGen.RandomWalk(rand, pointCount);
            double[] ys2 = ScottPlot.DataGen.RandomWalk(rand, pointCount);

            // plot the data
            formsPlot1.Reset();
            formsPlot1.plt.PlotSignal(ys1);
            formsPlot1.plt.PlotSignal(ys2);

            // additional styling
            formsPlot1.plt.Title($"Line Plot ({10_000:N0} points each)");
            formsPlot1.plt.XLabel("Horizontal Axis Label");
            formsPlot1.plt.YLabel("Vertical Axis Label");
            formsPlot1.Render();
        }

        private void SimpleBar_Click(object sender, EventArgs e)
        {
            // generate some random Y data
            int pointCount = 5;
            double[] xs = ScottPlot.DataGen.Consecutive(pointCount);
            double[] ys = ScottPlot.DataGen.RandomWalk(rand, pointCount, mult: 50, offset: 100);

            // plot the data
            formsPlot1.Reset();
            formsPlot1.plt.PlotBar(xs, ys);

            // additional styling
            formsPlot1.plt.Title("Simple Bar Graph");
            formsPlot1.plt.XLabel("Horizontal Axis Label");
            formsPlot1.plt.YLabel("Vertical Axis Label");
            formsPlot1.plt.Legend(location: ScottPlot.legendLocation.upperLeft);
            formsPlot1.plt.Axis(y1: 0);
            formsPlot1.Render();
        }
    }
}
