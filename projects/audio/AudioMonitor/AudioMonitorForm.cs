using NAudio.CoreAudioApi;
using NAudio.Wave;

namespace AudioMonitor;

public partial class AudioMonitorForm : Form
{
    NAudio.Wave.WaveInEvent? Wave;
    NAudio.Wave.WasapiLoopbackCapture? CaptureDevice = null;

    readonly double[] AudioValues;

    readonly int SampleRate = 44100;
    readonly int BitDepth = 16;
    readonly int ChannelCount = 1;
    readonly int BufferMilliseconds = 20;

    public AudioMonitorForm()
    {
        InitializeComponent();

        AudioValues = new double[SampleRate * BufferMilliseconds / 1000];

        formsPlot1.Plot.AddSignal(AudioValues, SampleRate / 1000);
        formsPlot1.Plot.YLabel("Level");
        formsPlot1.Plot.XLabel("Time (milliseconds)");
        formsPlot1.Render();
    }

    private void Form1_Load(object sender, EventArgs e)
    {
        for (int i = 0; i < NAudio.Wave.WaveIn.DeviceCount; i++)
        {
            var caps = NAudio.Wave.WaveIn.GetCapabilities(i);
            comboBox1.Items.Add(caps.ProductName);
        }


        #region Output devices
        
        var enumerator = new MMDeviceEnumerator();
        foreach (var wasapi in enumerator.EnumerateAudioEndPoints(DataFlow.Render, DeviceState.Active))
        {
            comboBox1.Items.Add(wasapi.FriendlyName);
        }

        #endregion Output devices
    }

    private void StopOutputDeviceRecording()
    {
        if (CaptureDevice is not null)
        {
            CaptureDevice.StopRecording();
            CaptureDevice.Dispose();
            CaptureDevice = null;

            for (int i = 0; i < AudioValues.Length; i++)
                AudioValues[i] = 0;
            formsPlot1.Plot.AxisAuto();
        }
    }

    private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
    {
        StopOutputDeviceRecording();

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

        if (comboBox1.SelectedIndex < NAudio.Wave.WaveIn.DeviceCount)
        {
            Wave = new NAudio.Wave.WaveInEvent()
            {
                DeviceNumber = comboBox1.SelectedIndex,
                WaveFormat = new NAudio.Wave.WaveFormat(SampleRate, BitDepth, ChannelCount),
                BufferMilliseconds = BufferMilliseconds
            };

            Wave.DataAvailable += WaveIn_DataAvailable;
            Wave.StartRecording();
        }
        else
        {
            #region Output devices

            var enumerator = new MMDeviceEnumerator();
            int selectedIndex = NAudio.Wave.WaveIn.DeviceCount;
            foreach (var wasapi in enumerator.EnumerateAudioEndPoints(DataFlow.Render, DeviceState.Active))
            {
                if (comboBox1.SelectedIndex == selectedIndex)
                {
                    MMDevice loopbackCaptureDevice = wasapi;
                    CaptureDevice = new WasapiLoopbackCapture(loopbackCaptureDevice);
                    CaptureDevice.DataAvailable += WaveIn_DataAvailable;
                    CaptureDevice.StartRecording();
                    break;
                }
                ++selectedIndex;
            }

            #endregion Output devices
        }

        formsPlot1.Plot.Title(comboBox1.SelectedItem.ToString());
    }

    void WaveIn_DataAvailable(object? sender, NAudio.Wave.WaveInEventArgs e)
    {
        for (int i = 0; i < e.Buffer.Length / 2; i++)
        {
            if (i < AudioValues.Length)
            {
                AudioValues[i] = BitConverter.ToInt16(e.Buffer, i * 2);
            }
        }
    }

    private void timer1_Tick(object sender, EventArgs e)
    {
        try
        {
            int level = (int)AudioValues.Max();

            // auto-scale the maximum progressbar level
            if (level > pbVolume.Maximum)
                pbVolume.Maximum = level;
            pbVolume.Value = level;

            // auto-scale the plot Y axis limits
            var currentLimits = formsPlot1.Plot.GetAxisLimits();
            formsPlot1.Plot.SetAxisLimits(
                yMin: Math.Min(currentLimits.YMin, -level),
                yMax: Math.Max(currentLimits.YMax, level));

            // request a redraw using a non-blocking render queue
            formsPlot1.RefreshRequest();
        }
        catch (Exception)
        {
            //handle hardware muting the mic
        }
    }

    private void AudioMonitorForm_FormClosed(object sender, FormClosedEventArgs e)
    {
        StopOutputDeviceRecording();
    }
}
