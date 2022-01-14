using Microsoft.Maui.Graphics;
using Microsoft.Maui.Graphics.Skia;

namespace FormsViewer
{
    public partial class Form1 : Form
    {
        GraphicsModelLibrary.IGraphicsModel ActiveModel;

        public Form1()
        {
            InitializeComponent();
            //ActiveModel = new GraphicsModelLibrary.Starfield.StarfieldModel();
            ActiveModel = new GraphicsModelLibrary.BallPit.BallPitModel();
            ActiveModel.Reset(skglControl1.Width, skglControl1.Height);
        }

        private void skglControl1_SizeChanged(object sender, EventArgs e)
        {
            ActiveModel.Resize(skglControl1.Width, skglControl1.Height);
        }

        private void skglControl1_PaintSurface(object sender, SkiaSharp.Views.Desktop.SKPaintGLSurfaceEventArgs e)
        {
            ICanvas canvas = new SkiaCanvas() { Canvas = e.Surface.Canvas };
            ActiveModel.Draw(canvas, skglControl1.Width, skglControl1.Height);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            ActiveModel.Advance();
            skglControl1.Invalidate();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            timer1.Enabled = checkBox1.Checked;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ActiveModel.Reset(skglControl1.Width, skglControl1.Height);
            skglControl1.Invalidate();
        }
    }
}