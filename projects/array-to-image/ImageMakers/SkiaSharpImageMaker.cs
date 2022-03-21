using SkiaSharp;
using System;
using System.IO;
using System.Runtime.InteropServices;

namespace ArrayToImage.ImageMakers;

internal class SkiaSharpImageMaker : IGraphicsPlatform
{
    public string Name => "SkiaSharp";

    public void SaveImageRgb(string filePath, byte[,,] pixelArray)
    {
        filePath = Path.GetFullPath(filePath);
        SKBitmap bmp = GetBitmap(pixelArray);
        using FileStream fs = new(filePath, FileMode.Create);
        bmp.Encode(fs, SKEncodedImageFormat.Png, quality: 100);
        Console.WriteLine(filePath);
    }

    public byte[,,] LoadImageRgb(string filePath)
    {
        SKBitmap bmp = SKBitmap.Decode(filePath);

        ReadOnlySpan<byte> spn = bmp.GetPixelSpan();

        byte[,,] pixelValues = new byte[bmp.Height, bmp.Width, 3];
        for (int y = 0; y < bmp.Height; y++)
        {
            for (int x = 0; x < bmp.Width; x++)
            {
                int offset = (y * bmp.Width + x) * bmp.BytesPerPixel;
                pixelValues[y, x, 0] = spn[offset + 2];
                pixelValues[y, x, 1] = spn[offset + 1];
                pixelValues[y, x, 2] = spn[offset + 0];
            }
        }

        return pixelValues;
    }

    private static SKBitmap GetBitmap(byte[,,] pixelArray)
    {
        int width = pixelArray.GetLength(1);
        int height = pixelArray.GetLength(0);

        uint[] pixelValues = new uint[width * height];
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                byte alpha = 255;
                byte red = pixelArray[y, x, 0];
                byte green = pixelArray[y, x, 1];
                byte blue = pixelArray[y, x, 2];
                uint pixelValue = (uint)red + (uint)(green << 8) + (uint)(blue << 16) + (uint)(alpha << 24);
                pixelValues[y * width + x] = pixelValue;
            }
        }

        SKBitmap bitmap = new();
        GCHandle gcHandle = GCHandle.Alloc(pixelValues, GCHandleType.Pinned);
        SKImageInfo info = new(width, height, SKColorType.Rgba8888, SKAlphaType.Premul);

        IntPtr ptr = gcHandle.AddrOfPinnedObject();
        int rowBytes = info.RowBytes;
        bitmap.InstallPixels(info, ptr, rowBytes, delegate { gcHandle.Free(); });

        return bitmap;
    }

    [Obsolete("For education only, too slow to be practically useful")]
    private static SKBitmap GetBitmapSLOW(byte[,,] pixelArray)
    {
        int width = pixelArray.GetLength(1);
        int height = pixelArray.GetLength(0);

        SKBitmap bitmap = new(width, height);

        for (int y = 0; y < height; y++)
        {
            Console.WriteLine($"Row {y}");
            for (int x = 0; x < width; x++)
            {
                byte r = pixelArray[y, x, 0];
                byte g = pixelArray[y, x, 1];
                byte b = pixelArray[y, x, 2];
                bitmap.Pixels[y * width + x] = new SKColor(r, g, b);
            }
        }

        return bitmap;
    }
}
