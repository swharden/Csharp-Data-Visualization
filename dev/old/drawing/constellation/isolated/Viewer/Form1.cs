using GraphicsModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Viewer
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
            field = new Field(pictureBox1.Width, pictureBox1.Height, 3);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            field.Dispose();
            base.OnClosing(e);
        }

        readonly Stopwatch stopwatch = new Stopwatch();
        private void timer1_Tick(object sender, EventArgs e)
        {
            stopwatch.Restart();
            using (Bitmap bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height, PixelFormat.Format32bppPArgb))
            {
                RenderSystemDrawing.Renderer.Render(field, bmp);
                pictureBox1.Image?.Dispose();
                pictureBox1.Image = (Bitmap)bmp.Clone();
            }

            stopwatch.Stop();
            double elapsedSec = (double)stopwatch.ElapsedTicks / Stopwatch.Frequency;
            Text = $"Constellation (Isolated) {1 / elapsedSec:0.00} FPS";
        }
    }
}
