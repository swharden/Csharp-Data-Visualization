using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SystemDrawing
{
    public partial class Form1 : Form
    {
        readonly Bitmap bmp = new Bitmap(600, 400, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
        public Form1()
        {
            InitializeComponent();
        }

        Random rand = new Random(0);
        List<double> renderTimesMsec = new List<double>();
        private void Render(int lineCount)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            using (Pen pen = new Pen(Color.White))
            using (Graphics gfx = Graphics.FromImage(bmp))
            {
                gfx.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                gfx.Clear(ColorTranslator.FromHtml("#003366"));
                for (int i = 0; i < lineCount; i++)
                {
                    pen.Color = Color.FromArgb(alpha: rand.Next(255),
                        red: rand.Next(255), green: rand.Next(255), blue: rand.Next(255));
                    pen.Width = rand.Next(1, 10);
                    gfx.DrawLine(pen,
                        rand.Next(bmp.Width), rand.Next(bmp.Height),
                        rand.Next(bmp.Width), rand.Next(bmp.Height));
                }
            }
            pictureBox1.Image?.Dispose();
            pictureBox1.Image = (Bitmap)bmp.Clone();

            stopwatch.Stop();
            renderTimesMsec.Add(1000.0 * stopwatch.ElapsedTicks / Stopwatch.Frequency);
            double mean = renderTimesMsec.Sum() / renderTimesMsec.Count();
            Debug.WriteLine($"Render {renderTimesMsec.Count:00} " +
                $"took {renderTimesMsec.Last():0.000} ms " +
                $"(running mean: {mean:0.000} ms)");

            Application.DoEvents();
        }

        private void Benchmark(int lineCount, int times = 10)
        {
            rand = new Random(0);
            renderTimesMsec.Clear();
            for (int i = 0; i < times; i++)
                Render(lineCount);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Benchmark(10);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Benchmark(1_000);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Benchmark(10_000);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Benchmark(100_000);
        }
    }
}
