using Microsoft.Maui.Graphics;
using Microsoft.Maui.Graphics.Skia;
using System;
using System.Windows.Forms;

namespace QuickstartWinForms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void skglControl1_PaintSurface(object sender, SkiaSharp.Views.Desktop.SKPaintGLSurfaceEventArgs e)
        {
            ICanvas canvas = new SkiaCanvas() { Canvas = e.Surface.Canvas };

            canvas.FillColor = Colors.Navy;
            canvas.FillRectangle(0, 0, skglControl1.Width, skglControl1.Height);

            canvas.StrokeColor = Colors.White.WithAlpha(.5f);
            canvas.StrokeSize = 2;
            for (int i = 0; i < 100; i++)
            {
                float x = Random.Shared.Next(skglControl1.Width);
                float y = Random.Shared.Next(skglControl1.Height);
                float r = Random.Shared.Next(5, 50);
                canvas.DrawCircle(x, y, r);
            }
        }

        private void skglControl1_SizeChanged(object sender, EventArgs e) => skglControl1.Invalidate();
        private void button1_Click(object sender, EventArgs e) => skglControl1.Invalidate();
        private void timer1_Tick(object sender, EventArgs e) => skglControl1.Invalidate();
        private void checkBox1_CheckedChanged(object sender, EventArgs e) => timer1.Enabled = checkBox1.Checked;
    }
}
