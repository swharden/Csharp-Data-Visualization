using FFMpegCore.Pipes;
using SkiaSharp;

namespace GraphicsToVideo;

internal class SKBitmapFrame : IVideoFrame, IDisposable
{
    public int Width => Source.Width;
    public int Height => Source.Height;
    public string Format => "bgra";

    private readonly SKBitmap Source;

    public SKBitmapFrame(SKBitmap bmp)
    {
        if (bmp.ColorType != SKColorType.Bgra8888)
            throw new NotImplementedException("only 'bgra' color type is supported");

        Source = bmp;
    }

    public void Serialize(Stream stream)
    {
        stream.Write(Source.Bytes);
    }

    public async Task SerializeAsync(Stream stream, CancellationToken token)
    {
        await stream.WriteAsync(Source.Bytes, token).ConfigureAwait(false);
    }

    public void Dispose()
    {
        Source.Dispose();
    }
}
