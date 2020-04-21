using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Starfield.WinForms
{
    public partial class Form1 : Form
    {
        readonly Field field = new Field(500);

        public Form1()
        {
            InitializeComponent();
        }

        Stopwatch stopwatch = new Stopwatch();
        private void timer1_Tick(object sender, EventArgs e)
        {
            stopwatch.Restart();
            field.Advance();
            Bitmap bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            byte alpha = (byte)(trackBar1.Value * 255 / 100);
            Color starColor = Color.FromArgb(alpha, Color.White);
            field.Render(bmp, starColor);
            pictureBox1.Image?.Dispose();
            pictureBox1.Image = bmp;
            double elapsedSec = (double)stopwatch.ElapsedTicks / Stopwatch.Frequency;
            Text = $"Starfield in Windows Forms - {elapsedSec * 1000:0.00} ms ({1 / elapsedSec:0.00} FPS)";
        }

        private void rb500_CheckedChanged(object sender, EventArgs e)
        {
            field.Reset(500);
        }

        private void rb100k_CheckedChanged(object sender, EventArgs e)
        {
            field.Reset(100_000);
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            label1.Text = $"{trackBar1.Value}%";
        }
    }
}
