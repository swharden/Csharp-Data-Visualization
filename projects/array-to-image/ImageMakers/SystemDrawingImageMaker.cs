using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;

namespace ArrayToImage.ImageMakers;

public class SystemDrawingImageMaker : IGraphicsPlatform
{
    public string Name => "System.Drawing";

    public void SaveImageRgb(string filePath, byte[,,] pixelArray)
    {
        filePath = Path.GetFullPath(filePath);
        using Bitmap bmp = GetBitmap(pixelArray);
        bmp.Save(filePath);
        Console.WriteLine(filePath);
    }

    public byte[,,] LoadImageRgb(string filePath)
    {
        using Bitmap bmp = new(filePath);
        int bytesPerPixel = Image.GetPixelFormatSize(bmp.PixelFormat) / 8;
        Rectangle rect = new(0, 0, bmp.Width, bmp.Height);
        BitmapData bmpData = bmp.LockBits(rect, ImageLockMode.ReadWrite, bmp.PixelFormat);
        int byteCount = Math.Abs(bmpData.Stride) * bmp.Height;
        byte[] bytes = new byte[byteCount];
        Marshal.Copy(bmpData.Scan0, bytes, 0, byteCount);
        bmp.UnlockBits(bmpData);

        byte[,,] pixelValues = new byte[bmp.Height, bmp.Width, 3];
        for (int y = 0; y < bmp.Height; y++)
        {
            for (int x = 0; x < bmp.Width; x++)
            {
                int offset = (y * bmpData.Stride) + x * bytesPerPixel;
                pixelValues[y, x, 0] = bytes[offset + 2]; // red
                pixelValues[y, x, 1] = bytes[offset + 1]; // green
                pixelValues[y, x, 2] = bytes[offset + 0]; // blue
            }
        }

        return pixelValues;
    }

    private static Bitmap GetBitmap(byte[,,] pixelArray)
    {
        int width = pixelArray.GetLength(1);
        int height = pixelArray.GetLength(0);
        int stride = (width % 4 == 0) ? width : width + 4 - width % 4;
        int bytesPerPixel = 3;

        byte[] bytes = new byte[stride * height * bytesPerPixel];
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                int offset = (y * stride + x) * bytesPerPixel;
                bytes[offset + 0] = pixelArray[y, x, 2]; // blue
                bytes[offset + 1] = pixelArray[y, x, 1]; // green
                bytes[offset + 2] = pixelArray[y, x, 0]; // red
            }
        }

        PixelFormat formatOutput = PixelFormat.Format24bppRgb;
        Rectangle rect = new(0, 0, width, height);
        Bitmap bmp = new(stride, height, formatOutput);
        BitmapData bmpData = bmp.LockBits(rect, ImageLockMode.ReadOnly, formatOutput);
        Marshal.Copy(bytes, 0, bmpData.Scan0, bytes.Length);
        bmp.UnlockBits(bmpData);

        Bitmap bmp2 = new(width, height, PixelFormat.Format32bppPArgb);
        Graphics gfx2 = Graphics.FromImage(bmp2);
        gfx2.DrawImage(bmp, 0, 0);

        return bmp2;
    }
}
