using Microsoft.Maui.Graphics;
using Microsoft.Maui.Graphics.Skia;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsNetApp
{
    public partial class FormBitmap : Form
    {
        readonly Random Rand = new();

        public FormBitmap()
        {
            InitializeComponent();
            Render();
        }

        private void Render()
        {
            using SkiaBitmapExportContext context = new(pictureBox1.Width, pictureBox1.Height, 1.0f);
            StandardGraphics.Render.TestImage(Rand, context.Canvas, context.Width, context.Height);

            using MemoryStream ms = new();
            context.WriteToStream(ms); // this gets really slow when the image is large
            Image img = Bitmap.FromStream(ms);

            Image original = pictureBox1.Image;
            pictureBox1.Image = img;
            original?.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Render();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            timer1.Enabled = checkBox1.Checked;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Render();
        }
    }
}
