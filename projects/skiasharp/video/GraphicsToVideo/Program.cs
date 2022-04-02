using FFMpegCore;
using FFMpegCore.Pipes;
using SkiaSharp;

namespace GraphicsToVideo;

public static class Program
{
    public static void Main()
    {
        string outputPath = Path.GetFullPath("output.webm");
        if (File.Exists(outputPath))
            File.Delete(outputPath);

        var frames = CreateFrames(count: 150, width: 400, height: 300);
        var videoFramesSource = new RawVideoPipeSource(frames) { FrameRate = 30 };
        var success = FFMpegArguments
            .FromPipeInput(videoFramesSource)
            .OutputToFile("output.webm", overwrite: true, options => options.WithVideoCodec("libvpx-vp9"))
            .ProcessSynchronously();

        Console.WriteLine($"Saved: {outputPath}");
    }

    static IEnumerable<IVideoFrame> CreateFrames(int count, int width, int height)
    {
        using SKFont textFont = new(SKTypeface.FromFamilyName("consolas"), size: 32);
        using SKPaint textPaint = new(textFont) { Color = SKColors.Yellow, TextAlign = SKTextAlign.Center };
        using SKPaint rectanglePaint = new() { Color = SKColors.Green, Style = SKPaintStyle.Fill };
        SKColor backgroundColor = SKColors.Navy;

        for (int i = 0; i < count; i++)
        {
            Console.WriteLine($"Rendering frame {i + 1} of {count}");
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
}