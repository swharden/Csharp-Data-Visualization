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

namespace IntroGDI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        IntroAnimation.Field field;
        Bitmap bmp;
        private void Render(int cornerCount = 100, int maxVisibleDistance = 200)
        {
            // create or resize the bitmap and field if needed
            if (bmp == null || bmp.Size != pictureBox1.Size)
            {
                bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
                field = new IntroAnimation.Field(bmp.Width, bmp.Height, cornerCount);
            }

            // step the field forward in time
            field.StepForward(3);

            // draw on the bitmap
            using (Graphics gfx = Graphics.FromImage(bmp))
            {
                gfx.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                var bgColor = field.GetBackgroundColor();
                gfx.Clear(Color.FromArgb(bgColor.R, bgColor.G, bgColor.B));

                Color myColor = Color.FromArgb(255, Color.White);
                Pen myPen = new Pen(myColor);
                Brush myBrush = new SolidBrush(myColor);

                // draw circles at every corner
                float radius = 2;
                for (int cornerIndex = 0; cornerIndex < field.corners.Length; cornerIndex++)
                    gfx.FillEllipse(myBrush, (float)field.corners[cornerIndex].X - radius, (float)field.corners[cornerIndex].Y - radius, radius * 2, radius * 2);

                // draw lines between every corner and every other corner
                for (int i = 0; i < field.corners.Length; i++)
                {
                    for (int j = 0; j < field.corners.Length; j++)
                    {
                        double distance = field.GetDistance(i, j);
                        if (distance < maxVisibleDistance && distance != 0)
                        {
                            PointF pt1 = new PointF((float)field.corners[i].X, (float)field.corners[i].Y);
                            PointF pt2 = new PointF((float)field.corners[j].X, (float)field.corners[j].Y);
                            double distanceFraction = distance / maxVisibleDistance;
                            byte alpha = (byte)(255 - distanceFraction * 256);
                            myPen.Color = Color.FromArgb(alpha, Color.White);
                            gfx.DrawLine(myPen, pt1, pt2);
                        }
                    }
                }
            }

            // apply the new bitmap to the picturebox and force a redraw
            pictureBox1.Image = bmp;

            // update the FPS display
            Text = field.GetBenchmarkMessage();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Render();
        }
    }
}
