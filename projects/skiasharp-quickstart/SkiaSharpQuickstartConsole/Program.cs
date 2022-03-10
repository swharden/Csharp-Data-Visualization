using SkiaSharp;

SKBitmap bmp = new(640, 480);
using SKCanvas canvas = new(bmp);
canvas.Clear(SKColor.Parse("#003366"));

SKPaint paint = new()
{
    Color = SKColors.White.WithAlpha(100),
    IsAntialias = true,
};

Random rand = new(0);
for (int i = 0; i < 100; i++)
{
    SKPoint pt1 = new(rand.Next(bmp.Width), rand.Next(bmp.Height));
    SKPoint pt2 = new(rand.Next(bmp.Width), rand.Next(bmp.Height));
    paint.StrokeWidth = rand.Next(1, 10);
    canvas.DrawLine(pt1, pt2, paint);
}

string filePath = Path.GetFullPath("quickstart.jpg");
SKFileWStream fs = new(filePath);
bmp.Encode(fs, SKEncodedImageFormat.Jpeg, quality: 85);
Console.WriteLine(filePath);