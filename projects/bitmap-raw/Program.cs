using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SkiaSharp;

SaveBitmapBytes(BitmapGenerator.Rainbow(), "rainbow.jpg");
SaveBitmapBytes(BitmapGenerator.RandomGrayscale(), "random-grayscale.jpg");
SaveBitmapBytes(BitmapGenerator.RandomRGB(), "random-rgb.jpg");
SaveBitmapBytes(BitmapGenerator.RandomRectangles(), "rectangles.jpg");
SaveBitmapBytes(BitmapGenerator.Ramps(), "ramps.jpg");

static void SaveBitmapBytes(byte[] bytes, string filename)
{
    SaveBitmapImageSharp(bytes, filename);
    SaveBitmapSkia(bytes, filename);
    SaveBitmapSystemDrawing(bytes, filename);
}

static void SaveBitmapImageSharp(byte[] bytes, string filename)
{
    using Image image = Image.Load(bytes);

    string saveAs = Path.GetFullPath("ImageSharp-" + filename);
    JpegEncoder encoder = new() { Quality = 95 };
    image.Save(saveAs, encoder);
    Console.WriteLine(saveAs);
}

static void SaveBitmapSkia(byte[] bytes, string filename)
{
    using SKBitmap bmp = SKBitmap.Decode(bytes);

    string saveAs = Path.GetFullPath("SkiaSharp-" + filename);
    using SKFileWStream fs = new(saveAs);
    bmp.Encode(fs, SKEncodedImageFormat.Jpeg, quality: 95);
    Console.WriteLine(saveAs);
}

static void SaveBitmapSystemDrawing(byte[] bytes, string filename)
{
    using MemoryStream ms = new(bytes);
    using System.Drawing.Image image = System.Drawing.Bitmap.FromStream(ms);

    string saveAs = Path.GetFullPath("SystemDrawing-" + filename);
    image.Save(saveAs);
    Console.WriteLine(saveAs);
}