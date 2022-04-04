using FFMpegCore.Pipes;
using SkiaSharp;
using VideoMaker.GraphicsModels;

namespace VideoMaker;

public partial class Form1 : Form
{
    readonly VideoFileMaker Maker;

    public Form1()
    {
        InitializeComponent();
        IGraphicsModel myModel = new BallField(640, 480, 100);
        Maker = new VideoFileMaker(myModel, 1000);
        progressBar1.Maximum = Maker.OutputFrames;
    }

    private void timer1_Tick(object sender, EventArgs e)
    {
        skglControl1.Invalidate();
        lblStatus.Text = Maker.Status;
        progressBar1.Value = Maker.CurrentFrame;
    }

    private void skglControl1_PaintSurface(object sender, SkiaSharp.Views.Desktop.SKPaintGLSurfaceEventArgs e)
    {
        SKBitmap? displayBitmap = Maker.DisplayBitmap;
        if (displayBitmap is not null)
            e.Surface.Canvas.DrawBitmap(displayBitmap, 0, 0);
    }

    private async void button2_Click(object sender, EventArgs e)
    {
        lblStatus.Text = $"starting renderer";
        Application.DoEvents();
        await Maker.RenderAsync_WebM("test.webm");
    }

    private void button1_Click(object sender, EventArgs e)
    {
        Maker.Cancel();
    }
}
