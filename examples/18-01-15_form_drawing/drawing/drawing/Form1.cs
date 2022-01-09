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

        private static ScottPlot scottPlot;

        public Form1()
        {
            
            InitializeComponent();

            // prevent mouseover events until it's fully loaded
            panel1.Visible = false;
            pic_selection.Visible = false;

            // last minute GUI customizations
            pic_selection.BackColor = Color.FromArgb(100, 255, 0, 0);

            // doing this reduced panel flickering by double buffering
            // TIP: pictureboxes are double-buffered already
            typeof(Panel).InvokeMember("DoubleBuffered", System.Reflection.BindingFlags.SetProperty | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic, null, panel1, new object[] { true });
            
        }

        private void timer_init_Tick(object sender, EventArgs e)
        {
            timer_init.Enabled = false;
            scottPlot = new ScottPlot(panel1.Width, panel1.Height);
            layout(null, null);
            panel1.Visible = true;
        }

        /// <summary>
        /// Resize the scrollbars and move the buttons according to the window size.
        /// </summary>
        private void Resize_controls()
        {
            int pad_bot = 70;
            int pad_left = 100;

            btn_zoom_in_y.Location = new Point(30, (panel1.Height - pad_bot) / 2 - btn_zoom_in_y.Height);
            btn_zoom_out_y.Location = new Point(30, (panel1.Height - pad_bot) / 2);

            btn_zoom_out_x.Location = new Point(0, panel1.Height - pad_bot + 2);
            btn_zoom_in_x.Location = new Point(btn_zoom_in_x.Width, panel1.Height - pad_bot + 2);

            vScrollBar1.Location = new Point(panel1.Width - vScrollBar1.Width, 0);
            vScrollBar1.Height = panel1.Height - pad_bot;
            //vScrollBar1.Enabled = false;
            hScrollBar1.Location = new Point(pad_left, panel1.Height - hScrollBar1.Height);
            hScrollBar1.Width = panel1.Width - pad_left;
            //hScrollBar1.Enabled = false;
        }

        /// <summary>
        /// Call this whenever the window resizes. It will move/resize controls and the ScottPlot.
        /// </summary>
        private void layout(object sender, EventArgs e)
        {
            Resize_controls();
            Graph_Redraw();
        }

        /// <summary>
        /// given a number, return it ensuring it is between two limits
        /// </summary>
        private int ClampNumber(int integer, int minimum_value, int maximum_value) {
            integer = Math.Max(integer, minimum_value);
            integer = Math.Min(integer, maximum_value);
            return integer;
        }

        private void Graph_Redraw(bool resetScrollBar=true)
        {
            if (scottPlot == null) return;
            scottPlot.Resize(panel1.Width, panel1.Height);
            panel1.BackgroundImage = scottPlot.Render();

            if (resetScrollBar)
            {

                int h = (int)(scottPlot.axis_visible_frac_X * hScrollBar1.Maximum);
                hScrollBar1.LargeChange = ClampNumber(h, hScrollBar1.Maximum / 20, hScrollBar1.Maximum-1);

                int v = (int)(scottPlot.axis_visible_frac_Y * vScrollBar1.Maximum);
                vScrollBar1.LargeChange = ClampNumber(v, vScrollBar1.Maximum / 20, vScrollBar1.Maximum - 1);

                int ideal_h = (int)(scottPlot.axis_position_frac_X * hScrollBar1.Maximum - hScrollBar1.LargeChange/2);
                int ideal_v = (int)(scottPlot.axis_position_frac_Y * vScrollBar1.Maximum - vScrollBar1.LargeChange/2);

                hScrollBar1.Value = ClampNumber(ideal_h, hScrollBar1.Minimum, hScrollBar1.Maximum);
                vScrollBar1.Value = ClampNumber(ideal_v, vScrollBar1.Minimum, vScrollBar1.Maximum);


            }

        }

        private void ScollBarMoved()
        {
            
            double fracY = 1 - ((double)vScrollBar1.Value) / (vScrollBar1.Maximum - vScrollBar1.LargeChange + 1);
            double fracX = ((double)hScrollBar1.Value) / (hScrollBar1.Maximum - hScrollBar1.LargeChange + 1);

            scottPlot.PanToFrac(fracX, fracY);
            
            /*
            
            double frac_upper = 1 - ((double)(vScrollBar1.Value)) / vScrollBar1.Maximum;
            double frac_lower = 1 - ((double)(vScrollBar1.Value + vScrollBar1.LargeChange - 1)) / vScrollBar1.Maximum;
            
            double frac_left = ((double)(hScrollBar1.Value)) / hScrollBar1.Maximum;
            double frac_right = ((double)(hScrollBar1.Value + hScrollBar1.LargeChange - 1)) / hScrollBar1.Maximum;

            double offsetX = scottPlot.axis_limit_X1;
            double offsetY = scottPlot.axis_limit_Y1;
            scottPlot.AxisSet(rangeX * frac_left + offsetX, 
                              rangeX * frac_right + offsetX, 
                              rangeY * frac_lower + offsetY, 
                              rangeY * frac_upper + offsetY);
            */

            Graph_Redraw(false);

        }

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            if (System.Windows.Forms.Control.MouseButtons == MouseButtons.Left) return;
            ScollBarMoved();
        }

        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            if (System.Windows.Forms.Control.MouseButtons == MouseButtons.Left) return;
            ScollBarMoved();
        }


        // click zoom buttons to zoom in and out

        private double _buttom_zoom_factor = .2;

        private void btn_zoom_in_y_Click(object sender, EventArgs e)
        {
            scottPlot.Zoom(1, 1 - _buttom_zoom_factor);
            Graph_Redraw();
        }

        private void btn_zoom_out_y_Click(object sender, EventArgs e)
        {
            scottPlot.Zoom(1, 1 + _buttom_zoom_factor);
            Graph_Redraw();
        }

        private void btn_zoom_in_x_Click(object sender, EventArgs e)
        {
            scottPlot.Zoom(1 - _buttom_zoom_factor, 1);
            Graph_Redraw();
        }

        private void btn_zoom_out_x_Click(object sender, EventArgs e)
        {
            scottPlot.Zoom(1 + _buttom_zoom_factor, 1);
            Graph_Redraw();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //scottPlot.Pan(-1,1);
            scottPlot.PanTo(222, 333);
            Graph_Redraw();
        }

        // when MouseUp event is called, this moves focus to an invisible button
        private void Focus_Reset(object sender, MouseEventArgs e)
        {
            button1.Focus();
        }


        /*
         * 
         * MOUSE TRACKING
         * 
         * 
         */

        private Point Mouse_down_point;
        private Point Mouse_up_point;

        private Rectangle Mouse_rectangle(Point p1, Point p2)
        {
            int X1 = Math.Min(p1.X, p2.X);
            int X2 = Math.Max(p1.X, p2.X);
            int Y1 = Math.Min(p1.Y, p2.Y);
            int Y2 = Math.Max(p1.Y, p2.Y);

            Rectangle rect = new Rectangle(X1, Y1, X2-X1, Y2-Y1);
            return rect;
        }

        private bool Mouse_in_zoom_horizontal(Point pt)
        {
            if (pt.X >= scottPlot.data_pos_left &&
                pt.X <= scottPlot.data_pos_right &&
                pt.Y >= scottPlot.data_pos_bottom &&
                pt.Y <= scottPlot.data_pos_bottom + 30)
            {
                return true;
            } else {
                return false;
            }
        }

        private bool Mouse_in_zoom_vertical(Point pt)
        {
            if (pt.X >= scottPlot.data_pos_left - 30 &&
                pt.X <= scottPlot.data_pos_left &&
                pt.Y >= scottPlot.data_pos_top &&
                pt.Y <= scottPlot.data_pos_bottom)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            // rubber band rectangle
            if (System.Windows.Forms.Control.MouseButtons == MouseButtons.Left)
            {

                Rectangle mouseRect = Mouse_rectangle(Mouse_down_point, e.Location);

                if (Mouse_in_zoom_horizontal(Mouse_down_point))
                {
                    panel1.Cursor = Cursors.NoMoveHoriz;
                    int x1 = mouseRect.X;
                    int x2 = mouseRect.X + mouseRect.Width;
                    int y1 = scottPlot.data_pos_bottom;
                    int y2 = scottPlot.data_pos_bottom + 7;
                    pic_selection.Location = new Point(x1, y1);
                    pic_selection.Size = new Size(x2 - x1, y2 - y1);
                }
                else if (Mouse_in_zoom_vertical(Mouse_down_point))
                {
                    panel1.Cursor = Cursors.NoMoveVert;
                    int x1 = scottPlot.data_pos_left - 7;
                    int x2 = scottPlot.data_pos_left;
                    int y1 = mouseRect.Y;
                    int y2 = mouseRect.Y + mouseRect.Height;
                    pic_selection.Location = new Point(x1, y1);
                    pic_selection.Size = new Size(x2 - x1, y2 - y1);
                }
            } else
            {
                // left button is not pressed
                if (Mouse_in_zoom_horizontal(e.Location)) panel1.Cursor = Cursors.NoMoveHoriz;
                else if (Mouse_in_zoom_vertical(e.Location)) panel1.Cursor = Cursors.NoMoveVert;
                else panel1.Cursor = Cursors.Arrow;
            }
        }

        
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            Mouse_down_point = new Point(e.X, e.Y);
            pic_selection.Visible = true;
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            Mouse_up_point = new Point(e.X, e.Y);
            pic_selection.Visible = false;
        }
    }
}
