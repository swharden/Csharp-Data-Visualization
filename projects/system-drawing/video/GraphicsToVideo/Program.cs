using FFMpegCore;
using FFMpegCore.Pipes;
using FFMpegCore.Extensions.System.Drawing.Common;

// mpeg4 (.mp4) worked in media player but not chrome
// libx264 for (.mp4) but in chrome but not media player
// libvpx-vp9 for (.webm) worked everywhere

string outputPath = Path.GetFullPath("output.webm");

var frames = CreateFramesSD(200, 400, 300);
RawVideoPipeSource source = new(frames) { FrameRate = 30 };
var success = FFMpegArguments
    .FromPipeInput(source)
    .OutputToFile(outputPath, overwrite: true, options => options.WithVideoCodec("libvpx-vp9"))
    .ProcessSynchronously();

Console.WriteLine($"Saved: {outputPath}");

static IEnumerable<BitmapVideoFrameWrapper> CreateFramesSD(int count, int w, int h)
{
    for (int i = 0; i < count; i++)
    {
        Console.CursorLeft = 0;
        Console.Write($"Encoding: frame {i + 1} of {count} ...");
        if (i == count - 1)
            Console.WriteLine("");

        using System.Drawing.Bitmap bmp = new(w, h, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
        using System.Drawing.Graphics gfx = System.Drawing.Graphics.FromImage(bmp);
        gfx.Clear(System.Drawing.Color.Navy);

        System.Drawing.Point pt = new(i, i);
        System.Drawing.Size sz = new(i, i);
        System.Drawing.Rectangle rect = new(pt, sz);
        gfx.FillRectangle(System.Drawing.Brushes.Green, rect);

        using System.Drawing.Font fnt = new("consolas", 24);
        gfx.DrawString($"Frame: {i + 1:N0}", fnt, System.Drawing.Brushes.Yellow, 2, 2);

        using BitmapVideoFrameWrapper wrappedBitmap = new(bmp);
        yield return wrappedBitmap;
    }
}