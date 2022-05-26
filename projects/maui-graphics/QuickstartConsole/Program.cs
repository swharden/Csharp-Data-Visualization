using Microsoft.Maui.Graphics;
using Microsoft.Maui.Graphics.Skia;

MinimalExample();
BiggerExample();

static void MinimalExample()
{
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
}

static void BiggerExample()
{
    // Create a Bitmap in memory and draw on its Canvas
    SkiaBitmapExportContext bmp = new(600, 400, 1.0f);
    ICanvas canvas = bmp.Canvas;

    // draw a big blue rectangle with a yellow border
    Rect backgroundRectangle = new(0, 0, bmp.Width, bmp.Height);
    canvas.FillColor = Color.FromArgb("#003366");
    canvas.FillRectangle(backgroundRectangle);
    canvas.StrokeColor = Colors.Black;
    canvas.StrokeSize = 20;
    canvas.DrawRectangle(backgroundRectangle);

    // draw circles randomly around the screen
    for (int i = 0; i < 100; i++)
    {
        float x = Random.Shared.Next(bmp.Width);
        float y = Random.Shared.Next(bmp.Height);
        float r = Random.Shared.Next(5, 50);

        Color randomColor = Color.FromRgb(
            red: Random.Shared.Next(255),
            green: Random.Shared.Next(255),
            blue: Random.Shared.Next(255));

        canvas.StrokeSize = r / 3;
        canvas.StrokeColor = randomColor.WithAlpha(.3f);
        canvas.DrawCircle(x, y, r);
    }

    // measure string
    string myText = "Hello, Maui.Graphics!";
    Font myFont = new Font("Impact");
    float myFontSize = 48;
    canvas.Font = myFont;
    SizeF textSize = canvas.GetStringSize(myText, myFont, myFontSize);

    // draw the rectangle that holds the string
    Point point = new(
        x: (bmp.Width - textSize.Width) / 2,
        y: (bmp.Height - textSize.Height) / 2);

    // measure and draw a string
    Rect myTextRectangle = new(point, textSize);
    canvas.FillColor = Colors.Black.WithAlpha(.5f);
    canvas.FillRectangle(myTextRectangle);
    canvas.StrokeSize = 2;
    canvas.StrokeColor = Colors.Yellow;
    canvas.DrawRectangle(myTextRectangle);
    canvas.FontSize = myFontSize * .9f; // smaller than the rectangle
    canvas.FontColor = Colors.White;
    canvas.DrawString(myText, myTextRectangle, HorizontalAlignment.Center, VerticalAlignment.Center, TextFlow.OverflowBounds);

    // Save the image
    bmp.WriteToFile("console2.png");
}