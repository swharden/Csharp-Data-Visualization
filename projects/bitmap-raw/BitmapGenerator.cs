using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

internal static class BitmapGenerator
{
    public static byte[] Rainbow(int width = 400, int height = 300)
    {
        RawBitmap bmp = new(width, height);

        Random rand = new();
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                byte r = (byte)(255.0 * x / width);
                byte g = (byte)(255.0 * y / height);
                byte b = (byte)(255 - 255.0 * x / width);
                RawColor color = new(r, g, b);
                bmp.SetPixel(x, y, color);
            }
        }

        return bmp.GetBitmapBytes();
    }

    public static byte[] RandomGrayscale(int width = 400, int height = 300)
    {
        RawBitmap bmp = new(width, height);

        Random rand = new();
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                byte value = (byte)rand.Next(256);
                RawColor color = new(value, value, value);
                bmp.SetPixel(x, y, color);
            }
        }

        return bmp.GetBitmapBytes();
    }

    public static byte[] RandomRGB(int width = 400, int height = 300)
    {
        RawBitmap bmp = new(width, height);

        Random rand = new();
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                bmp.SetPixel(x, y, RawColor.Random(rand));
            }
        }

        return bmp.GetBitmapBytes();
    }

    public static byte[] RandomRectangles(int width = 400, int height = 300)
    {
        RawBitmap bmp = new(width, height);

        Random rand = new();
        for (int i = 0; i < 1000; i++)
        {

            // random box dimensions and color
            int rectX = rand.Next(bmp.Width);
            int rectY = rand.Next(bmp.Height);
            int rectWidth = rand.Next(50);
            int rectHeight = rand.Next(50);
            RawColor color = RawColor.Random(rand);

            // draw the rectangle
            for (int x = rectX; x < rectX + rectWidth; x++)
            {
                for (int y = rectY; y < rectY + rectHeight; y++)
                {
                    if (x < 0 || x >= width) continue;
                    if (y < 0 || y >= height) continue;
                    bmp.SetPixel(x, y, color);
                }
            }
        }

        return bmp.GetBitmapBytes();
    }

    public static byte[] Ramps(int width = 401, int height = 307)
    {
        RawBitmap bmp = new(width, height);

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                byte value = (byte)((x % 10) * 25);
                RawColor color = RawColor.Gray(value++);
                bmp.SetPixel(x, y, color);
            }
        }

        return bmp.GetBitmapBytes();
    }
}
