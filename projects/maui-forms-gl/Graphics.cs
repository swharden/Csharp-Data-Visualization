using Microsoft.Maui.Graphics;

namespace FormsLife
{
    public static class Graphics
    {
        public static void RandomLines(Random rand, ICanvas canvas, float width, float height, int lines = 100)
        {
            canvas.FillColor = Colors.Navy;
            canvas.FillRectangle(0, 0, width, height);

            canvas.StrokeColor = Colors.White.WithAlpha(.1f);
            canvas.StrokeSize = 2;
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
}