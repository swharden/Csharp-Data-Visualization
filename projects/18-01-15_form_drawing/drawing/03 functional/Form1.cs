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
            mouse_track_this_control(panel_dataView);
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









        // ############
        // ZOOM BUTTONS
        // ############

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







        // ##########################
        // SCROLL BAR MOUSE DETECTION
        // ##########################

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {

        }

        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {

        }

















        // ###########################
        // MARKER WINDOW DRAWING MARKS
        // ###########################

        private int marker_selected = 0;

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



            for (int i = 0; i < scottPlot.markers_px.Length; i++)
            {

                // coordinates for an upside-down triangle
                int xPixel = scottPlot.markers_px[i];
                Point[] triangle_points = {
                new Point(xPixel - 8, 0),
                new Point(xPixel + 8, 0),
                new Point(xPixel, 12) };

                // default colors
                Brush brush_triangle = new SolidBrush(Color.Gray);
                Brush brush_label = new SolidBrush(Color.Black);
                Pen pen = new Pen(Color.Black, 1);

                if (marker_selected - 1 == i)
                {
                    brush_triangle = new SolidBrush(Color.Red);
                    brush_label = new SolidBrush(Color.Red);
                }

                // draw the 
                gfx_markers.FillPolygon(brush_triangle, triangle_points);
                gfx_markers.DrawPolygon(pen, triangle_points);
                int xOffset = 12 * (int)((i % 2 - .5) * 2); // -12 for markers 1 and 3, +12 for markers 2 and 4
                gfx_markers.DrawString((i + 1).ToString(), font, brush_label, new Point(xPixel + xOffset, -2), stringFormat);

            }

            pb_markers.Image = bmp_markers;
        }






        // ################################
        // MOUSE MOVEMENT AND CLICK ACTIONS
        // ################################


        private Point mouse_position_down;
        private Point mouse_position_up;
        private Point mouse_position;
        private Rectangle mouse_selection_rectangle;

        private int mouse_grabbed_marker;

        private void MouseTracker_info()
        {
            string msg = $"POSITION: {mouse_position.X}, {mouse_position.Y}\n";
            msg += $"MOUSE DOWN: {mouse_position_down.X}, {mouse_position_down.Y}\n";
            msg += $"MOUSE UP: {mouse_position_up.X}, {mouse_position_up.Y}\n";
            msg += $"SELECTION: {mouse_selection_rectangle.ToString()}\n";
            richTextBox1.Text = msg;
        }
        
        private Rectangle Rectangle_rectify (Rectangle rect)
        {
            Rectangle rect2 = new Rectangle();
            rect2.X = Math.Min(rect.X, rect.X + rect.Width);
            rect2.Y = Math.Min(rect.Y, rect.Y + rect.Height);
            rect2.Width = Math.Abs(rect.Width);
            rect2.Height = Math.Abs(rect.Height);
            return rect2;
        }

        // detect if the mouse is hovered over something

        private bool Mouse_in_horizontal_zoom_area()
        {
            return ((mouse_position.X > pb_graph.Location.X + 40) && 
                    (mouse_position.Y > pb_graph.Location.Y + pb_graph.Height - 5));
        }


        private bool Mouse_in_vertical_zoom_area()
        {
            return ((mouse_position.X < pb_graph.Location.X + 43) &&
                    (mouse_position.X > pb_graph.Location.X + 20) &&
                    (mouse_position.Y < pb_graph.Height - 3) &&
                    (mouse_position.Y > 14)
                    );
        }

        private int Mouse_over_marker()
        {
            if (mouse_position.Y > 13) return 0;

            for (int i=scottPlot.markers_px.Length-1; i>=0; i--)
            {
                int relative_position = mouse_position.X - pb_graph.Location.X - 3;
                if (Math.Abs(relative_position - scottPlot.markers_px[i]) < 10) return i+1;
            }
            return 0;
        }

        private void MouseTracker_move(object sender, MouseEventArgs e)
        {
            // find absolute cusor position on the screen
            mouse_position = new Point(Cursor.Position.X, Cursor.Position.Y);

            // subtract-out the location of the panel we consider (0, 0)
            mouse_position.X -= this.PointToScreen(panel_dataView.Location).X;
            mouse_position.Y -= this.PointToScreen(panel_dataView.Location).Y;

            // if the left button is down, consider it a drag
            if (System.Windows.Forms.Control.MouseButtons == MouseButtons.Left)
            {
                int dX = mouse_position.X - mouse_position_down.X;
                int dY = mouse_position.Y - mouse_position_down.Y;
                mouse_selection_rectangle = new Rectangle(mouse_position_down.X, mouse_position_down.Y, dX, dY);
                mouse_selection_rectangle = Rectangle_rectify(mouse_selection_rectangle);

                // account for dragging a marker
                if (mouse_grabbed_marker > 0)
                {
                    scottPlot.markers_px[mouse_grabbed_marker-1] = mouse_position.X - pb_markers.Location.X;
                    dataView_redraw_markers();
                    dataView_redraw_graph();
                }

            }

            // debugging message
            MouseTracker_info();

            // change cursor based on what we are hovering over
            if (Mouse_in_vertical_zoom_area())
            {
                pb_graph.Cursor = Cursors.NoMoveVert;
                pb_markers.Cursor = Cursors.NoMoveVert;
            }
            else if (Mouse_in_horizontal_zoom_area())
            {
                pb_graph.Cursor = Cursors.NoMoveHoriz;
                pb_markers.Cursor = Cursors.NoMoveHoriz;
            }
            else if (Mouse_over_marker() > 0)
            {
                pb_graph.Cursor = Cursors.SizeWE;
                pb_markers.Cursor = Cursors.SizeWE;
            }
            else {
                pb_graph.Cursor = Cursors.Arrow;
                pb_markers.Cursor = Cursors.Arrow;
            }


        }


        

        private void MouseTracker_down(object sender, MouseEventArgs e)
        {
            // update position information
            mouse_position_down = mouse_position;
            mouse_position_up = mouse_position;
            mouse_grabbed_marker = Mouse_over_marker();

            if (mouse_grabbed_marker > 0)
            {
                marker_selected = mouse_grabbed_marker;
                scottPlot.MarkerSet(marker_selected, mouse_position.X - pb_markers.Location.X, true);
                dataView_redraw_markers();
                dataView_redraw_graph();
            }

            MouseTracker_info();
        }
        private void MouseTracker_up(object sender, MouseEventArgs e)
        {
            // update position information
            mouse_position_up = new Point(mouse_position.X, mouse_position.Y);
            MouseTracker_info();
        }

        private void mouse_track_this_control(Control control)
        {
            control.MouseMove += MouseTracker_move;
            control.MouseDown += MouseTracker_down;
            control.MouseUp += MouseTracker_up;
            System.Console.WriteLine($"Connecting mouse tracker to {control.Name}");
            foreach (Control control_child in control.Controls)
            {
                mouse_track_this_control(control_child);
            }

        }

    }
}
