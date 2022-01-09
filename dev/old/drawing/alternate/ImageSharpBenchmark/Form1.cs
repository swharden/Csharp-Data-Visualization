using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
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

namespace ImageSharpBenchmark
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            Debug.WriteLine($"This is a {IntPtr.Size * 8}-bit application");

            if (System.Numerics.Vector.IsHardwareAccelerated)
                Debug.WriteLine($"hardware acceleration ENABLED");
            else
                Debug.WriteLine($"hardware acceleration DISABLED");
        }

        Random rand = new Random(0);
        List<double> renderTimesMsec = new List<double>();
        private void Render(int lineCount)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            using (var image = new Image<SixLabors.ImageSharp.PixelFormats.Rgba32>(600, 400))
            {
                image.Mutate(imageContext =>
                {
                    // draw background
                    var bgColor = SixLabors.ImageSharp.PixelFormats.Rgba32.FromHex("#003366");
                    imageContext.BackgroundColor(bgColor);

                    for (int i = 0; i < lineCount; i++)
                    {
                        // create an array of two points to make the straight line
                        var points = new SixLabors.Primitives.PointF[2];
                        points[0] = new SixLabors.Primitives.PointF(
                            x: (float)(rand.NextDouble() * pictureBox1.Width),
                            y: (float)(rand.NextDouble() * pictureBox1.Height));
                        points[1] = new SixLabors.Primitives.PointF(
                            x: (float)(rand.NextDouble() * pictureBox1.Width),
                            y: (float)(rand.NextDouble() * pictureBox1.Height));

                        // create a pen unique to this line
                        var lineColor = SixLabors.ImageSharp.Color.FromRgba(
                            r: (byte)rand.Next(255),
                            g: (byte)rand.Next(255),
                            b: (byte)rand.Next(255),
                            a: (byte)rand.Next(255));
                        float lineWidth = rand.Next(1, 10);
                        var linePen = new SixLabors.ImageSharp.Processing.Pen(lineColor, lineWidth);

                        // draw the line
                        imageContext.DrawLines(linePen, points);
                    }
                });

                // render onto an Image
                var stream = new System.IO.MemoryStream();
                image.SaveAsBmp(stream);
                System.Drawing.Image img = System.Drawing.Image.FromStream(stream);

                pictureBox1.Image?.Dispose();
                pictureBox1.Image = img;
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
