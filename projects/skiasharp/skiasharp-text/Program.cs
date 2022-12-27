using SkiaSharp;

Quickstart();
Options();
MeasureString();
Rotation();
Rotation2();

static void Quickstart()
{
    SKBitmap bmp = new(400, 200);
    using SKCanvas canvas = new(bmp);
    canvas.Clear(SKColors.Navy);

    using SKPaint paint = new()
    {
        Color = SKColors.Yellow,
        IsAntialias = true,
        TextSize = 36,
    };

    canvas.DrawText("Hello, World", 20, 100, paint);

    using SKFileWStream fs = new("quickstart.png");
    bmp.Encode(fs, SKEncodedImageFormat.Png, quality: 100);
}

static void Options()
{
    SKBitmap bmp = new(400, 200);
    using SKCanvas canvas = new(bmp);
    canvas.Clear(SKColors.Navy);

    using SKPaint paint = new()
    {
        Color = SKColors.Yellow,
        IsAntialias = true,
        TextSize = 64,
        Typeface = SKTypeface.FromFamilyName(
            familyName: "Impact",
            weight: SKFontStyleWeight.SemiBold,
            width: SKFontStyleWidth.Normal,
            slant: SKFontStyleSlant.Italic),
    };

    canvas.DrawText("Hello, World", 20, 100, paint);

    using SKFileWStream fs = new("options.png");
    bmp.Encode(fs, SKEncodedImageFormat.Png, quality: 100);
}

static void MeasureString()
{
    SKBitmap bmp = new(400, 200);
    using SKCanvas canvas = new(bmp);
    canvas.Clear(SKColors.Navy);

    using SKPaint paint = new()
    {
        Color = SKColors.Yellow,
        IsAntialias = true,
        TextSize = 64,
    };

    string text = "Hello, World";
    SKRect rect = new();
    paint.MeasureText(text, ref rect);
    Console.WriteLine($"Width={rect.Width}, Height={rect.Height}");

    SKPoint pt = new(20, 100);
    canvas.DrawText(text, pt, paint);

    rect.Offset(pt);
    paint.IsStroke = true;
    paint.Color = SKColors.Magenta;
    canvas.DrawRect(rect, paint);

    using SKFileWStream fs = new("measure.png");
    bmp.Encode(fs, SKEncodedImageFormat.Png, quality: 100);
}

static void Rotation()
{
    SKBitmap bmp = new(400, 400);
    using SKCanvas canvas = new(bmp);
    canvas.Clear(SKColors.Navy);

    canvas.Translate(200, 200);
    canvas.RotateDegrees(-45);

    using SKPaint paint = new()
    {
        Color = SKColors.Yellow,
        IsAntialias = true,
        TextSize = 36,
    };

    canvas.DrawText("Hello, World", 0, 0, paint);

    SKFileWStream fs = new("rotation.png");
    bmp.Encode(fs, SKEncodedImageFormat.Png, quality: 100);
}

static void Rotation2()
{
    SKBitmap bmp = new(200, 200);
    using SKCanvas canvas = new(bmp);
    canvas.Clear(SKColors.Navy);

    canvas.Translate(100, 100);
    canvas.RotateDegrees(-90.1f);

    using SKPaint paint = new()
    {
        Color = SKColors.Yellow,
        IsAntialias = true,
        TextSize = 16,
    };

    canvas.DrawText("Very Wild", 0, 0, paint);

    SKFileWStream fs = new("rotation-artifact-fixed.png");
    bmp.Encode(fs, SKEncodedImageFormat.Png, quality: 100);
}