using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace swharden_demo {
    public partial class Form1 : Form {

        internal ScottPlot SP = new ScottPlot();

        private void Replot() {
            SP.TimerStart();
            SP.setSize(pictureBox1.Width, pictureBox1.Height);
            SP.PlotXY();
            pictureBox1.BackgroundImage = SP.bufferGraph;
            AxisRead();
            Application.DoEvents();
            SP.TimerStop();
            statusBar.Text = SP.TimerVal();
            
        }

        private void AxisRead() {
            nudX1.Value = (decimal)SP.axisX1;
            nudX2.Value = (decimal)SP.axisX2;
            nudY1.Value = (decimal)SP.axisY1;
            nudY2.Value = (decimal)SP.axisY2;
            lblX1.Text = string.Format("{0:00.00}", SP.axisX1);
            lblX2.Text = string.Format("{0:00.00}", SP.axisX2);
            lblY1.Text = string.Format("{0:00.00}", SP.axisY1);
            lblY2.Text = string.Format("{0:00.00}", SP.axisY2);
        }

        private void AxisApply() {
            SP.axisX1 = (double)nudX1.Value;
            SP.axisX2 = (double)nudX2.Value;
            SP.axisY1 = (double)nudY1.Value;
            SP.axisY2 = (double)nudY2.Value;
            Replot();
        }

        /* ###################################
         * GUI THINGS
         * ###################################
         */

        // form load
        public Form1() {
            InitializeComponent();
        }

        // buttons for data things
        private void BtnReplot_Click(object sender, EventArgs e) {Replot();}
        private void btnDataRandom_Click(object sender, EventArgs e) {SP.CreateDataRandom((int)nudPoints.Value);Replot();}
        private void btnDataSine_Click(object sender, EventArgs e) {SP.CreateDataSine((int)nudPoints.Value);Replot();}
        private void btnAxisGet_Click(object sender, EventArgs e) {AxisRead();}
        private void btnAxisFit_Click(object sender, EventArgs e) {SP.AxisFit();Replot();}
        private void btnAxisApply_Click(object sender, EventArgs e) {AxisApply();}

        // buttons for panning
        private void btnUp_Click(object sender, EventArgs e) { SP.AxisPanPx(0, -1); Replot(); }
        private void btnDown_Click(object sender, EventArgs e) { SP.AxisPanPx(0, 1); Replot(); }
        private void btnLeft_Click(object sender, EventArgs e) { SP.AxisPanPx(1, 0); Replot(); }
        private void btnRight_Click(object sender, EventArgs e) { SP.AxisPanPx(-1, 0); Replot(); }

        // click and drag rescaling
        public bool mouseCalculating = false;
        public int mouseDownX;
        public int mouseDownY;
        public int mouseDeltaX;
        public int mouseDeltaY;

        // mouse click-drag panning and zooming
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e) {
            mouseDownX = e.X;
            mouseDownY = e.Y;
            mouseCalculating = false;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e) {
            if (mouseCalculating == true) { return; }
            if (e.Button == MouseButtons.None) { return;  }
            mouseCalculating = true;
            mouseDeltaX = mouseDownX - e.X; mouseDeltaY = e.Y - mouseDownY;
            mouseDownX = e.X; mouseDownY = e.Y;
            if (mouseDeltaX == 0 && mouseDeltaY == 0) return;
            if (e.Button == MouseButtons.Left) { SP.AxisPanPx(mouseDeltaX, mouseDeltaY); }
            if (e.Button == MouseButtons.Right) { SP.AxisZoomPx(mouseDeltaX, mouseDeltaY); }
            Replot();
            mouseCalculating = false;
        }

        // toggle quality
        private void checkBox1_CheckedChanged(object sender, EventArgs e) {
            if (checkBox1.Checked) SP.highQuality = true;
            else SP.highQuality = false;
            SP.TimerReset();
            Replot();
        }

        private void pictureBox1_SizeChanged(object sender, EventArgs e) {
            SP.TimerReset();
            Replot();
        }
    }
}
