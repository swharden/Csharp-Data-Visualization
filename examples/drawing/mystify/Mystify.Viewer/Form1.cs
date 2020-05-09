using Mystify.Model;
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
        public Form1()
        {
            InitializeComponent();
            skglControl1.Dock = DockStyle.Fill;
            pictureBox1.Visible = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Reset();
        }

        private void panel1_SizeChanged(object sender, EventArgs e)
        {
            Reset();
        }

        readonly Random rand = new Random();
        Polygon[] polys;
        private void Reset()
        {
            stopwatch.Restart();
            renderCount = 0;
            polys = new Polygon[(int)nudPolygons.Value];
            double colorShift = rand.NextDouble();
            for (int i = 0; i < polys.Length; i++)
            {
                polys[i] = new Polygon(rand, (int)nudCorners.Value, 
                    skglControl1.Width, skglControl1.Height, 
                    (int)nudHistory.Value, (double)i/polys.Length + colorShift);
                
            }
            polys = polys.OrderBy(x => rand.Next()).ToArray();
        }
        private void nudPolygons_ValueChanged(object sender, EventArgs e) { Reset(); }
        private void nudCorners_ValueChanged(object sender, EventArgs e) { Reset(); }
        private void nudHistory_ValueChanged(object sender, EventArgs e) { Reset(); }
        private void nudSpacing_ValueChanged(object sender, EventArgs e) { Reset(); }

        readonly Stopwatch stopwatch = new Stopwatch();
        int renderCount = 0;

        private void skglControl1_PaintSurface(object sender, SkiaSharp.Views.Desktop.SKPaintGLSurfaceEventArgs e)
        {
            var surface = e.Surface;

            surface.Canvas.Clear(SKColors.Black);

            var paint = new SKPaint
            {
                Style = SKPaintStyle.Stroke,
                Color = SKColors.White,
                StrokeWidth = 1,
                IsAntialias = true
            };

            bool fade = false;

            foreach(Polygon poly in polys)
            {
                paint.Color = new SKColor(poly.Color.R, poly.Color.G, poly.Color.B, poly.Color.A);
                for (int historyIndex = 0; historyIndex < poly.Corners[0].Points.Count(); historyIndex++)
                {
                    var historyFrac = (double)historyIndex / (poly.Corners[0].Points.Count() - 1);
                    using (var path = new SKPath())
                    {
                        for (int cornerIndex = 0; cornerIndex < poly.Corners.Count(); cornerIndex++)
                        {
                            var corner = poly.Corners[cornerIndex];
                            if (cornerIndex == 0)
                                path.MoveTo(corner.Points[historyIndex].X, corner.Points[historyIndex].Y);
                            else
                                path.LineTo(corner.Points[historyIndex].X, corner.Points[historyIndex].Y);
                        }
                        path.Close();

                        if (fade)
                            paint.Color = new SKColor(255, 255, 255, (byte)(255 * historyFrac));

                        e.Surface.Canvas.DrawPath(path, paint);
                    }
                }
            }


            renderCount += 1;
        }

        private void timerRender_Tick(object sender, EventArgs e)
        {
            foreach(Polygon poly in polys)
                poly.Advance((double)nudSpeed.Value, cbRainbow.Checked);

            skglControl1.Invalidate();

            double elapsedSec = (double)stopwatch.ElapsedTicks / Stopwatch.Frequency;
            double framesPerSec = renderCount / elapsedSec;
            lblStatus.Text = $"Rendered {renderCount} frames " +
                $"in {elapsedSec:0.00} seconds " +
                $"({framesPerSec:0.00} FPS)";
        }

    }
}
