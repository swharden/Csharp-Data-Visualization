using FFMpegCore;
using FFMpegCore.Pipes;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoMaker.GraphicsModels;

namespace VideoMaker;

public class VideoFileMaker
{
    readonly IGraphicsModel GraphicsModel;
    public readonly int OutputFrames;
    public readonly int OutputFPS;
    public int CurrentFrame { get; private set; }
    public SKBitmap? DisplayBitmap { get; private set; }
    public string Status = "not yet started";
    private bool Abort = false;

    private DateTime LastFrameTime;
    private readonly List<double> FrameRenderTimes = new();

    public VideoFileMaker(IGraphicsModel graphicsModel, int frames, int fps = 30)
    {
        GraphicsModel = graphicsModel;
        OutputFrames = frames;
        OutputFPS = fps;
    }

    public async Task RenderAsync_WebM(string filename)
    {
        if (!filename.EndsWith(".webm"))
            throw new ArgumentException(".webm extension required");

        await RenderAsync(filename, "libvpx-vp9");
    }

    public async Task RenderAsync_X264(string filename)
    {
        if (!filename.EndsWith(".mp4"))
            throw new ArgumentException(".mp4 extension required");

        await RenderAsync(filename, "libx264");
    }

    public async Task RenderAsync_MP4(string filename)
    {
        if (!filename.EndsWith(".mp4"))
            throw new ArgumentException(".mp4 extension required");

        await RenderAsync(filename, "mpeg4");
    }

    public async Task RenderAsync(string filename, string codec)
    {
        Abort = false;
        Status = $"starting renderer";
        var videoFramesSource = new RawVideoPipeSource(CreateFrames()) { FrameRate = 30 };
        await FFMpegArguments
           .FromPipeInput(videoFramesSource)
           .OutputToFile(filename, overwrite: true, options => options.WithVideoCodec(codec))
           .ProcessAsynchronously();
    }

    public void Cancel() { Abort = true; }

    private IEnumerable<IVideoFrame> CreateFrames()
    {
        for (int i = 0; i < OutputFrames; i++)
        {
            if (Abort)
                break;

            if (i > 0)
            {
                TimeSpan frameTime = DateTime.Now - LastFrameTime;
                FrameRenderTimes.Add(frameTime.TotalMilliseconds);
                while (FrameRenderTimes.Count > 10)
                    FrameRenderTimes.RemoveAt(0);
                double meanFrameTime = FrameRenderTimes.Sum() / FrameRenderTimes.Count;
                double meanFps = 1000.0 / meanFrameTime;
                Status = $"rendering {i + 1} of {OutputFrames} ({meanFps:N3} fps)";
            }

            CurrentFrame = i + 1;
            GraphicsModel.Advance(1);
            GraphicsModel.Draw();
            DisplayBitmap = GraphicsModel.Bitmap.Copy();
            SKBitmapFrame frame = new(GraphicsModel.Bitmap);
            LastFrameTime = DateTime.Now;
            yield return frame;
        }

        Status = Abort ? "rendering cancelled" : "rendering complete";
    }
}
