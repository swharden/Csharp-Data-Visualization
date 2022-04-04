using FFMpegCore.Pipes;
using SkiaSharp;

namespace VideoMaker;

internal class SKBitmapFrame : IVideoFrame
{
    public int Width { get; private set; }
    public int Height { get; private set; }
    public string Format => "bgra";
    private readonly byte[] Bytes;

    public SKBitmapFrame(SKBitmap bmp)
    {
        if (bmp.ColorType != SKColorType.Bgra8888)
            throw new NotImplementedException("only 'bgra' color type is supported");
        Width = bmp.Width;
        Height = bmp.Height;
        Bytes = bmp.Bytes;
    }

    public void Serialize(Stream stream) => stream.Write(Bytes);

    public async Task SerializeAsync(Stream stream, CancellationToken token) =>
        await stream.WriteAsync(Bytes, token).ConfigureAwait(false);
}
