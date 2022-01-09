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

        private double[] amplitudes;
        private void PlotInitialize(int pointCount = 500)
        {
            amplitudes = new double[pointCount];
            scottPlotUC1.plt.Clear();
            scottPlotUC1.plt.PlotSignal(amplitudes, sampleRate: 1000.0 / 20, markerSize: 0);
            scottPlotUC1.plt.PlotVLine(0, color: Color.Red, lineWidth: 2);
            scottPlotUC1.plt.PlotHLine(0, color: Color.Black, lineWidth: 2);
            scottPlotUC1.plt.YLabel("Amplitude (%)");
            scottPlotUC1.plt.XLabel("Time (seconds)");
            scottPlotUC1.Render();
        }

        private void PlotAddPoint(double value)
        {
            int amplitudesIndex = buffersRead%amplitudes.Length;
            amplitudes[amplitudesIndex] = value;
        }

        private NAudio.Wave.WaveInEvent wvin;
        private int buffersRead = -1;
        private double peakAmplitudeSeen = 0;
        private void OnDataAvailable(object sender, NAudio.Wave.WaveInEventArgs args)
        {
            int bytesPerSample = wvin.WaveFormat.BitsPerSample / 8;
            int samplesRecorded = args.BytesRecorded / bytesPerSample;
            Int16[] lastBuffer = new Int16[samplesRecorded];
            for (int i = 0; i < samplesRecorded; i++)
                lastBuffer[i] = BitConverter.ToInt16(args.Buffer, i * bytesPerSample);
            int lastBufferAmplitude = lastBuffer.Max() - lastBuffer.Min();
            double amplitude = (double)lastBufferAmplitude / Math.Pow(2, wvin.WaveFormat.BitsPerSample);
            if (amplitude > peakAmplitudeSeen)
                peakAmplitudeSeen = amplitude;
            amplitude = amplitude / peakAmplitudeSeen * 100;
            buffersRead += 1;

            // TODO: make this sane
            ScottPlot.PlottableAxLine axLine = (ScottPlot.PlottableAxLine)scottPlotUC1.plt.GetPlottables()[1];
            axLine.position = (buffersRead % amplitudes.Length) * 20.0 / 1000.0;

            Console.WriteLine(string.Format("Buffer {0:000} amplitude: {1:00.00}%", buffersRead, amplitude));
            PlotAddPoint(amplitude);
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
                scottPlotUC1.plt.Axis(null, null, -5, null);
            }
            scottPlotUC1.Render();
        }
    }
}
