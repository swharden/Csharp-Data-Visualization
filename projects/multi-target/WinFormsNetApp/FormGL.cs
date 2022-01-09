using Microsoft.Maui.Graphics;
using Microsoft.Maui.Graphics.Skia;

namespace WinFormsNetApp
{
    public partial class FormGL : Form
    {
        readonly Random Rand = new();

        public FormGL()
        {
            InitializeComponent();
        }

        private void skglControl1_PaintSurface(object sender, SkiaSharp.Views.Desktop.SKPaintGLSurfaceEventArgs e)
        {
            ICanvas canvas = new SkiaCanvas() { Canvas = e.Surface.Canvas };
            StandardGraphics.Render.TestImage(Rand, canvas, skglControl1.Width, skglControl1.Height);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            skglControl1.Invalidate();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            timer1.Enabled = checkBox1.Checked;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            skglControl1.Invalidate();
        }
    }
}