using SkiaSharp;
using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Random rand = new Random();

            // set up the image
            SKImageInfo imageInfo = new SKImageInfo(800, 600, SKColorType.Rgb888x, SKAlphaType.Premul);
            SKSurface surface = SKSurface.Create(imageInfo);
            SKCanvas canvas = surface.Canvas;

            // draw some lines
            canvas.Clear(SKColor.Parse("#003366"));
            var paint = new SKPaint
            {
                Color = new SKColor(255, 255, 255, 123),
                IsAntialias = true
            };
            for (int i = 0; i < 1000; i++)
            {
                SKPoint ptA = new SKPoint(rand.Next(imageInfo.Width), rand.Next(imageInfo.Height));
                SKPoint ptB = new SKPoint(rand.Next(imageInfo.Width), rand.Next(imageInfo.Height));
                canvas.DrawLine(ptA, ptB, paint);
            }

            // save the output
            string filename = System.IO.Path.GetFullPath("test.png");
            System.IO.Stream fileStream = System.IO.File.OpenWrite(filename);
            SKImage snap = surface.Snapshot();
            SKData encoded = snap.Encode(SKEncodedImageFormat.Png, 100);
            encoded.SaveTo(fileStream);
            Console.WriteLine($"Saved: {filename}");
        }
    }
}
