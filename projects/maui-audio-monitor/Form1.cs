using Microsoft.Maui.Graphics;
using Microsoft.Maui.Graphics.Skia;
using System;
using System.Windows.Forms;

namespace MauiAudioMon;

public partial class Form1 : Form
{
    float LevelFraction = 0;
    int MaxPcmValue = 0;

    public Form1()
    {
        InitializeComponent();

        var waveIn = new NAudio.Wave.WaveInEvent
        {
            DeviceNumber = 0, // customize this to select your microphone device
            WaveFormat = new NAudio.Wave.WaveFormat(rate: 1000, bits: 16, channels: 1),
            BufferMilliseconds = 10
        };
        waveIn.DataAvailable += WaveIn_DataAvailable; ;
        waveIn.StartRecording();
    }

    private void WaveIn_DataAvailable(object? sender, NAudio.Wave.WaveInEventArgs e)
    {
        int latestMax = int.MinValue;
        for (int index = 0; index < e.BytesRecorded; index += 2)
        {
            int value = BitConverter.ToInt16(e.Buffer, index);
            latestMax = Math.Max(latestMax, value);
        }

        // report maximum relative to the maximum value previously seen
        MaxPcmValue = Math.Max(MaxPcmValue, latestMax);
        float fraction = (float)latestMax / MaxPcmValue;

        // basic smoothing so the level does not change too quickly
        LevelFraction += (fraction - LevelFraction) * .1f;

        skglControl1.Invalidate();
    }

    private void skglControl1_PaintSurface(object sender, SkiaSharp.Views.Desktop.SKPaintGLSurfaceEventArgs e)
    {
        float width = skglControl1.Width;
        float height = skglControl1.Height;

        ICanvas canvas = new SkiaCanvas() { Canvas = e.Surface.Canvas };
        canvas.FillColor = Color.FromArgb("#003366");
        canvas.FillRectangle(0, 0, width, height);

        canvas.FillColor = Colors.LightGreen;
        canvas.FillRectangle(0, 0, width * LevelFraction, height);
    }
}
