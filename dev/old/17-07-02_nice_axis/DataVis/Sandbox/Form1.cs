using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sandbox
{
    public partial class Form1 : Form
    {
        ScottPlot2.ScottPlot SP = new ScottPlot2.ScottPlot();
        ScottPlot2.Generate SPgen = new ScottPlot2.Generate();

        // this is where we will store our data
        private List<double> Xs;
        private List<double> Ys;

        public Form1()
        {
            InitializeComponent();
            //GraphResize();
            //GraphDraw();
        }

        // call when RESIZING the figure
        public void GraphResize()
        {
            SP.SetSize(pictureBox1.Width, pictureBox1.Height); // update plot dimensions
            GraphDraw(); // draw the graph
        }

        // call to REPLOT the figure (i.e., after a view or axis change)
        public void GraphDraw()
        {
            if (SP.dataSizeX == 0) GraphResize();
            SP.stopwatch.Restart(); // start the stopwatch
            SP.ClearData(); // clear the graph entirely
            SP.DrawGrid(); // make a line grid
            SP.AddLine(Xs, Ys); // plot the points stored in Xs and Ys
            pictureBox1.BackgroundImage = SP.Render(); // render the axis+graph
            this.Refresh(); // force the window to redraw
            richTextBox1.Text = SP.Info(); // update the textbox info
        }

        private void btnResize_Click(object sender, EventArgs e)
        {
            GraphResize();
        }

        private void btnDraw_Click(object sender, EventArgs e)
        {
            GraphDraw();
        }

        private void btnRandom_Click(object sender, EventArgs e)
        {
            Xs = SPgen.Sequence((int)numericUpDown1.Value);
            Ys = SPgen.Random((int)numericUpDown1.Value);
            SP.AX.Auto(Xs, Ys);
            GraphDraw();
        }

        private void btnSine_Click(object sender, EventArgs e)
        {
            Xs = SPgen.Sequence((int)numericUpDown1.Value);
            Ys = SPgen.Sine((int)numericUpDown1.Value);
            SP.AX.Auto(Xs, Ys);
            GraphDraw();
        }

        private void btnRandom2_Click(object sender, EventArgs e)
        {
            Xs = SPgen.Random((int)numericUpDown1.Value);
            Ys = SPgen.Random((int)numericUpDown1.Value);
            SP.AX.Auto(Xs, Ys);
            GraphDraw();
        }

        private void btnAutoAxis_Click(object sender, EventArgs e)
        {
            SP.AX.Auto(Xs, Ys);
            GraphDraw();
        }

        /* MOUSE CLICK-AND-DRAG FOR PANNING AND ZOOMING */

        internal int mouseDownX, mouseDownY;
        internal double mouseDownAxisX1, mouseDownAxisY1, mouseDownAxisX2, mouseDownAxisY2;


        internal double mouseDownAxisXunitsPerPx, mouseDownAxisYunitsPerPx;

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDownX = e.X;
            mouseDownY = e.Y;
            double[] axis = SP.AX.GetAxis();
            mouseDownAxisX1 = axis[0];
            mouseDownAxisX2 = axis[1];
            mouseDownAxisY1 = axis[2];
            mouseDownAxisY2 = axis[3];
            mouseDownAxisXunitsPerPx = SP.AX.UnitsPerPxX;
            mouseDownAxisYunitsPerPx = SP.AX.UnitsPerPxY;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.None) return;
            double dX = (e.X - mouseDownX) ;
            double dY = (e.Y - mouseDownY) ;
            if (e.Button == MouseButtons.Left)
            {
                SP.AX.SetAxis(mouseDownAxisX1 - dX * mouseDownAxisXunitsPerPx,
                              mouseDownAxisX2 - dX * mouseDownAxisXunitsPerPx,
                              mouseDownAxisY1 - dY * mouseDownAxisYunitsPerPx,
                              mouseDownAxisY2 - dY * mouseDownAxisYunitsPerPx);
                GraphDraw();
            }
            if (e.Button == MouseButtons.Right)
            {
                // todo: directional zooming
                //double centerX = (mouseDownX / SP.figureWidth);
                //double centerY = (1 - (mouseDownY / SP.figureHeight));

                // I trial-and-error found a math equation that gives me smooth mouse zooming in (decreasing sensitivity with distance)
                if (dX > 0) dX = Math.Sqrt(50 * Math.Abs(dX) * Math.Pow(.02, Math.Abs(dX) / 10000)) * dX / Math.Abs(dX);
                if (dY < 0) dY = Math.Sqrt(50 * Math.Abs(dY) * Math.Pow(.02, Math.Abs(dY) / 10000)) * dY / Math.Abs(dY);

                // apply this temporary axis
                SP.AX.SetAxis(mouseDownAxisX1 + dX * mouseDownAxisXunitsPerPx, 
                              mouseDownAxisX2 - dX * mouseDownAxisXunitsPerPx, 
                              mouseDownAxisY1 - dY * mouseDownAxisYunitsPerPx, 
                              mouseDownAxisY2 + dY * mouseDownAxisYunitsPerPx);
                GraphDraw();
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            // todo: apply view
        }
    }
}


