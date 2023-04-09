---
title: Plot Audio FFT with C#
description: Display the frequency power spectrum of microphone audio in real time using NAudio, FftSharp, and ScottPlot
date: 2022-05-09
weight: 3
---

**This page describes how to create a Windows Forms application that plots the frequency spectrum of microphone audio in real time.** I used NAudio to capture the audio, FftSharp to calculate the power spectrum density (using the Fast Fourier Transform), and displayed the result using [ScottPlot](https://scottplot.net). The result is a simple application that displays audio frequency data in real time at a high framerate. This application also calculates and reports the peak frequency.

<img src='fft-csharp-audio-microphone.gif' class='mx-auto d-block my-5'>

> 💡 Although this page describes a Windows Forms application, ScottPlot has a matching WPF control which can be used to achieve a similar result. See the [ScottPlot WPF Quickstart](https://scottplot.net/quickstart/wpf/) guide to get started.

## Capture Audio Input

You may find these prerequisite pages useful references:

* [Getting Started with NAudio](../naudio) - how to setup NAudio to capture microphone data

*  [Realtime Audio Level Monitor](../level) - how to display an audio waveform in real time in a Windows Forms application

I begin with a top-level fixed-size array containing audio values from the latest buffer.

```cs
readonly double[] AudioValues;
```

In my program initialization I use information about the sample rate and buffer size to determine the size of the array.

```cs
int SampleRate = 44100;
int BufferMilliseconds = 20;
AudioValues = new double[SampleRate * BufferMilliseconds / 1000];
```

I also configure the NAudio WaveIn event to call a function whenever new data arrives. That function converts bytes in the buffer to audio levels which it places in the top-level fixed-size array.

```cs
NAudio.Wave.WaveInEvent Wave = new ()
{
    DeviceNumber = comboBox1.SelectedIndex,
    WaveFormat = new NAudio.Wave.WaveFormat(SampleRate, BitDepth, ChannelCount),
    BufferMilliseconds = BufferMilliseconds
};

Wave.DataAvailable += WaveIn_DataAvailable;
Wave.StartRecording();
```

```cs
void WaveIn_DataAvailable(object? sender, NAudio.Wave.WaveInEventArgs e)
{
    for (int i = 0; i < e.Buffer.Length / 2; i++)
        AudioValues[i] = BitConverter.ToInt16(e.Buffer, i * 2);
}
```

> 💡 No plotting or UI objects should be interacted with directly from this method because it may not run on the UI thread. Populating values in an already-created fixed-size array alleviates this concern.

## Plot Setup

I create a top-level fixed-size array to contain FFT values, plot the array, then later can update the values inside it and request a re-plot using a timer.

One important consideration is that FFTs must be calculated using input data whose length is a power of 2. FftSharp has a `ZeroPad()` helper method which will pad any array with zeros to achieve this. I simulate zero-padding the audio data once in my initializer, calculate its FFT, then use that length to determine what size the `FftValues` array should be fixed to.

```cs
readonly double[] FftValues;
double[] paddedAudio = FftSharp.Pad.ZeroPad(AudioValues);
double[] fftMag = FftSharp.Transform.FFTmagnitude(paddedAudio);
FftValues = new double[fftMag.Length];
```

I also use the `FFTfreqPeriod()` helper function to determine the frequency resolution of the FFT that is created. The total FFT spans a frequency between 0 and half of the sample rate (the Nyquist frequency), and this helper function just calculates the frequency spacing between each data point in that FFT.

```cs
double fftPeriod = FftSharp.Transform.FFTfreqPeriod(SampleRate, fftMag.Length);
```

I can then add the FFT array to the plot once, and simply request a refresh using a timer once the program is running.

```cs
formsPlot1.Plot.AddSignal(FftValues, 1.0 / fftPeriod);
formsPlot1.Plot.YLabel("Spectral Power");
formsPlot1.Plot.XLabel("Frequency (kHz)");
formsPlot1.Refresh();
```

## Setup the FFT Calculator

A timer is used to periodically re-calculate the FFT from the latest audio values and update the plot. The full source code at the bottom of the page demonstrates extra functionality I add here to perform peak frequency detection.

```cs
private void timer1_Tick(object sender, EventArgs e)
{
    // calculate FFT
    double[] paddedAudio = FftSharp.Pad.ZeroPad(AudioValues);
    double[] fft = FftSharp.Transform.FFTpower(paddedAudio);

    // copy FFT into top-level FFT array
    Array.Copy(fft, FftValues, fft.Length);

    // request a redraw using a non-blocking render queue
    formsPlot1.RefreshRequest();
}
```

> 💡 `FFTpower()` is log-transformed alternative to `FFTmagnitude()` which reports frequency power as Decibels (dB)

## Increase FFT Size to Increase Frequency Resolution

To increase the frequency resolution, more data must be included in the FFT. This could be achieved by increasing the sample rate, but 44100 Hz is pretty standard so let's consider alternate ways we could increase spectral resolution.

### Option 1: Increase Buffer Length

The current system creates a FFT sized according to the buffer length, so increasing the buffer size will result in more data entering the FFT calculation and improving spectral resolution. This will come at the expense of decrease frame rate though, because larger buffers means fewer buffers per second.

### Option 2: Analyze FFT Across Multiple Buffers

Increasing the amount of data entering the FFT increases the spectral resolution of the whole operation. The best way to achieve this is to have a data structure that accumulates samples across multiple buffers, then when the GUI requests an update the most recent N points are pulled from the growing accumulation of audio values that were obtained across multiple buffers. An ideal system would analyze the last N points where N is an even power of 2.

## Use a Window to Improve Accuracy

Assuming our signal is centered at 0, applying a window function can taper the edges of the input waveform so they approach 0, and the result is a reduction in the DC (low frequency) component of the FFT, and an increase in the spectral segregation properties of the waveform as well. Windowing is an important topic for anyone serious about creating FFTs from audio data, but is outside the scope of this article. See the [FftSharp windowing](https://github.com/swharden/FftSharp#windowing) page for details.

## Full Source Code

This project may be downloaded from GitHub: [AudioMonitor](https://github.com/swharden/Csharp-Data-Visualization/tree/main/projects/audio/AudioMonitor)

```cs
public partial class FftMonitorForm : Form
{
    NAudio.Wave.WaveInEvent? Wave;

    readonly double[] AudioValues;
    readonly double[] FftValues;

    readonly int SampleRate = 44100;
    readonly int BitDepth = 16;
    readonly int ChannelCount = 1;
    readonly int BufferMilliseconds = 20;

    public FftMonitorForm()
    {
        InitializeComponent();

        AudioValues = new double[SampleRate * BufferMilliseconds / 1000];
        double[] paddedAudio = FftSharp.Pad.ZeroPad(AudioValues);
        double[] fftMag = FftSharp.Transform.FFTmagnitude(paddedAudio);
        FftValues = new double[fftMag.Length];

        double fftPeriod = FftSharp.Transform.FFTfreqPeriod(SampleRate, fftMag.Length);

        formsPlot1.Plot.AddSignal(FftValues, 1.0 / fftPeriod);
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
```

## Resources
* Source code for this project: [AudioMonitor](https://github.com/swharden/Csharp-Data-Visualization/tree/main/projects/audio/AudioMonitor)
* [ScottPlot](https://ScottPlot.NET) - interactive plotting library for .NET
* [ScottPlot Cookbook](https://scottplot.net/cookbook/)
* [ScottPlot FAQ: How to plot live data](https://scottplot.net/faq/live-data/)
