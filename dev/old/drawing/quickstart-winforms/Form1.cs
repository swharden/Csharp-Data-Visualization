using System;
using System.Drawing;
using System.Windows.Forms;

namespace DrawingQuickstartWinForms
{
    public partial class Form1 : Form
    {
        Random rand = new Random();

        public Form1()
        {
            InitializeComponent();
            Render();
        }

        void Render()
        {
            using (var bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height))
            using (var gfx = Graphics.FromImage(bmp))
            using (var pen = new Pen(Color.White))
            {
                // draw one thousand random white lines on a dark blue background
                gfx.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                gfx.Clear(Color.Navy);
                for (int i = 0; i < 1000; i++)
                {
                    var pt1 = new Point(rand.Next(bmp.Width), rand.Next(bmp.Height));
                    var pt2 = new Point(rand.Next(bmp.Width), rand.Next(bmp.Height));
                    gfx.DrawLine(pen, pt1, pt2);
                }

                // copy the bitmap to the picturebox (double buffered)
                pictureBox1.Image?.Dispose();
                pictureBox1.Image = (Bitmap)bmp.Clone();
            }
        }

        private void pictureBox1_SizeChanged(object sender, EventArgs e)
        {
            Render();
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            Render();
        }
    }
}
