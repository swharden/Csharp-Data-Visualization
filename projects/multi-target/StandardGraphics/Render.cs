using Microsoft.Maui.Graphics;
using System;

namespace StandardGraphics;

public static class Render
{
    public static void TestImage(Random rand, ICanvas canvas, float width, float height, int lines = 100)
    {
        canvas.FillColor = Colors.Navy;
        canvas.FillRectangle(0, 0, width, height);

        canvas.StrokeColor = Colors.White;
        for (int i = 0; i < lines; i++)
        {
            float x1 = (float)rand.NextDouble() * width;
            float x2 = (float)rand.NextDouble() * width;
            float y1 = (float)rand.NextDouble() * height;
            float y2 = (float)rand.NextDouble() * height;
            canvas.DrawLine(x1, y1, x2, y2);
        }
    }
}
