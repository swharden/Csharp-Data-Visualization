using Microsoft.Maui.Graphics;
using Microsoft.Maui.Graphics.Skia;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsFrameworkApp
{
    public partial class Form1 : Form
    {
        readonly Random Rand = new();
        readonly SkiaSharp.Views.Desktop.SKGLControl skglControl1;

        public Form1()
        {
            InitializeComponent();

            skglControl1 = new SkiaSharp.Views.Desktop.SKGLControl()
            {
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                BackColor = System.Drawing.Color.Black,
                Location = new System.Drawing.Point(12, 41),
                Margin = new Padding(4, 3, 4, 3),
                Name = "skglControl1",
                Size = new System.Drawing.Size(775, 397),
                TabIndex = 0,
                VSync = true,
            };

            Controls.Add(skglControl1);

            skglControl1.PaintSurface += new EventHandler<SkiaSharp.Views.Desktop.SKPaintGLSurfaceEventArgs>(skglControl1_PaintSurface);
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
