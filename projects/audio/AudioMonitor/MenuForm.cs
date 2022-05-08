using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AudioMonitor
{
    public partial class MenuForm : Form
    {
        public MenuForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new AudioMonitorForm().ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new FftMonitorForm().ShowDialog();
        }
    }
}
