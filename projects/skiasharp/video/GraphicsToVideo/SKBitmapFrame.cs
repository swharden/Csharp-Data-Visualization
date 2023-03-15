using FFMpegCore.Pipes;
using SkiaSharp;

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

    public void Dispose() => Source.Dispose();
    public void Serialize(Stream s) => s.Write(Source.Bytes);
    public async Task SerializeAsync(Stream s, CancellationToken t) => 
        await s.WriteAsync(Source.Bytes, t).ConfigureAwait(false);

}
