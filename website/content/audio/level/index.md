---
title: Plot Audio Waveform with C#
description: Display microphone audio in real time using NAudio and ScottPlot
date: 2022-05-08
weight: 2
---

**This page describes how I created a Windows Forms application to plot microphone audio in real time.** I used NAudio to capture the audio, and as new data arrived from the audio input device I converted its values to `double` and stored them in a `readonly double[]` used for plotting with [ScottPlot](https://scottplot.net). The result is a simple application that displays audio data in real time at a high framerate.

<img src='winforms-audio-level-meter.gif' class='mx-auto d-block my-5'>

> 💡 Although this page describes a Windows Forms application, ScottPlot has a matching WPF control which can be used to achieve a similar result. See the [ScottPlot WPF Quickstart](https://scottplot.net/quickstart/wpf/) guide to get started.

### Prepare an Array to Store Audio Data

I started by creating an array the class level which will hold the values to be plotted. This array has been designed to hold the exact number of audio values that arrive from a single recorded buffer.

```cs
readonly double[] AudioValues;
```

The length of the array can be calculated from the recording parameters I chose:

```cs
AudioValues = new double[SampleRate * BufferMilliseconds / 1000];
```

### Setup the Plot

I add the audio value array to the plot even though it contains all zeros now, but later I will populate the array with real data and simply request the plot to refresh itself. I'm using ScottPlot's _Signal Plot_ type which is performance-optimized to display evenly-spaced data. See the [ScottPlot Cookbook](https://scottplot.net/cookbook/) for details.

```cs
formsPlot1.Plot.AddSignal(AudioValues);
```

### List and Activate Recording Devices

When the program starts, a ComboBox is populated with recording devices:

```cs
for (int i = 0; i < NAudio.Wave.WaveIn.DeviceCount; i++)
{
    var caps = NAudio.Wave.WaveIn.GetCapabilities(i);
    comboBox1.Items.Add(caps.ProductName);
}
```

> 💡 NAudio considers `-1` a valid device ID. It typically maps to the Microsoft Sound Mapper, but I often omit it for simplicity.

Selecting a recording device activates it. If a device was previously selected, that one is unloaded first.

```cs
int SampleRate = 44100;
int BitDepth = 16;
int ChannelCount = 1;
int BufferMilliseconds = 20;

NAudio.Wave.WaveInEvent? Wave;

private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
{
    if (Wave is not null)
    {
        Wave.StopRecording();
        Wave.Dispose();
    }

    Wave = new NAudio.Wave.WaveInEvent()
    {
        DeviceNumber = comboBox1.SelectedIndex,
        WaveFormat = new NAudio.Wave.WaveFormat(SampleRate, BitDepth, ChannelCount),
        BufferMilliseconds = BufferMilliseconds
    };

    Wave.DataAvailable += WaveIn_DataAvailable;
    Wave.StartRecording();
}
```

### Update Values when New Data is Available

Since functions in the `DataAvailable` event handler may be invoked on threads other than the UI thread, it is important not to make any UI updates from these functions. Instead, this function translates the bytes from the buffer into values that are stored in the array we created when the Form was constructed.

```cs
void WaveIn_DataAvailable(object? sender, NAudio.Wave.WaveInEventArgs e)
{
    for (int i = 0; i < e.Buffer.Length / 2; i++)
        AudioValues[i] = BitConverter.ToInt16(e.Buffer, i * 2);
}
```

### Update the UI with a Timer

I configured a `Timer` to run every 20 milliseconds which inspects data in the `AudioValues` array and updates the level meter and the plot.

```cs
private void timer1_Tick(object sender, EventArgs e)
{
    int level = (int)AudioValues.Max();
    pbVolume.Value = (int)Math.Max(level, pbVolume.Maximum);
    formsPlot1.RefreshRequest();
}
```

> 💡 ScottPlot's `RefreshRequest()` is a non-blocking alternative to `Refresh()` designed to keep UI windows interactive during redraws.

## Full Source Code

The extended source code shown here has a few additional features (such as automatic scaling of plots) that was not described in the code samples above. The source code here is what was used to create the animated screenshot at the top of the page. Full project source code is available for download on GitHub: [AudioMonitor](https://github.com/swharden/Csharp-Data-Visualization/tree/main/projects/audio/AudioMonitor)

```cs
public partial class Form1 : Form
{
    NAudio.Wave.WaveInEvent? Wave;

    readonly double[] AudioValues;

    int SampleRate = 44100;
    int BitDepth = 16;
    int ChannelCount = 1;
    int BufferMilliseconds = 20;

    public Form1()
    {
        InitializeComponent();

        AudioValues = new double[SampleRate * BufferMilliseconds / 1000];

        formsPlot1.Plot.AddSignal(AudioValues, SampleRate / 1000);
        formsPlot1.Plot.YLabel("Level");
        formsPlot1.Plot.XLabel("Time (milliseconds)");
        formsPlot1.Refresh();
    }

    private void Form1_Load(object sender, EventArgs e)
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
}
```

## Resources
* Source code for this project: [AudioMonitor](https://github.com/swharden/Csharp-Data-Visualization/tree/main/projects/audio/AudioMonitor)
* [ScottPlot](https://ScottPlot.NET) - interactive plotting library for .NET
* [ScottPlot Cookbook](https://scottplot.net/cookbook/)
* [ScottPlot FAQ: How to plot live data](https://scottplot.net/faq/live-data/)