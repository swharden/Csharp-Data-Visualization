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

            using (var renderer = new SkiaSharpRenderer(e.Surface.Canvas))
            {
                field.Render(renderer);
            }

            stopwatch.Stop();
            double elapsedSec = (double)stopwatch.ElapsedTicks / Stopwatch.Frequency;
            Text = $"Constellation with SkiaSharp and OpenGL (Injected) {1 / elapsedSec:0.00} FPS";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            skglControl1.Invalidate();
        }
    }
}
