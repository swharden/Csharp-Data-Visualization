using Microsoft.Maui.Graphics;
using Microsoft.Maui.Graphics.Skia;

namespace MauiFft;

public partial class Form1 : Form
{
    double[]? LastFft;
    double MaxFft = 1;
    readonly FftProcessor FftProc = new();

    public Form1()
    {
        InitializeComponent();
    }

    private void skglControl1_PaintSurface(object sender, SkiaSharp.Views.Desktop.SKPaintGLSurfaceEventArgs e)
    {
        if (LastFft is null)
            return;

        float width = skglControl1.Width;
        float height = skglControl1.Height;

        ICanvas canvas = new SkiaCanvas() { Canvas = e.Surface.Canvas };
        canvas.FillColor = Microsoft.Maui.Graphics.Color.FromArgb("#003366");
        canvas.FillRectangle(0, 0, width, height);

        double lastFftMax = LastFft.Max();
        MaxFft = Math.Max(MaxFft, lastFftMax);
        float[] ys = LastFft.Select(x => (float)(x / MaxFft) * skglControl1.Height).ToArray();
        float[] xs = Enumerable.Range(0, ys.Length).Select(x => (float)x / ys.Length * skglControl1.Width).ToArray();
        var points = LastFft.Select((mag, i) => new Microsoft.Maui.Graphics.PointF(xs[i], skglControl1.Height - ys[i])).ToArray();

        canvas.StrokeColor = Colors.White;
        for (int i = 0; i < points.Length - 1; i++)
            canvas.DrawLine(points[i], points[i + 1]);
    }

    private void timer1_Tick(object sender, EventArgs e)
    {
        double[]? fft = FftProc.GetFft();
        if (fft is not null)
        {
            LastFft = fft;
            skglControl1.Invalidate();
        }
    }
}
