using Microsoft.Maui.Graphics;
using Microsoft.Maui.Graphics.Skia;

namespace ConsoleApp;

public static class Program
{
    public static void Main()
    {
        RenderStandardModel();
    }

    private static void RenderStandardModel()
    {
        using SkiaBitmapExportContext context = new(400, 300, 1.0f);

        Random rand = new();
        StandardGraphics.Render.TestImage(rand, context.Canvas, context.Width, context.Height);

        using FileStream fs = new("test.png", FileMode.Create);
        context.WriteToStream(fs);
    }
}