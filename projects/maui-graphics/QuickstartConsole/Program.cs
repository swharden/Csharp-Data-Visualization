using Microsoft.Maui.Graphics;
using Microsoft.Maui.Graphics.Skia;

SkiaBitmapExportContext bmp = new(600, 400, 1.0f);
ICanvas canvas = bmp.Canvas;

canvas.FillColor = Colors.Navy;
canvas.FillRectangle(0, 0, bmp.Width, bmp.Height);

Random rand = new(0);
canvas.StrokeColor = Colors.White.WithAlpha(.5f);
canvas.StrokeSize = 2;
for (int i = 0; i < 100; i++)
{
    float x = rand.Next(bmp.Width);
    float y = rand.Next(bmp.Height);
    float r = rand.Next(5, 50);
    canvas.DrawCircle(x, y, r);
}

bmp.WriteToFile("console.png");