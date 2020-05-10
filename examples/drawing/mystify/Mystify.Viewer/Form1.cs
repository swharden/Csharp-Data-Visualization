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
            cbGraphics.SelectedIndex = 0;
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
                    (int)nudHistory.Value, (double)i / polys.Length + colorShift);

            }
            polys = polys.OrderBy(x => rand.Next()).ToArray();
        }
        private void nudPolygons_ValueChanged(object sender, EventArgs e) { Reset(); }
        private void nudCorners_ValueChanged(object sender, EventArgs e) { Reset(); }
        private void nudHistory_ValueChanged(object sender, EventArgs e) { Reset(); }
        private void nudSpacing_ValueChanged(object sender, EventArgs e) { Reset(); }
        private void btnReset_Click(object sender, EventArgs e) { Reset(); }

        readonly Stopwatch stopwatch = new Stopwatch();
        int renderCount = 0;

        private void skglControl1_PaintSurface(object sender, SkiaSharp.Views.Desktop.SKPaintGLSurfaceEventArgs e)
        {
            var renderer = new SkiaRenderer(e.Surface.Canvas);
            Field.Render(renderer, polys, (float)nudWidth.Value, cbFade.Checked);
            renderCount += 1;
        }

        private void timerRender_Tick(object sender, EventArgs e)
        {
            foreach (Polygon poly in polys)
                poly.Advance((double)nudSpeed.Value, cbRainbow.Checked);

            if (cbGraphics.Text.Contains("Skia"))
            {
                skglControl1.Invalidate();
            }
            else
            {
                var bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
                var rend = new DrawingRenderer(bmp);
                Field.Render(rend, polys, (float)nudWidth.Value, cbFade.Checked);
                pictureBox1.Image = bmp;
            }

            double elapsedSec = (double)stopwatch.ElapsedTicks / Stopwatch.Frequency;
            double framesPerSec = renderCount / elapsedSec;
            lblStatus.Text = $"Rendered {renderCount} frames " +
                $"in {elapsedSec:0.00} seconds " +
                $"({framesPerSec:0.00} FPS)";
        }

        private void cbGraphics_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbGraphics.Text.Contains("Skia"))
            {
                pictureBox1.Visible = false;
                skglControl1.Dock = DockStyle.Fill;
                skglControl1.Visible = true;
                Reset();
            }
            else
            {
                skglControl1.Visible = false;
                pictureBox1.Dock = DockStyle.Fill;
                pictureBox1.Visible = true;
                Reset();
            }
        }
    }
}
