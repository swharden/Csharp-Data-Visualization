using OpenTK.Graphics.ES20;
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

namespace Starfield.WinFormsSkiaSharp
{
    public partial class Form1 : Form
    {
        readonly Field field = new Field(500);

        public Form1()
        {
            InitializeComponent();
        }

        private void skglControl1_PaintSurface(object sender, SkiaSharp.Views.Desktop.SKPaintGLSurfaceEventArgs e)
        {
            // perform the drawing
            e.Surface.Canvas.Clear(SKColors.Black);
            var starColor = new SKColor(255, 255, 255, starAlpha);
            var starPaint = new SKPaint() { IsAntialias = true, Color = starColor };
            foreach (var star in field.GetStars())
            {
                float xPixel = (float)star.x * skglControl1.Width;
                float yPixel = (float)star.y * skglControl1.Height;
                float radius = (float)star.size - 1;
                var point = new SKPoint(xPixel, yPixel);
                e.Surface.Canvas.DrawCircle(point, radius, starPaint);
            }
        }

        Stopwatch stopwatch = new Stopwatch();
        private void timer1_Tick(object sender, EventArgs e)
        {
            stopwatch.Restart();
            field.Advance();
            skglControl1.Invalidate();
            double elapsedSec = (double)stopwatch.ElapsedTicks / Stopwatch.Frequency;
            Text = $"Starfield in Windows Forms - {elapsedSec * 1000:0.00} ms ({1 / elapsedSec:0.00} FPS)";
        }

        private void rb500_CheckedChanged(object sender, EventArgs e)
        {
            field.Reset(500);
        }

        private void rb100k_CheckedChanged(object sender, EventArgs e)
        {
            field.Reset(100_000);
        }

        static byte starAlpha = 255;
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            starAlpha = (byte)(trackBar1.Value * 255 / 100);
            lblAlpha.Text = $"{trackBar1.Value}%";
        }
    }
}
