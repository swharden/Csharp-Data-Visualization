using SkiaSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestSkia
{
    public partial class Form1 : Form
    {
        private SKSurface surface;
        private bool rendering = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            UpdateSurfaceFromCurrentDimensions();
        }

        private void PictureBox1_Resize(object sender, EventArgs e)
        {
            UpdateSurfaceFromCurrentDimensions();
        }

        private void UpdateSurfaceFromCurrentDimensions()
        {
            SKImageInfo info = new SKImageInfo(pictureBox1.Width, pictureBox1.Height);
            surface = SKSurface.Create(info);
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            if (rendering)
                return;
            else
                rendering = true;

            var benchmark = new Benchmark();

            if (rbText.Checked)
                pictureBox1.Image = TestImages.Text(surface, (int)nudCount.Value);
            else if (rbLines.Checked)
                pictureBox1.Image = TestImages.Lines(surface, (int)nudCount.Value);
            else
                throw new NotImplementedException();

            lblStatus.Text = benchmark.GetMessage();
            rendering = false;
        }

        private void BtnStartStop_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled)
            {
                timer1.Enabled = false;
                btnStartStop.Text = "Start";
            }
            else
            {
                timer1.Enabled = true;
                btnStartStop.Text = "Stop";
            }
        }
    }
}
