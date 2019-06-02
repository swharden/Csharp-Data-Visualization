using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrawWithMouse
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            InitializeBitmapAndGraphics();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

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

        private void PictureBox1_SizeChanged(object sender, EventArgs e)
        {
            InitializeBitmapAndGraphics();
        }

        private Point lastMousePosition;
        private void PictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            Console.WriteLine(e.Location);
            if (e.Button == MouseButtons.Left && lastMousePosition != null)
            {
                Pen pen = new Pen(Color.Black, (float)numericUpDown1.Value)
                {
                    StartCap = System.Drawing.Drawing2D.LineCap.Round,
                    EndCap = System.Drawing.Drawing2D.LineCap.Round,
                    LineJoin = System.Drawing.Drawing2D.LineJoin.Round
                };
                gfx.DrawLine(pen, lastMousePosition, e.Location);
                pictureBox1.Image = bmp;
            }
            lastMousePosition = e.Location;
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            InitializeBitmapAndGraphics();
        }
    }
}
