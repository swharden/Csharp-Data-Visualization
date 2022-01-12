
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Graphics.Skia;
using System.Diagnostics;

namespace FormsLife
{
    public partial class Form1 : Form
    {
        readonly Random Rand = new();

        readonly Stopwatch Watch = Stopwatch.StartNew();

        readonly int LineCount = 10_000;

        public Form1()
        {
            InitializeComponent();
        }

        private void UpdateBenchmarkMessage()
        {
            Text = $"skglControl [{skglControl1.Width}x{skglControl1.Height}] " +
                $"Rendered {LineCount:N0} lines rendered " +
                $"in {Watch.Elapsed.TotalMilliseconds} ms " +
                $"({1 / Watch.Elapsed.TotalSeconds:N1} Hz)";
            Watch.Restart();
        }

        private void skglControl1_PaintSurface(object sender, SkiaSharp.Views.Desktop.SKPaintGLSurfaceEventArgs e)
        {
            ICanvas canvas = new SkiaCanvas() { Canvas = e.Surface.Canvas };
            Graphics.RandomLines(Rand, canvas, skglControl1.Width, skglControl1.Height, LineCount);
            UpdateBenchmarkMessage();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            skglControl1.Invalidate();
        }
    }
}