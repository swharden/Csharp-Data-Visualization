using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Starfield.WinForms
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();

            bmpLive = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            bmpLast = (Bitmap)bmpLive.Clone();

            var renderThread = new Thread(new ThreadStart(RenderForever));
            renderThread.Start();
        }

        static readonly Field field = new Field(500);
        static Bitmap bmpLive;
        static Bitmap bmpLast;
        private static void RenderForever()
        {
            double maxFPS = 100;
            double minFramePeriodMsec = 1000.0 / maxFPS;

            Stopwatch stopwatch = new Stopwatch();
            while (true)
            {
                stopwatch.Restart();
                field.Advance();
                field.Render(bmpLive, starColor);
                lock (bmpLast)
                {
                    bmpLast.Dispose();
                    bmpLast = (Bitmap)bmpLive.Clone();
                }

                double msToWait = minFramePeriodMsec - stopwatch.ElapsedMilliseconds;
                if (msToWait > 0)
                    Thread.Sleep((int)msToWait);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lock (bmpLast)
            {
                pictureBox1.Image?.Dispose();
                pictureBox1.Image = (Bitmap)bmpLast.Clone();
            }
        }

        private void rb500_CheckedChanged(object sender, EventArgs e)
        {
            field.Reset(500);
        }

        private void rb100k_CheckedChanged(object sender, EventArgs e)
        {
            field.Reset(100_000);
        }

        static Color starColor = Color.White;
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            label1.Text = $"{trackBar1.Value}%";
            byte alpha = (byte)(trackBar1.Value * 255 / 100);
            starColor = Color.FromArgb(alpha, Color.White);
        }
    }
}
