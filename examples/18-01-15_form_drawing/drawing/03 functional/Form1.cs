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

        private System.Diagnostics.Stopwatch stopwatch = System.Diagnostics.Stopwatch.StartNew();

        private void dataView_redraw_graph()
        {
            // seems like a good time to update our scrollbar positions and sizes
            dataView_update_scrollbars();

            this.stopwatch.Reset();
            this.stopwatch.Start();
            System.Console.WriteLine(dataValues.Count);

            if (display_sweeps_stacked)
            {
                int nSweeps = dataValues.Count / display_sweep_length;

                // manually define limits since they're not intuitive
                scottPlot.axis_limit_X1 = 0;
                scottPlot.axis_limit_X2 = 5E-05 * display_sweep_length;
                scottPlot.axis_limit_Y1 = -500;
                scottPlot.axis_limit_Y2 = 1200;

                // manually render the frame before we start
                scottPlot.Render_frame();

                // draw the data sweep by sweep
                for (int sweepNumber=0; sweepNumber < nSweeps; sweepNumber++)
                {
                    List<double> sweepData = dataValues.GetRange(sweepNumber * display_sweep_length, display_sweep_length);
                    System.Console.WriteLine($"Sweep {sweepNumber+1} first value: {sweepData[0]}");
                    scottPlot.Render_data(sweepData, 5E-05, 0, sweepNumber*5);
                }
                
                // update the image without drawing over the background
                pb_graph.Image = scottPlot.GetBitmap(false, true);

            } else
            {
                // simply render whatever is loaded in dataValues without worrying with sweeps
                scottPlot.Render_frame();
                scottPlot.Render_data(dataValues);
                pb_graph.Image = scottPlot.GetBitmap(false, true);
            }
            
            
            this.stopwatch.Stop();
        }

        private int ClampNumber(int integer, int minimum_value, int maximum_value)
        {
            return Math.Min(Math.Max(integer, minimum_value), maximum_value);
        }
        private double ClampNumber(double integer, double minimum_value, double maximum_value)
        {
            return Math.Min(Math.Max(integer, minimum_value), maximum_value);
        }

        private void dataView_update_scrollbars()
        {
            int h = (int)(scottPlot.axis_visible_frac_X * hScrollBar1.Maximum);
            hScrollBar1.LargeChange = ClampNumber(h, hScrollBar1.Maximum / 20, hScrollBar1.Maximum - 1);

            int v = (int)(scottPlot.axis_visible_frac_Y * vScrollBar1.Maximum);
            vScrollBar1.LargeChange = ClampNumber(v, vScrollBar1.Maximum / 20, vScrollBar1.Maximum - 1);

            int ideal_h = (int)(scottPlot.axis_position_frac_X * hScrollBar1.Maximum - hScrollBar1.LargeChange / 2);
            int ideal_v = (int)(scottPlot.axis_position_frac_Y * vScrollBar1.Maximum - vScrollBar1.LargeChange / 2);

            hScrollBar1.Value = ClampNumber(ideal_h, hScrollBar1.Minimum, hScrollBar1.Maximum);
            vScrollBar1.Value = ClampNumber(ideal_v, vScrollBar1.Minimum, vScrollBar1.Maximum);

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

        private double _buttom_zoom_factor = .4;

        private void btn_zoom_y_in_Click(object sender, EventArgs e)
        {
            scottPlot.Zoom(1, 1 - _buttom_zoom_factor);
            dataView_redraw_markers();
            dataView_redraw_graph();
        }

        private void btn_zoom_y_out_Click(object sender, EventArgs e)
        {
            scottPlot.Zoom(1, 1 + _buttom_zoom_factor);
            dataView_redraw_markers();
            dataView_redraw_graph();
        }

        private void btn_zoom_x_in_Click(object sender, EventArgs e)
        {
            scottPlot.Zoom(1 - _buttom_zoom_factor, 1);
            dataView_redraw_markers();
            dataView_redraw_graph();
        }

        private void btn_zoom_x_out_Click(object sender, EventArgs e)
        {
            scottPlot.Zoom(1 + _buttom_zoom_factor, 1);
            dataView_redraw_markers();
            dataView_redraw_graph();
        }








        // ##########################
        // SCROLL BAR MOUSE DETECTION
        // ##########################

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            if (System.Windows.Forms.Control.MouseButtons == MouseButtons.Left) return; // wait for user to let go before moving
            double fracX = ((double)hScrollBar1.Value) / (hScrollBar1.Maximum - hScrollBar1.LargeChange + 1);
            scottPlot.PanToFrac(fracX, scottPlot.axis_position_frac_Y);
            dataView_redraw_markers();
            dataView_redraw_graph();
        }

        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            if (System.Windows.Forms.Control.MouseButtons == MouseButtons.Left) return; // wait for user to let go before moving
            double fracY = 1 - ((double)vScrollBar1.Value) / (vScrollBar1.Maximum - vScrollBar1.LargeChange + 1);
            scottPlot.PanToFrac(scottPlot.axis_position_frac_X, fracY);
            dataView_redraw_markers();
            dataView_redraw_graph();
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
            double timeMS = this.stopwatch.ElapsedTicks * 1000 / System.Diagnostics.Stopwatch.Frequency;
            string msg = $"POSITION: {mouse_position.X}, {mouse_position.Y}\n";
            msg += $"MOUSE DOWN: {mouse_position_down.X}, {mouse_position_down.Y}\n";
            msg += $"MOUSE UP: {mouse_position_up.X}, {mouse_position_up.Y}\n";
            msg += $"SELECTION:{ mouse_selection_rectangle.X}, {mouse_selection_rectangle.Y}";
            msg += $" (size: { mouse_selection_rectangle.Width}, {mouse_selection_rectangle.Height})\n";
            msg += $"RENDERED {dataValues.Count} points in {timeMS} ms";
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

        private bool Mouse_over_markerBar()
        {
            if (mouse_position.X < pb_markers.Location.X) return false;
            if (mouse_position.X > pb_markers.Location.X + pb_markers.Width) return false;
            if (mouse_position.Y < pb_markers.Location.Y) return false;
            if (mouse_position.Y > pb_markers.Location.Y + pb_markers.Height) return false;
            return true;
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
                    scottPlot.MarkerSet(marker_selected, mouse_position.X - pb_markers.Location.X, true);
                    dataView_redraw_markers();
                    dataView_redraw_graph();
                }

                // account for zooming
                if (mouse_is_zooming_horizontally == true)
                {
                    scottPlot.mouse_zooming_X1px = mouse_position_down.X - pb_graph.Location.X - scottPlot.data_pos_left;
                    scottPlot.mouse_zooming_X2px = mouse_position.X - pb_graph.Location.X - scottPlot.data_pos_left;
                    dataView_redraw_graph();
                }
                if (mouse_is_zooming_vertically == true)
                {
                    scottPlot.mouse_zooming_Y1px = mouse_position_down.Y - pb_graph.Location.Y - 13;
                    scottPlot.mouse_zooming_Y2px = mouse_position.Y - pb_graph.Location.Y - 13;
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
        private void Autoscale_horizontal(object sender, EventArgs e)
        {
            scottPlot.AxisSet(scottPlot.axis_limit_X1, scottPlot.axis_limit_X2, scottPlot.axis_Y1, scottPlot.axis_Y2);
            dataView_redraw_markers();
            dataView_redraw_graph();
        }

        private void Autoscale_vertical(object sender, EventArgs e)
        {
            scottPlot.AxisSet(scottPlot.axis_X1, scottPlot.axis_X2, scottPlot.axis_limit_Y1, scottPlot.axis_limit_Y2);
            dataView_redraw_markers();
            dataView_redraw_graph();
        }

        private void Autoscale_all(object sender, EventArgs e)
        {
            scottPlot.AxisSet(scottPlot.axis_limit_X1, scottPlot.axis_limit_X2, scottPlot.axis_limit_Y1, scottPlot.axis_limit_Y2);
            dataView_redraw_markers();
            dataView_redraw_graph();
        }

        private bool mouse_is_zooming_vertically = false;
        private bool mouse_is_zooming_horizontally = false;

        private void MouseTracker_down(object sender, MouseEventArgs e)
        {
            // update position information
            MouseTracker_move(sender, e); // updates mouse X/Y position
            mouse_position_down = mouse_position;
            mouse_position_up = mouse_position;
            mouse_grabbed_marker = Mouse_over_marker();

            // ZOOM AREAS
            if (e.Button == MouseButtons.Left)
            {
                mouse_is_zooming_horizontally = Mouse_in_horizontal_zoom_area();
                mouse_is_zooming_vertically = Mouse_in_vertical_zoom_area();
            }

            if (e.Button == MouseButtons.Right)
            {
                if (Mouse_in_horizontal_zoom_area())
                {
                    ContextMenu contextMenu = new ContextMenu();
                    contextMenu.MenuItems.Add(new MenuItem("auto-scale horizontal", new EventHandler(Autoscale_horizontal)));
                    contextMenu.MenuItems.Add(new MenuItem("auto-scale all", new EventHandler(Autoscale_all)));
                    contextMenu.Show(panel_dataView, mouse_position);
                }
                if (Mouse_in_vertical_zoom_area())
                {
                    ContextMenu contextMenu = new ContextMenu();
                    contextMenu.MenuItems.Add(new MenuItem("auto-scale vertical", new EventHandler(Autoscale_vertical)));
                    contextMenu.MenuItems.Add(new MenuItem("auto-scale all", new EventHandler(Autoscale_all)));
                    contextMenu.Show(panel_dataView, mouse_position);
                }
            }

            // MARKERS

            if (mouse_grabbed_marker > 0 && e.Button == MouseButtons.Left)
            {
                // we need to drag/move a marker
                marker_selected = mouse_grabbed_marker;
                scottPlot.MarkerSet(marker_selected, mouse_position.X - pb_markers.Location.X, true);
                dataView_redraw_markers();
                dataView_redraw_graph();
            }

            if (mouse_grabbed_marker > 0 && e.Button == MouseButtons.Right)
            {
                // we are right-clicking a marker
                ContextMenu marker_right_click_menu = new ContextMenu();
                marker_right_click_menu.MenuItems.Add(new MenuItem($"show marker {mouse_grabbed_marker}", new EventHandler(Marker_show)));
                marker_right_click_menu.MenuItems.Add(new MenuItem($"hide marker {mouse_grabbed_marker}", new EventHandler(Marker_hide)));
                marker_right_click_menu.MenuItems.Add(new MenuItem($"set position of marker {mouse_grabbed_marker}"));
                marker_right_click_menu.Show(panel_dataView, mouse_position);
            }

            if (mouse_grabbed_marker == 0 && Mouse_over_markerBar()==true && e.Button == MouseButtons.Right)
            {
                // right-clicking the marker bar outside a marker
                ContextMenu marker_right_click_menu = new ContextMenu();
                marker_right_click_menu.MenuItems.Add(new MenuItem("bring offscreen markers here", new EventHandler(Marker_bring_marks)));
                marker_right_click_menu.MenuItems.Add(new MenuItem("reset all marker positions", new EventHandler(Marker_reset_marks)));
                marker_right_click_menu.MenuItems.Add(new MenuItem("show all markers", new EventHandler(Marker_show_all)));
                marker_right_click_menu.MenuItems.Add(new MenuItem("hide all markers", new EventHandler(Marker_hide_all)));
                marker_right_click_menu.Show(panel_dataView, mouse_position);
            }

            MouseTracker_info();
        }

        private void Marker_bring_marks(object sender, EventArgs e)
        {
            scottPlot.Marker_bring_offscreen_markers();
            dataView_redraw_markers();
            dataView_redraw_graph();
        }
        private void Marker_reset_marks(object sender, EventArgs e)
        {
            scottPlot.Marker_bring_offscreen_markers(true);
            dataView_redraw_markers();
            dataView_redraw_graph();
        }
        private void Marker_show_all(object sender, EventArgs e)
        {
            for (int i=0; i<scottPlot.markers_px.Length; i++) scottPlot.markers_visible[i] = true;
            dataView_redraw_markers();
            dataView_redraw_graph();
        }
        private void Marker_hide_all(object sender, EventArgs e)
        {
            for (int i = 0; i < scottPlot.markers_px.Length; i++) scottPlot.markers_visible[i] = false;
            dataView_redraw_markers();
            dataView_redraw_graph();
        }

        private void Marker_show(object sender, EventArgs e)
        {
            scottPlot.markers_visible[mouse_grabbed_marker - 1] = true;
            dataView_redraw_markers();
            dataView_redraw_graph();
        }

        private void Marker_hide(object sender, EventArgs e)
        {
            scottPlot.markers_visible[mouse_grabbed_marker - 1] = false;
            dataView_redraw_markers();
            dataView_redraw_graph();
        }

        private void Marker_set(object sender, EventArgs e)
        {
            throw new NotImplementedException("set marker not yet supported");
        }

        private void MouseTracker_up(object sender, MouseEventArgs e)
        {
            // update position information
            MouseTracker_move(sender, e); // updates mouse X/Y position

            // we are only interested in mouse-up events from LEFT clicks
            // if (System.Windows.Forms.Control.MouseButtons != MouseButtons.Left) return;
            
            mouse_position_up = new Point(mouse_position.X, mouse_position.Y);
            if (marker_selected > 0)
            {
                marker_selected = 0;
                dataView_redraw_markers();
            }

            // see if an axis zoom just occurred
            if (mouse_is_zooming_horizontally)
            {
                // update the axis based on where we let go
                double posX1 = scottPlot.Position_X_units_from_px(scottPlot.mouse_zooming_X1px + scottPlot.data_pos_left) ;
                double posX2 = scottPlot.Position_X_units_from_px(scottPlot.mouse_zooming_X2px + scottPlot.data_pos_left);
                scottPlot.AxisSet(Math.Min(posX1, posX2), Math.Max(posX1, posX2), scottPlot.axis_Y1, scottPlot.axis_Y2);

                // reset the zooming flags
                scottPlot.mouse_zooming_X1px = 0;
                scottPlot.mouse_zooming_X2px = 0;
                mouse_is_zooming_horizontally = false;

                // redraw the graph
                dataView_redraw_markers();
                dataView_redraw_graph();
            }

            if (mouse_is_zooming_vertically)
            {
                // update the axis based on where we let go
                double posY1 = scottPlot.Position_Y_units_from_px(scottPlot.mouse_zooming_Y1px);
                double posY2 = scottPlot.Position_Y_units_from_px(scottPlot.mouse_zooming_Y2px);
                
                scottPlot.AxisSet(scottPlot.axis_X1, scottPlot.axis_X2, Math.Min(posY1, posY2), Math.Max(posY1, posY2));

                // reset the zooming flags
                scottPlot.mouse_zooming_Y1px = 0;
                scottPlot.mouse_zooming_Y2px = 0;
                mouse_is_zooming_vertically = false;

                // redraw the graph
                dataView_redraw_markers();
                dataView_redraw_graph();
            }

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

        private List<double> dataValues = new List<double>();

        private void Data_synthesize_noisy_sine(int data_point_count = 100)
        {
            // data will be stored at this class level
            this.dataValues = new List<double>(data_point_count);

            // synthesize the data and add salt
            Random rand = new Random();
            for (int i = 0; i < data_point_count; i++)
            {
                dataValues.Add(Math.Sin(20*i / (double)data_point_count) * 5 + rand.NextDouble());
            }

            // set limits based on data
            double[] dataValuesArray = dataValues.ToArray();
            scottPlot.axis_limit_X1 = 0; // add X offset here
            scottPlot.axis_limit_X2 = 1.0 / 20000 * dataValuesArray.Length;
            scottPlot.axis_limit_Y1 = dataValuesArray.Min();
            scottPlot.axis_limit_Y2 = dataValuesArray.Max();
            double Ycenter = (scottPlot.axis_limit_Y1 + scottPlot.axis_limit_Y2) / 2;
            double Yhalf = Math.Abs(Ycenter - scottPlot.axis_limit_Y1);
            scottPlot.axis_limit_Y1 -= Yhalf;
            scottPlot.axis_limit_Y2 += Yhalf;
            
            // auto zoom to limits
            Autoscale_all(null, null);
            dataView_redraw_graph();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            display_sweeps_stacked = false;
            display_sweep_length = 0; // continuous
            Data_synthesize_noisy_sine(10_000);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            display_sweeps_stacked = false;
            display_sweep_length = 0; // continuous
            Data_synthesize_noisy_sine(60 * 60 * 20_000);
        }

        private bool display_sweeps_stacked;
        private int display_sweep_length;
        private void button2_Click(object sender, EventArgs e)
        {
            display_sweeps_stacked = true;
            display_sweep_length = 10 * 20000; // ten second sweeps
            Data_synthesize_noisy_sine(20 * 10 * 20_000);
            scottPlot.AxisSet(0, display_sweep_length, -25, 125);
            dataView_redraw_markers();
            dataView_redraw_graph();
        }
    }
}
