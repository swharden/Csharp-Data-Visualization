using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Random rand = new Random();
        Bitmap bmp;
        Stopwatch stopwatch = Stopwatch.StartNew();
        int renderCount = 0;
        private void Render()
        {
            // create or resize the bitmap if needed
            if (bmp==null || bmp.Size != pictureBox1.Size)
            {
                bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            }

            // draw on the bitmap
            using (Graphics gfx = Graphics.FromImage(bmp))
            {
                gfx.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                gfx.Clear(Color.DarkBlue);
                Color myColor = Color.FromArgb(123, Color.White);
                Pen myPen = new Pen(myColor);

                for (int i = 0; i < 1000; i++)
                {
                    Point ptA = new Point(rand.Next(bmp.Width), rand.Next(bmp.Height));
                    Point ptB = new Point(rand.Next(bmp.Width), rand.Next(bmp.Height));
                    gfx.DrawLine(myPen, ptA, ptB);
                }
            }

            // apply the new bitmap to the picturebox and force a redraw
            pictureBox1.Image = bmp;

            // update the FPS display
            renderCount += 1;
            double elapsedSeconds = (double)stopwatch.ElapsedMilliseconds / 1000;
            Text = string.Format("Rendered {0} frames in {1:0.00} seconds ({2:0.00} Hz)", renderCount, elapsedSeconds, renderCount / elapsedSeconds);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Render();
        }
    }
}
