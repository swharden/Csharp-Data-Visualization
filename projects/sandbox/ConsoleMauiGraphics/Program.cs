using Microsoft.Maui.Graphics;
using Microsoft.Maui.Graphics.Skia;

// create a new image with a blue background
using SkiaBitmapExportContext context = new(400, 300, 1.0f);
ICanvas canvas = context.Canvas;
canvas.FillColor = Colors.Navy;
canvas.FillRectangle(0, 0, context.Width, context.Height);

// draw a test pattern
canvas.StrokeColor = Colors.Yellow;
canvas.StrokeSize = 3;
canvas.FillColor = Colors.Red;
canvas.FillRoundedRectangle(
    x: 50,
    y: 50,
    width: 300,
    height: 200,
    topLeftCornerRadius: 5,
    topRightCornerRadius: 20,
    bottomLeftCornerRadius: 40,
    bottomRightCornerRadius: 60);

// save output
string filePath = Path.GetFullPath("test.png");
using FileStream fs = new(filePath, FileMode.Create);
context.WriteToStream(fs);
Console.WriteLine(filePath);