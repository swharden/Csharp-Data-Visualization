using Microsoft.Maui.Graphics;
using Microsoft.Maui.Graphics.Skia;

namespace FormsLife
{
    public partial class Form1 : Form
    {
        readonly Life.Board Board = new(150, 75);
        System.Diagnostics.Stopwatch Watch = System.Diagnostics.Stopwatch.StartNew();

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Advance();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            timer1.Enabled = checkBox1.Checked;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Advance();
        }

        private void Advance()
        {
            Board.Advance();
            skglControl1.Invalidate();

            Text = $"[{skglControl1.Width}x{skglControl1.Height}] " +
                $"Rendered in {Watch.Elapsed.TotalMilliseconds:N2} ms " +
                $"({1 / Watch.Elapsed.TotalSeconds:N2} Hz)";
            Watch.Restart();
        }

        private void skglControl1_PaintSurface(object sender, SkiaSharp.Views.Desktop.SKPaintGLSurfaceEventArgs e)
        {
            ICanvas canvas = new SkiaCanvas() { Canvas = e.Surface.Canvas };
            Life.Render.DrawBoard(Board, canvas, skglControl1.Width, skglControl1.Height);
        }

        private void btnFaster_Click(object sender, EventArgs e)
        {
            timer1.Interval = Math.Max(1, timer1.Interval / 2);
        }

        private void btnSlower_Click(object sender, EventArgs e)
        {
            timer1.Interval *= 2;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Board.Clear();
            skglControl1.Invalidate();
        }

        private void btnAddRandom_Click(object sender, EventArgs e)
        {
            Board.AddRandom(.1);
            skglControl1.Invalidate();
        }
    }
}