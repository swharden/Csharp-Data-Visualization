using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MSChartDemo
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var frm = new FormBar())
                frm.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (var frm = new FormScatter())
                frm.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (var frm = new FormLine())
                frm.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            using (var frm = new FormLineFast())
                frm.ShowDialog();
        }
    }
}
