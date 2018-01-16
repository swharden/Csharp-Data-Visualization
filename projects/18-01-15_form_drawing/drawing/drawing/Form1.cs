using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace drawing
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
            // doing this reduced panel flickering by double buffering
            typeof(Panel).InvokeMember("DoubleBuffered", System.Reflection.BindingFlags.SetProperty | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic, null, panel1, new object[] { true });
            pictureBox1.BackColor = Color.FromArgb(100, 255, 255, 255);
        }

        private void timer_init_Tick(object sender, EventArgs e)
        {
            timer_init.Enabled = false;
            layout(null, null);
        }
        
        private void layout(object sender, EventArgs e)
        {

            // PLACE AND SIZE CONTROLS

            int data_pad_bot = 70; // how large the bottom "bar" is below the data area
            int data_pad_left = 100; // pixels from the left side to display data

            int distLeft_zoom_y = 30;
            btn_zoom_in_y.Location = new Point(distLeft_zoom_y, (panel1.Height-data_pad_bot) / 2 - btn_zoom_in_y.Height);
            btn_zoom_out_y.Location = new Point(distLeft_zoom_y, (panel1.Height - data_pad_bot) / 2);

            btn_zoom_out_x.Location = new Point(0, panel1.Height - data_pad_bot + 2);
            btn_zoom_in_x.Location = new Point(btn_zoom_in_x.Width, panel1.Height - data_pad_bot + 2);

            vScrollBar1.Location = new Point(panel1.Width - vScrollBar1.Width, 0);
            vScrollBar1.Height = panel1.Height - data_pad_bot;
            hScrollBar1.Location = new Point(data_pad_left, panel1.Height - hScrollBar1.Height);
            hScrollBar1.Width = panel1.Width - data_pad_left;

            pictureBox1.Location = new Point(data_pad_left, 0);
            pictureBox1.Width = panel1.Width - data_pad_left - vScrollBar1.Width;
            pictureBox1.Height = panel1.Height - data_pad_bot;

            // PREPARE GRAPHICS OBJECT

            /// create a bitmap the size of our panel, draw on the bitmap, then apply the bitmap to the panel (avoids flickering)
            Bitmap bitmap = new Bitmap(panel1.Width, panel1.Height);
            System.Drawing.Graphics gfxFrame = Graphics.FromImage(bitmap);

            /// if we were going to directly draw on the memory buffer (causing flickering) we would use:
            //Graphics gfxFrame = panel1.CreateGraphics();
            gfxFrame.Clear(SystemColors.Control);

            // DRAW COLORED AREA ON FAR LEFT
            gfxFrame.FillRectangle(new SolidBrush(Color.DarkGray), new Rectangle(0, 0, distLeft_zoom_y, panel1.Height - data_pad_bot));
            
            // DRAW AXIS LABELS

            // draw the label for units on the far left
            string axis_label_y = "Analog Input 0\n(pA)";
            Font font_axis_labels = new Font("arial", 9, FontStyle.Regular);
            SizeF axis_label_y_size = gfxFrame.MeasureString(axis_label_y, font_axis_labels);
            StringFormat string_format = new StringFormat();
            string_format.Alignment = StringAlignment.Center;

            gfxFrame.RotateTransform(-90);
            gfxFrame.DrawString(axis_label_y, font_axis_labels, new SolidBrush(Color.Black), new Point(-(panel1.Height-data_pad_bot) /2, 1), string_format);
            gfxFrame.ResetTransform();

            gfxFrame.DrawString("Time (ms)", font_axis_labels, new SolidBrush(Color.Black), new Point(panel1.Width/2,panel1.Height-hScrollBar1.Height-16), string_format);

            // DRAW AXIS LINES
            Pen pen = new Pen(System.Drawing.Color.Black);
            Pen penGrid = new Pen(System.Drawing.Color.LightGray);

            // horizontal axis
            int axis_horiz_pos = panel1.Height - data_pad_bot;
            gfxFrame.DrawLine(pen, new Point(data_pad_left, axis_horiz_pos), new Point(panel1.Width, axis_horiz_pos));
            string_format.Alignment = StringAlignment.Center;
            for (int x=-panel1.Width; x<=panel1.Width; x += 50)
            {
                int X = x + (int)hScrollBar1.Value*5;
                if (X < data_pad_left) continue;
                if (X > panel1.Width) continue;
                gfxFrame.DrawLine(pen, new Point(X, axis_horiz_pos), new Point(X, axis_horiz_pos+3));
                gfxFrame.DrawLine(penGrid, new Point(X, 0), new Point(X, axis_horiz_pos));
                string s = string.Format("{0}", X);
                gfxFrame.DrawString(s, font_axis_labels, new SolidBrush(Color.Black), new Point(X, axis_horiz_pos + 8), string_format);
            }

            // vertical axis
            gfxFrame.DrawLine(pen, new Point(data_pad_left-1, 0), new Point(data_pad_left-1, panel1.Height - data_pad_bot));
            string_format.Alignment = StringAlignment.Far;
            for (int y=-panel1.Height; y<=panel1.Height; y+= 50)
            {
                int Y = y + (int)vScrollBar1.Value * 3;
                if (Y < 0) continue;
                if (Y > panel1.Height - data_pad_bot) continue;
                gfxFrame.DrawLine(pen, new Point(data_pad_left - 4, Y), new Point(data_pad_left - 1, Y));
                gfxFrame.DrawLine(penGrid, new Point(data_pad_left, Y), new Point(panel1.Width, Y));
                string s = string.Format("{0}", Y);
                gfxFrame.DrawString(s, font_axis_labels, new SolidBrush(Color.Black), new Point(data_pad_left - 4, Y - 8), string_format);
            }

            // DRAW AXIS TICK MARKS

            // CLEAN UP
            //pictureBox1.Image = bitmap;
            panel1.BackgroundImage = bitmap;
            gfxFrame.Dispose();
        }

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            layout(null, null);
        }

        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            layout(null, null);
        }
    }
}
