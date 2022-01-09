using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace graphicsBasics
{
    public partial class Form1 : Form
    {
        Random rand = new Random();
        public Form1()
        {
            InitializeComponent();
            InitializeBitmapAndGraphics();
        }

        private Bitmap bmp;
        private Graphics gfx;

        private void InitializeBitmapAndGraphics()
        {
            bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            gfx = Graphics.FromImage(bmp);
            gfx.Clear(Color.White);
            gfx.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            Console.WriteLine($"Created new bitmap {bmp.Size}");
            pictureBox1.Image = bmp;
        }

        private void AddRandomLine()
        {
            if (gfx != null)
            {
                Point pt1 = new Point(rand.Next(bmp.Width), rand.Next(bmp.Height));
                Point pt2 = new Point(rand.Next(bmp.Width), rand.Next(bmp.Height));
                float lineWidth = (float)(rand.NextDouble() * 3);
                Pen pen = new Pen(Color.Blue, lineWidth);
                Console.WriteLine($"Drawing {lineWidth} px line to connect {pt1} and {pt2}");
                gfx.DrawLine(pen, pt1, pt2);
                pictureBox1.Image = bmp;
            }
        }

        private void BtnAddLine_Click(object sender, EventArgs e)
        {
            AddRandomLine();
        }

        private void CbAddLines_CheckedChanged(object sender, EventArgs e)
        {
            timer1.Enabled = cbAddLines.Checked;
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            AddRandomLine();
        }

        private void PictureBox1_SizeChanged(object sender, EventArgs e)
        {
            InitializeBitmapAndGraphics();
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            InitializeBitmapAndGraphics();
        }
    }
}
