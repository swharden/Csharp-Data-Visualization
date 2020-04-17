using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuickstartWinForms
{
    public partial class Form1 : Form
    {
        Random rand = new Random();
        Bitmap bmp;
        Graphics gfx;

        public Form1()
        {
            InitializeComponent();
            Render();
        }

        private void pictureBox1_SizeChanged(object sender, EventArgs e)
        {
            Render();
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            Render();
        }

        void ResizeBitmap()
        {
            bmp?.Dispose();
            gfx?.Dispose();
            bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            gfx = Graphics.FromImage(bmp);
            pictureBox1.Image = bmp;
        }

        void Render()
        {
            if (bmp is null || bmp?.Size != pictureBox1.Size)
                ResizeBitmap();

            using (var brush = new SolidBrush(Color.White))
            using (var pen = new Pen(brush))
            {
                gfx.SmoothingMode = SmoothingMode.AntiAlias;

                gfx.Clear(Color.Navy);
                for (int i = 0; i < 1000; i++)
                {
                    gfx.DrawLine(pen: pen,
                        x1: rand.Next(bmp.Width),
                        y1: rand.Next(bmp.Height),
                        x2: rand.Next(bmp.Width),
                        y2: rand.Next(bmp.Height));
                }
            }

            pictureBox1.Invalidate();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
