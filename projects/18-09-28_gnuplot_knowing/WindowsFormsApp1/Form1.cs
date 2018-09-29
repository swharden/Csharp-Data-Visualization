using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AwokeKnowing.GnuplotCSharp;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// create a bunch of data points and fill them with a noisy sine wave
        /// </summary>
        double[] NoisySine(int nPoints = 1000, double cycles = 3)
        {
            Random rnd = new Random();
            double[] data = new double[nPoints];
            for (int i = 0; i < data.Length; i++)
            {
                double frac = (double)i / nPoints;
                double value = Math.Sin(frac * 2 * Math.PI * cycles);

                double noise = rnd.NextDouble() * 2 - 1;
                noise /= 10;

                data[i] = value + noise;
            }
            return data;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GnuPlot gp = new GnuPlot();
            gp.HoldOn();
            gp.Unset("key");
            gp.Plot(NoisySine(1000, 1));
            gp.Plot(NoisySine(1000, 1.5));
            gp.Plot(NoisySine(1000, 2));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            GnuPlot gp = new GnuPlot();
            gp.HoldOn();
            gp.Set("title 'Phase-Locked Signals'");
            gp.Set("samples 2000");
            gp.Unset("key");
            gp.Plot("sin(x)");
            gp.Plot("cos(x)");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //pictureBox1.Image = GnuPlot.GetBitmap();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            GnuPlot gp = new GnuPlot();
            gp.Plot("sin(x)");
        }
    }
}
