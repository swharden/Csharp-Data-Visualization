using GraphicsModel;
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

namespace ViewerGL
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
            field = new Field(skglControl1.Width, skglControl1.Height, 3);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            field.Dispose();
        }

        readonly Stopwatch stopwatch = new Stopwatch();
        private void skglControl1_PaintSurface(object sender, SkiaSharp.Views.Desktop.SKPaintGLSurfaceEventArgs e)
        {
            if (field is null)
                return;

            stopwatch.Restart();
            var canvas = e.Surface.Canvas;

            // clear background
            canvas.Clear(SKColor.Parse("#003366"));

            // draw stars
            float starRadius = 3;
            SKPaint paint = new SKPaint() { IsAntialias = true, Color = SKColors.White };
            foreach (var star in field.Stars)
            {
                var rect = new RectangleF(
                        x: star.X - starRadius,
                        y: star.Y - starRadius,
                        width: starRadius * 2,
                        height: starRadius * 2
                    );

                var pt = new SKPoint(star.X, star.Y);
                e.Surface.Canvas.DrawCircle(pt, starRadius, paint);
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
                    paint.Color = new SKColor(255, 255, 255, (byte)alpha);
                    if (distance < connectDistance)
                        canvas.DrawLine(star1.X, star1.Y, star2.X, star2.Y, paint);
                }
            }

            stopwatch.Stop();
            double elapsedSec = (double)stopwatch.ElapsedTicks / Stopwatch.Frequency;
            Text = $"Constellation with SkiaSharp and OpenGL (Simple) {1 / elapsedSec:0.00} FPS";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            skglControl1.Invalidate();
        }
    }
}
