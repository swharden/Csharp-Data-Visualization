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
            
            // doing this reduced panel flickering by double buffering
            typeof(Panel).InvokeMember("DoubleBuffered", System.Reflection.BindingFlags.SetProperty | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic, null, panel1, new object[] { true });
            
        }

        private void timer_init_Tick(object sender, EventArgs e)
        {
            timer_init.Enabled = false;
            scottPlot = new ScottPlot(panel1.Width, panel1.Height);
            layout(null, null);
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
            vScrollBar1.Enabled = false;
            hScrollBar1.Location = new Point(pad_left, panel1.Height - hScrollBar1.Height);
            hScrollBar1.Width = panel1.Width - pad_left;
            hScrollBar1.Enabled = false;
        }

        /// <summary>
        /// Call this whenever the window resizes. It will move/resize controls and the ScottPlot.
        /// </summary>
        private void layout(object sender, EventArgs e)
        {
            Resize_controls();
            Graph_Redraw();
        }

        private void Graph_Redraw()
        {
            if (scottPlot == null) return;
            scottPlot.Resize(panel1.Width, panel1.Height);
            panel1.BackgroundImage = scottPlot.Render();
        }


        
        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            Graph_Redraw();
        }

        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            Graph_Redraw();
        }


        // click zoom buttons to zoom in and out

        private double _buttom_zoom_factor = .3;

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
    }
}
