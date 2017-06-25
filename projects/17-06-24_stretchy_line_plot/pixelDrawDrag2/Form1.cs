using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic; // so we can use lists

namespace pixelDrawDrag2
{
    public partial class Form1 : Form
    {
        public List<int> listTimes = new List<int>();
        internal ScottPlot SP = new ScottPlot();

        public Form1()
        {
            InitializeComponent();
            pictureBox1.Image = SP.bufferGraph;
            resize(null, new EventArgs());
        }

        private void Form1_Load(object sender, EventArgs e) {

        }
        
        private void pictureBox1_Click(object sender, EventArgs e) {

        }

        private void label1_Click(object sender, EventArgs e) {

        }

        private void panel1_Paint(object sender, PaintEventArgs e) {

        }

        private void timer1_Tick(object sender, EventArgs e) {
            SP.TimerStart();
            SP.Roll();
            SP.PlotXY();
            pictureBox1.Image = SP.bufferGraph;
            SP.TimerStop();
            richTextBox1.Text = SP.TimerVal();
        }

        private void resize(object sender, EventArgs e) {
            SP.pxWidth = pictureBox1.Width;
            SP.pxHeight = pictureBox1.Height;
            SP.InitBitmap();
        }
    }
}
