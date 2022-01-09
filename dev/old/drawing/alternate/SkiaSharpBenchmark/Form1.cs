using SkiaSharp;
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

namespace SkiaSharpBenchmark
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Random rand = new Random(0);
        List<double> renderTimesMsec = new List<double>();
        private void Render(int lineCount)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            var imageInfo = new SKImageInfo(
                width: pictureBox1.Width,
                height: pictureBox1.Height,
                colorType: SKColorType.Rgba8888,
                alphaType: SKAlphaType.Premul);

            var surface = SKSurface.Create(imageInfo);
            var canvas = surface.Canvas;

            canvas.Clear(SKColor.Parse("#003366"));

            for (int i = 0; i < lineCount; i++)
            {
                float lineWidth = rand.Next(1, 10);
                var lineColor = new SKColor(
                        red: (byte)rand.Next(255),
                        green: (byte)rand.Next(255),
                        blue: (byte)rand.Next(255),
                        alpha: (byte)rand.Next(255));

                var linePaint = new SKPaint
                {
                    Color = lineColor,
                    StrokeWidth = lineWidth,
                    IsAntialias = true,
                    Style = SKPaintStyle.Stroke
                };

                int x1 = rand.Next(imageInfo.Width);
                int y1 = rand.Next(imageInfo.Height);
                int x2 = rand.Next(imageInfo.Width);
                int y2 = rand.Next(imageInfo.Height);
                canvas.DrawLine(x1, y1, x2, y2, linePaint);
            }

            using (SKImage image = surface.Snapshot())
            using (SKData data = image.Encode())
            using (System.IO.MemoryStream mStream = new System.IO.MemoryStream(data.ToArray()))
            {
                pictureBox1.Image?.Dispose();
                pictureBox1.Image = new Bitmap(mStream, false);
            }

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
