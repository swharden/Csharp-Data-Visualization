using FFMpegCore;
using FFMpegCore.Pipes;
using SkiaSharp;

string saveAs = Path.GetFullPath("output.webm");
var frames = CreateFrames(count: 150, width: 400, height: 300);
RawVideoPipeSource videoFramesSource = new(frames) { FrameRate = 30 };
bool success = FFMpegArguments
    .FromPipeInput(videoFramesSource)
    .OutputToFile(saveAs, overwrite: true, options => options.WithVideoCodec("libvpx-vp9"))
    .ProcessSynchronously();

Console.WriteLine($"Saved: {saveAs}");

static IEnumerable<IVideoFrame> CreateFrames(int count, int width, int height)
{
    using SKFont textFont = new(SKTypeface.FromFamilyName("consolas"), size: 32);
    using SKPaint textPaint = new(textFont) { Color = SKColors.Yellow, TextAlign = SKTextAlign.Center };
    using SKPaint rectanglePaint = new() { Color = SKColors.Green, Style = SKPaintStyle.Fill };
    SKColor backgroundColor = SKColors.Navy;

    for (int i = 0; i < count; i++)
    {
        Console.WriteLine($"\rRendering frame {i + 1} of {count}");
        using SKBitmap bmp = new(width, height);
        using SKCanvas canvas = new(bmp);
        canvas.Clear(backgroundColor);
        canvas.DrawRect(i, i, i * 2, i * 2, rectanglePaint);
        canvas.DrawText("SkiaSharp", bmp.Width / 2, bmp.Height * .4f, textPaint);
        canvas.DrawText($"Frame {i}", bmp.Width / 2, bmp.Height * .6f, textPaint);

        using SKBitmapFrame frame = new(bmp);
        yield return frame;
    }
}