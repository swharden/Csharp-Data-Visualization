namespace AudioMonitor;

public partial class FftMonitorForm : Form
{
    NAudio.Wave.WaveInEvent? Wave;

    readonly double[] AudioValues;
    readonly double[] FftValues;

    readonly int SampleRate = 44100;
    readonly int BitDepth = 16;
    readonly int ChannelCount = 1;
    readonly int BufferMilliseconds = 20; // increase this to increase frequency resolution

    public FftMonitorForm()
    {
        InitializeComponent();

        AudioValues = new double[SampleRate * BufferMilliseconds / 1000];
        double[] paddedAudio = FftSharp.Pad.ZeroPad(AudioValues);
        double[] fftMag = FftSharp.Transform.FFTmagnitude(paddedAudio);
        FftValues = new double[fftMag.Length];

        double fftPeriod = FftSharp.Transform.FFTfreqPeriod(SampleRate, fftMag.Length);

        formsPlot1.Plot.AddSignal(FftValues, fftPeriod);
        formsPlot1.Plot.YLabel("Spectral Power");
        formsPlot1.Plot.XLabel("Frequency (kHz)");

        formsPlot1.Refresh();
    }

    private void FftMonitorForm_Load(object sender, EventArgs e)
    {
        for (int i = 0; i < NAudio.Wave.WaveIn.DeviceCount; i++)
        {
            var caps = NAudio.Wave.WaveIn.GetCapabilities(i);
            comboBox1.Items.Add(caps.ProductName);
        }
    }

    private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Wave is not null)
        {
            Wave.StopRecording();
            Wave.Dispose();

            for (int i = 0; i < AudioValues.Length; i++)
                AudioValues[i] = 0;
            formsPlot1.Plot.AxisAuto();
        }

        if (comboBox1.SelectedIndex == -1)
            return;

        Wave = new NAudio.Wave.WaveInEvent()
        {
            DeviceNumber = comboBox1.SelectedIndex,
            WaveFormat = new NAudio.Wave.WaveFormat(SampleRate, BitDepth, ChannelCount),
            BufferMilliseconds = BufferMilliseconds
        };

        Wave.DataAvailable += WaveIn_DataAvailable;
        Wave.StartRecording();

        formsPlot1.Plot.Title(comboBox1.SelectedItem.ToString());
    }

    void WaveIn_DataAvailable(object? sender, NAudio.Wave.WaveInEventArgs e)
    {
        for (int i = 0; i < e.Buffer.Length / 2; i++)
            AudioValues[i] = BitConverter.ToInt16(e.Buffer, i * 2);
    }

    private void timer1_Tick(object sender, EventArgs e)
    {
        double[] paddedAudio = FftSharp.Pad.ZeroPad(AudioValues);
        double[] fftMag = FftSharp.Transform.FFTpower(paddedAudio);
        Array.Copy(fftMag, FftValues, fftMag.Length);

        // find the frequency peak
        int peakIndex = 0;
        for (int i = 0; i < fftMag.Length; i++)
        {
            if (fftMag[i] > fftMag[peakIndex])
                peakIndex = i;
        }
        double fftPeriod = FftSharp.Transform.FFTfreqPeriod(SampleRate, fftMag.Length);
        double peakFrequency = fftPeriod * peakIndex;
        label1.Text = $"Peak Frequency: {peakFrequency:N0} Hz";

        // auto-scale the plot Y axis limits
        double fftPeakMag = fftMag.Max();
        double plotYMax = formsPlot1.Plot.GetAxisLimits().YMax;
        formsPlot1.Plot.SetAxisLimits(
            xMin: 0,
            xMax: 6,
            yMin: 0,
            yMax: Math.Max(fftPeakMag, plotYMax));

        // request a redraw using a non-blocking render queue
        formsPlot1.RefreshRequest();
    }
}
