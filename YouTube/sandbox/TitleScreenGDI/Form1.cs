using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TitleScreenGDI
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            btnRedraw_Click(null, null);
        }

        Animations.CirclesAndLines stuff;
        Bitmap bmp;

        private void btnRedraw_Click(object sender, EventArgs e)
        {
            stuff = new Animations.CirclesAndLines(pictureBox1.Width, pictureBox1.Height, 10);
            bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            pictureBox1.Image = Animations.RenderGDI.Render(stuff, bmp);
        }

        private void btnIterate_Click(object sender, EventArgs e)
        {
            stuff.Iterate(5);
            pictureBox1.Image = Animations.RenderGDI.Render(stuff, bmp);
        }

        private void cbRun_CheckedChanged(object sender, EventArgs e)
        {
            timer1.Enabled = cbRun.Checked;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            btnIterate_Click(null, null);
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            btnRedraw_Click(null, null);
        }
    }
}
