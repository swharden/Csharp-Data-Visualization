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
        private void PlotInitialize()
        {
            if (dataFft != null)
            {
                scottPlotUC1.plt.Clear();
                double fftSpacing = (double)wvin.WaveFormat.SampleRate / dataFft.Length;
                scottPlotUC1.plt.PlotSignal(dataFft, sampleRate: fftSpacing, markerSize: 0);
                scottPlotUC1.plt.PlotHLine(0, color: Color.Black, lineWidth: 1);
                scottPlotUC1.plt.YLabel("Power");
                scottPlotUC1.plt.XLabel("Frequency (kHz)");
                scottPlotUC1.Render();
            }
        } 

        private NAudio.Wave.WaveInEvent wvin;
        private void AudioMonitorInitialize(
                int DeviceIndex, int sampleRate = 32_000, 
                int bitRate = 16, int channels = 1, 
                int bufferMilliseconds = 50, bool start = true
            )
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

        Int16[] dataPcm;
        private void OnDataAvailable(object sender, NAudio.Wave.WaveInEventArgs args)
        {
            int bytesPerSample = wvin.WaveFormat.BitsPerSample / 8;
            int samplesRecorded = args.BytesRecorded / bytesPerSample;
            if (dataPcm == null)
                dataPcm = new Int16[samplesRecorded];
            for (int i = 0; i < samplesRecorded; i++)
                dataPcm[i] = BitConverter.ToInt16(args.Buffer, i * bytesPerSample);
        }

        double[] dataFft;

        private void updateFFT()
        {
            // the PCM size to be analyzed with FFT must be a power of 2
            int fftPoints = 2;
            while (fftPoints * 2 <= dataPcm.Length)
                fftPoints *= 2;

            // apply a Hamming window function as we load the FFT array then calculate the FFT
            NAudio.Dsp.Complex[] fftFull = new NAudio.Dsp.Complex[fftPoints];
            for (int i = 0; i < fftPoints; i++)
                fftFull[i].X = (float)(dataPcm[i] * NAudio.Dsp.FastFourierTransform.HammingWindow(i, fftPoints));
            NAudio.Dsp.FastFourierTransform.FFT(true, (int)Math.Log(fftPoints, 2.0), fftFull);

            // copy the complex values into the double array that will be plotted
            if (dataFft == null)
                dataFft = new double[fftPoints / 2];
            for (int i = 0; i < fftPoints / 2; i++)
            {
                double fftLeft = Math.Abs(fftFull[i].X + fftFull[i].Y);
                double fftRight = Math.Abs(fftFull[fftPoints - i - 1].X + fftFull[fftPoints - i - 1].Y);
                dataFft[i] = fftLeft + fftRight;
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
            if (dataPcm == null)
                return;

            updateFFT();

            if (scottPlotUC1.plt.GetPlottables().Count == 0)
                PlotInitialize();

            if (cbAutoAxis.Checked)
            {
                scottPlotUC1.plt.AxisAuto();
                scottPlotUC1.plt.TightenLayout();
            }
            scottPlotUC1.Render();
        }
    }
}
