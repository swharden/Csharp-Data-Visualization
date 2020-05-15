using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NPlotDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            // generate data
            int pointCount = 10_000;
            Random rand = new Random(0);
            double[] randomWalkData = new double[pointCount];
            for (int i = 1; i < pointCount; i++)
                randomWalkData[i] = randomWalkData[i - 1] + rand.NextDouble() - .5;

            // create a line plot containing the data
            var linePlot = new NPlot.LinePlot
            {
                DataSource = randomWalkData,
                Color = Color.Blue
            };

            // add the line plot to the plot surface (user control)
            plotSurface2D1.Add(linePlot);
            plotSurface2D1.Title = $"NPlot Demo ({randomWalkData.Length.ToString("N0")} points)";
            plotSurface2D1.YAxis1.Label = "Sum of Random Values";
            plotSurface2D1.XAxis1.Label = "Array Index";
            plotSurface2D1.Refresh();
        }
    }
}
