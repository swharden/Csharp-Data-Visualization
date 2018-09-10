using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScottPlotMicrophoneFFT
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SetupGraphLabels();
        }

        public void SetupGraphLabels()
        {
            scottPlotUC1.fig.labelTitle = "Microphone Amplitude";
            scottPlotUC1.fig.labelY = "Amplitude (PCM)";
            scottPlotUC1.fig.labelX = "Time (ms)";

            scottPlotUC2.fig.labelTitle = "Microphone Frequency";
            scottPlotUC2.fig.labelY = "Power (raw)";
            scottPlotUC1.fig.labelX = "Time (ms)";
        }
    }
}
