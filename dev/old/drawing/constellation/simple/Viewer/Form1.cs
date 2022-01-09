using GraphicsModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Viewer
{
    public partial class Form1 : Form
    {
        Field field;

        public Form1()
        {
            InitializeComponent();
            Form1_SizeChanged(null, null);
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            field?.Dispose();
            field = new Field(pictureBox1.Width, pictureBox1.Height, 3);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            field.Dispose();
            base.OnClosing(e);
        }

        readonly Stopwatch stopwatch = new Stopwatch();
        private void timer1_Tick(object sender, EventArgs e)
        {
            stopwatch.Restart();
            using (Bitmap bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height, PixelFormat.Format32bppPArgb))
            using (Graphics gfx = Graphics.FromImage(bmp))
            using (Brush brush = new SolidBrush(Color.White))
            using (Pen pen = new Pen(Color.White))
            {
                gfx.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                // clear background
                gfx.Clear(ColorTranslator.FromHtml("#003366"));

                // draw stars
                float starRadius = 3;
                foreach (var star in field.Stars)
                {
                    var rect = new RectangleF(
                            x: star.X - starRadius,
                            y: star.Y - starRadius,
                            width: starRadius * 2,
                            height: starRadius * 2
                        );

                    gfx.FillEllipse(brush, rect);
                }

                // draw lines connecting close stars
                double connectDistance = 100;
                foreach (var star1 in field.Stars)
                {
                    foreach (var star2 in field.Stars)
                    {
                        double dX = Math.Abs(star1.X - star2.X);
                        double dY = Math.Abs(star1.Y - star2.Y);
                        if (dX > connectDistance || dY > connectDistance)
                            continue;
                        double distance = Math.Sqrt(dX * dX + dY * dY);
                        int alpha = (int)(255 - distance / connectDistance * 255);
                        alpha = Math.Min(alpha, 255);
                        alpha = Math.Max(alpha, 0);
                        pen.Color = Color.FromArgb(alpha, Color.White);
                        if (distance < connectDistance)
                            gfx.DrawLine(pen, star1.X, star1.Y, star2.X, star2.Y);
                    }
                }

                pictureBox1.Image?.Dispose();
                pictureBox1.Image = (Bitmap)bmp.Clone();
            }

            stopwatch.Stop();
            double elapsedSec = (double)stopwatch.ElapsedTicks / Stopwatch.Frequency;
            Text = $"Constellation with System.Drawing (Simple) {1 / elapsedSec:0.00} FPS";
        }
    }
}
