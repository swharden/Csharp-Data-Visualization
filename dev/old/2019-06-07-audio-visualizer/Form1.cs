using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AudioPeak
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            ScanSoundCards();
            PlotInitialize();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void ScanSoundCards()
        {
            cbDevice.Items.Clear();
            for (int i = 0; i < NAudio.Wave.WaveIn.DeviceCount; i++)
                cbDevice.Items.Add(NAudio.Wave.WaveIn.GetCapabilities(i).ProductName);
            if (cbDevice.Items.Count > 0)
                cbDevice.SelectedIndex = 0;
            else
                MessageBox.Show("ERROR: no recording devices available");
        }

        private double[] pcmValues;
        private void PlotInitialize(int pointCount = 8_000 * 10)
        {
            pcmValues = new double[pointCount];
            scottPlotUC1.plt.Clear();
            scottPlotUC1.plt.PlotSignal(pcmValues, sampleRate: 8000, markerSize: 0);
            scottPlotUC1.plt.PlotVLine(0, color: Color.Red, lineWidth: 2);
            scottPlotUC1.plt.PlotHLine(0, color: Color.Black, lineWidth: 1);
            scottPlotUC1.plt.YLabel("Amplitude (%)");
            scottPlotUC1.plt.XLabel("Time (seconds)");
            scottPlotUC1.Render();
        }

        private NAudio.Wave.WaveInEvent wvin;
        private int buffersRead = 0;
        private void OnDataAvailable(object sender, NAudio.Wave.WaveInEventArgs args)
        {
            int bytesPerSample = wvin.WaveFormat.BitsPerSample / 8;
            int samplesRecorded = args.BytesRecorded / bytesPerSample;
            int buffersToDisplay = 80_000 / samplesRecorded;
            int offset = (buffersRead % buffersToDisplay) * samplesRecorded;
            for (int i = 0; i < samplesRecorded; i++)
                pcmValues[i + offset] = BitConverter.ToInt16(args.Buffer, i * bytesPerSample);

            buffersRead += 1;

            // TODO: make this sane
            ScottPlot.PlottableAxLine axLine = (ScottPlot.PlottableAxLine)scottPlotUC1.plt.GetPlottables()[1];
            axLine.position = offset / 8000.0;
        }

        private void AudioMonitorInitialize(int DeviceIndex, int sampleRate = 8000, int bitRate = 16,
                int channels = 1, int bufferMilliseconds = 20, bool start = true)
        {
            if (wvin == null)
            {
                wvin = new NAudio.Wave.WaveInEvent();
                wvin.DeviceNumber = DeviceIndex;
                wvin.WaveFormat = new NAudio.Wave.WaveFormat(sampleRate, bitRate, channels);
                wvin.DataAvailable += OnDataAvailable;
                wvin.BufferMilliseconds = bufferMilliseconds;
                if (start)
                    wvin.StartRecording();
            }
        }


        private void BtnStart_Click(object sender, EventArgs e)
        {
            AudioMonitorInitialize(cbDevice.SelectedIndex);
        }

        private void BtnStop_Click(object sender, EventArgs e)
        {
            if (wvin != null)
            {
                wvin.StopRecording();
                wvin = null;
            }
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            if (cbAutoAxis.Checked)
            {
                scottPlotUC1.plt.AxisAuto();
                scottPlotUC1.plt.TightenLayout();
            }
            scottPlotUC1.Render();
        }
    }
}
