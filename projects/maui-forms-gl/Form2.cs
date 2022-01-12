using Microsoft.Maui.Graphics;
using Microsoft.Maui.Graphics.Skia;
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

namespace FormsLife
{
    public partial class Form2 : Form
    {
        readonly Random Rand = new();

        readonly Stopwatch Watch = Stopwatch.StartNew();

        readonly int LineCount = 10_000;

        public Form2()
        {
            InitializeComponent();
        }

        private void UpdateBenchmarkMessage()
        {
            Text = $"skControl [{skControl1.Width}x{skControl1.Height}] " +
                $"Rendered {LineCount:N0} lines rendered " +
                $"in {Watch.Elapsed.TotalMilliseconds} ms " +
                $"({1 / Watch.Elapsed.TotalSeconds:N1} Hz)";
            Watch.Restart();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            skControl1.Invalidate();
        }

        private void skControl1_PaintSurface(object sender, SkiaSharp.Views.Desktop.SKPaintSurfaceEventArgs e)
        {
            ICanvas canvas = new SkiaCanvas() { Canvas = e.Surface.Canvas };
            Graphics.RandomLines(Rand, canvas, skControl1.Width, skControl1.Height, LineCount);
            UpdateBenchmarkMessage();
        }
    }
}
