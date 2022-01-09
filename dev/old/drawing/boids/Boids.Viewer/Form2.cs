using Boids.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Boids.Viewer
{
    public partial class Form2 : Form
    {
        Field field;
        public Form2()
        {
            InitializeComponent();
            Reset();
        }

        private void pictureBox1_SizeChanged(object sender, EventArgs e) => Reset();
        private void pictureBox1_Click(object sender, EventArgs e) => Reset();
        private void Reset() => field = new Field(pictureBox1.Width, pictureBox1.Height, 100);

        private void timer1_Tick(object sender, EventArgs e)
        {
            field.Advance();
            pictureBox1.Image?.Dispose();
            pictureBox1.Image = SDRender.RenderField(field);
        }

    }
}
