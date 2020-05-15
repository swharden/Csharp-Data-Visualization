using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZedGraphDemo
{
    public partial class Form1 : Form
    {
        double[] xs, ys;
        public Form1()
        {
            InitializeComponent();

            int pointCount = 100_000;
            Random rand = new Random(0);
            xs = new double[pointCount];
            ys = new double[pointCount];
            for (int i=1; i<pointCount; i++)
            {
                xs[i] = i;
                ys[i] = ys[i - 1] + rand.NextDouble() - .5;
            }
    }

        private void Form1_Load(object sender, EventArgs e)
        {
            Regraph();
        }

        private void pictureBox1_SizeChanged(object sender, EventArgs e)
        {
            Regraph();
        }

        private void Regraph()
        {

        var pane = new ZedGraph.GraphPane();

            pane.Title.Text = $"ZedGraph Demo ({ys.Length.ToString("N0")} points)";
            pane.XAxis.Title.Text = "data point (index)";
            pane.YAxis.Title.Text = "value (units)";
            pane.LineType = ZedGraph.LineType.Normal;

            var curve = pane.AddCurve("demo data", xs, ys, Color.Blue);
            curve.Line.IsAntiAlias = false;
            curve.Symbol.IsVisible = false;
            
            pane.AxisChange(); // auto-axis

            pictureBox1.Image = pane.GetImage(pictureBox1.Width, pictureBox1.Height, dpi: 100);
        }
    }
}
