using SkiaSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSkia
{
    public static class TestImages
    {
        public static System.Drawing.Bitmap Text(SKSurface surface, int count)
        {
            float width = surface.Canvas.LocalClipBounds.Width;
            float height = surface.Canvas.LocalClipBounds.Height;

            surface.Canvas.DrawColor(new SKColor(0, 0, 0));

            var paint = new SKPaint();
            paint.TextSize = 64.0f;
            paint.IsAntialias = true;

            Random rand = new Random();
            for (int i = 0; i < count; i++)
            {
                paint.Color = new SKColor((byte)rand.Next(255), (byte)rand.Next(255), (byte)rand.Next(255));
                surface.Canvas.DrawText("Skia", (float)rand.NextDouble() * width, (float)rand.NextDouble() * height, paint);
            }

            SKImage image = surface.Snapshot();
            SKData data = image.Encode(SKEncodedImageFormat.Png, 100);
            MemoryStream mStream = new MemoryStream(data.ToArray());

            var bmp = new System.Drawing.Bitmap(mStream, false);
            return bmp;
        }

        public static System.Drawing.Bitmap Lines(SKSurface surface, int count)
        {
            float width = surface.Canvas.LocalClipBounds.Width;
            float height = surface.Canvas.LocalClipBounds.Height;

            surface.Canvas.DrawColor(new SKColor(0, 0, 0));

            var paint = new SKPaint();
            paint.IsAntialias = true;

            Random rand = new Random();
            for (int i = 0; i < count; i++)
            {
                paint.Color = new SKColor((byte)rand.Next(255), (byte)rand.Next(255), (byte)rand.Next(255));
                SKPoint pt0 = new SKPoint((float)(rand.NextDouble() * width), (float)(rand.NextDouble() * height));
                SKPoint pt1 = new SKPoint((float)(rand.NextDouble() * width), (float)(rand.NextDouble() * height));
                surface.Canvas.DrawLine(pt0, pt1, paint);
            }

            SKImage image = surface.Snapshot();
            SKData data = image.Encode(SKEncodedImageFormat.Png, 100);
            MemoryStream mStream = new MemoryStream(data.ToArray());

            var bmp = new System.Drawing.Bitmap(mStream, false);
            return bmp;
        }

    }
}
