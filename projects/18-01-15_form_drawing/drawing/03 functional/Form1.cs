using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _03_functional
{
    public partial class Form1 : Form
    {

        private ScottPlot scottPlot;

        public Form1()
        {
            InitializeComponent();
            scottPlot = new ScottPlot(pb_graph.Width, pb_graph.Height);
            dataView_update_all();
        }

        // TOP LEVEL THINGS

        private void dataView_update_all()
        {
            dataView_update_labels();
            dataView_update_scrollbars();
            dataView_redraw_markers();
            dataView_redraw_graph();
        }




        // MID LEVEL THINGS

        private void dataView_redraw_graph()
        {
            pb_graph.Image = scottPlot.Render();
        }

        private void dataView_redraw_markers()
        {
            Bitmap bmp_markers = new Bitmap(pb_markers.Width, pb_markers.Height);
            Graphics gfx_markers = Graphics.FromImage(bmp_markers);
            gfx_markers.Clear(SystemColors.Control);

            //int marker_position_1 = 10;
            //int marker_position_2 = 50;
            //int marker_position_3 = 80;
            //int marker_position_4 = 120;



            // prepare brushes and fonts
            
            Font font = new Font("arial", 9, FontStyle.Regular);
            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;

            int[] markers = { 50, 150, 200, 222 };
            int markerSelected = 3;
            
            for (int i=0; i<markers.Length;i++)
            {

                // coordinates for an upside-down triangle
                int xPixel = markers[i];
                Point[] triangle_points = {
                new Point(xPixel - 8, 0),
                new Point(xPixel + 8, 0),
                new Point(xPixel, 12) };

                // default colors
                Brush brush_triangle = new SolidBrush(Color.Gray);
                Brush brush_label = new SolidBrush(Color.Black);
                Pen pen = new Pen(Color.Black, 1);

                if (markerSelected - 1 == i)
                {
                    brush_triangle = new SolidBrush(Color.Red);
                    brush_label = new SolidBrush(Color.Red);
                }

                // draw the 
                gfx_markers.FillPolygon(brush_triangle, triangle_points);
                gfx_markers.DrawPolygon(pen, triangle_points);
                int xOffset = 12 * (int)((i % 2 - .5)*2); // -12 for markers 1 and 3, +12 for markers 2 and 4
                gfx_markers.DrawString((i+1).ToString(), font, brush_label, new Point(xPixel + xOffset, -2), stringFormat);

            }

            pb_markers.Image = bmp_markers;
        }

        private void dataView_update_scrollbars()
        {
            hScrollBar1.Value = 50;
            vScrollBar1.Value = 50;
        }

        private void dataView_update_labels()
        {
            lbl_axis_x.Text = "Time (seconds)";
            lbl_axis_y.Text = "Units";
            lbl_sweep.Text = "Sweep 1 of 10";
        }






        // EVENT BINDINGS

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (scottPlot == null) return;
            scottPlot.Resize(pb_graph.Width, pb_graph.Height);
            dataView_update_all();
        }

        private void btn_zoom_y_in_Click(object sender, EventArgs e)
        {

        }

        private void btn_zoom_y_out_Click(object sender, EventArgs e)
        {

        }

        private void btn_zoom_x_out_Click(object sender, EventArgs e)
        {

        }

        private void btn_zoom_x_in_Click(object sender, EventArgs e)
        {

        }

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {

        }

        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {

        }

        private void pb_graph_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void pb_graph_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void pb_graph_MouseUp(object sender, MouseEventArgs e)
        {

        }


    }
}
