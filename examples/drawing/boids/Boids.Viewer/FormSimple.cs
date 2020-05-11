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
    public partial class FormSimple : Form
    {
        readonly Model.Field field;

        public FormSimple()
        {
            InitializeComponent();
            field = new Model.Field(pictureBox1.Width, pictureBox1.Height);
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            field.Advance(pictureBox1.Width, pictureBox1.Height);
            pictureBox1.Image = SDRender.RenderField(field);
        }
    }
}
