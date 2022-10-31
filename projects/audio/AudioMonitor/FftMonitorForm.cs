using NAudio.CoreAudioApi;
using NAudio.Wave;

namespace AudioMonitor;

public partial class FftMonitorForm : Form
{
    readonly double[] AudioValues;

    readonly WasapiCapture AudioDevice;
    readonly double[] FftValues;

    public FftMonitorForm(WasapiCapture audioDevice)
    {
        InitializeComponent();
        AudioDevice = audioDevice;
        WaveFormat fmt = audioDevice.WaveFormat;

        AudioValues = new double[fmt.SampleRate / 10];
        double[] paddedAudio = FftSharp.Pad.ZeroPad(AudioValues);
        double[] fftMag = FftSharp.Transform.FFTpower(paddedAudio);
        FftValues = new double[fftMag.Length];
        double fftPeriod = FftSharp.Transform.FFTfreqPeriod(fmt.SampleRate, fftMag.Length);

        formsPlot1.Plot.AddSignal(FftValues, 1.0 / fftPeriod);
        formsPlot1.Plot.YLabel("Spectral Power");
        formsPlot1.Plot.XLabel("Frequency (kHz)");
        formsPlot1.Plot.Title($"{fmt.Encoding} ({fmt.BitsPerSample}-bit) {fmt.SampleRate} KHz");
        formsPlot1.Plot.SetAxisLimits(0, 6000, 0, .005);
        formsPlot1.Refresh();

        AudioDevice.DataAvailable += WaveIn_DataAvailable;
        AudioDevice.StartRecording();

        FormClosed += FftMonitorForm_FormClosed;
    }

    private void FftMonitorForm_FormClosed(object? sender, FormClosedEventArgs e)
    {
        System.Diagnostics.Debug.WriteLine($"Closing audio device: {AudioDevice}");
        AudioDevice.StopRecording();
        AudioDevice.Dispose();
    }

    private void WaveIn_DataAvailable(object? sender, WaveInEventArgs e)
    {
        int bytesPerSamplePerChannel = AudioDevice.WaveFormat.BitsPerSample / 8;
        int bytesPerSample = bytesPerSamplePerChannel * AudioDevice.WaveFormat.Channels;
        int bufferSampleCount = e.Buffer.Length / bytesPerSample;

        if (bufferSampleCount >= AudioValues.Length)
        {
            bufferSampleCount = AudioValues.Length;
        }

        if (bytesPerSamplePerChannel == 2 && AudioDevice.WaveFormat.Encoding == WaveFormatEncoding.Pcm)
        {
            for (int i = 0; i < bufferSampleCount; i++)
                AudioValues[i] = BitConverter.ToInt16(e.Buffer, i * bytesPerSample);
        }
        else if (bytesPerSamplePerChannel == 4 && AudioDevice.WaveFormat.Encoding == WaveFormatEncoding.Pcm)
        {
            for (int i = 0; i < bufferSampleCount; i++)
                AudioValues[i] = BitConverter.ToInt32(e.Buffer, i * bytesPerSample);
        }
        else if (bytesPerSamplePerChannel == 4 && AudioDevice.WaveFormat.Encoding == WaveFormatEncoding.IeeeFloat)
        {
            for (int i = 0; i < bufferSampleCount; i++)
                AudioValues[i] = BitConverter.ToSingle(e.Buffer, i * bytesPerSample);
        }
        else
        {
            throw new NotSupportedException(AudioDevice.WaveFormat.ToString());
        }
    }

    private void timer1_Tick(object sender, EventArgs e)
    {
        double[] paddedAudio = FftSharp.Pad.ZeroPad(AudioValues);
        double[] fftMag = FftSharp.Transform.FFTmagnitude(paddedAudio);
        Array.Copy(fftMag, FftValues, fftMag.Length);

        // find the frequency peak
        int peakIndex = 0;
        for (int i = 0; i < fftMag.Length; i++)
        {
            if (fftMag[i] > fftMag[peakIndex])
                peakIndex = i;
        }
        double fftPeriod = FftSharp.Transform.FFTfreqPeriod(AudioDevice.WaveFormat.SampleRate, fftMag.Length);
        double peakFrequency = fftPeriod * peakIndex;
        label1.Text = $"Peak Frequency: {peakFrequency:N0} Hz";

        // request a redraw using a non-blocking render queue
        formsPlot1.RefreshRequest();
    }
}
