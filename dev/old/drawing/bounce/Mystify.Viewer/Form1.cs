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

namespace Mystify.Viewer
{
    public partial class Form1 : Form
    {
        Random rand = new Random();

        public Form1()
        {
            InitializeComponent();
        }

        SkiaSharp.Views.Desktop.SKGLControl skglControl1;
        private void Form1_Load(object sender, EventArgs e)
        {
            // create an anti-aliased skglControl and add it to the panel
            var cf = new OpenTK.Graphics.ColorFormat(8, 8, 8, 8);
            var gm = new OpenTK.Graphics.GraphicsMode(color: cf, depth: 24, stencil: 8, samples: 4);
            skglControl1 = new SkiaSharp.Views.Desktop.SKGLControl(gm);
            skglControl1.BackColor = Color.Black;
            skglControl1.Dock = DockStyle.Fill;
            skglControl1.VSync = true;
            skglControl1.PaintSurface += SkglControl1_PaintSurface;
            panel1.Controls.Add(skglControl1);

            Reset();
        }

        private void panel1_SizeChanged(object sender, EventArgs e)
        {
            Reset();
        }

        Model.Corner testCorner;
        private void Reset()
        {
            stopwatch.Restart();
            renderCount = 0;
            testCorner = new Model.Corner(rand, skglControl1.Width, skglControl1.Height);
        }

        readonly Stopwatch stopwatch = new Stopwatch();
        int renderCount = 0;

        private void SkglControl1_PaintSurface(object sender, SkiaSharp.Views.Desktop.SKPaintGLSurfaceEventArgs e)
        {
            var surface = e.Surface;

            surface.Canvas.Clear(SKColor.Parse("#003366"));

            var paint = new SKPaint
            {
                Style = SKPaintStyle.Stroke,
                Color = SKColors.White,
                StrokeWidth = 1,
                IsAntialias = true
            };

            for (int i = 0; i < testCorner.Points.Count; i++)
            {
                double fracToEnd = (double)i / (testCorner.Points.Count - 1);
                paint.Color = new SKColor(255, 255, 255, (byte)(255 * fracToEnd));
                if (fracToEnd == 1)
                    paint.Style = SKPaintStyle.Fill;

                var pt = new SKPoint(testCorner.Points[i].X, testCorner.Points[i].Y);
                surface.Canvas.DrawCircle(pt, 10f, paint);
            }

            renderCount += 1;
        }

        private void timerRender_Tick(object sender, EventArgs e)
        {
            testCorner.Advance(10);
            skglControl1.Invalidate();

            double elapsedSec = (double)stopwatch.ElapsedTicks / Stopwatch.Frequency;
            double framesPerSec = renderCount / elapsedSec;
            lblStatus.Text = $"Rendered {renderCount} frames " +
                $"in {elapsedSec:0.00} seconds " +
                $"({framesPerSec:0.00} FPS)";
        }
    }
}
