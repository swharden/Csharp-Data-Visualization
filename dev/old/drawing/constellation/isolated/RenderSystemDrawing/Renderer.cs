using GraphicsModel;
using System;
using System.Drawing;

namespace RenderSystemDrawing
{
    public static class Renderer
    {
        public static void Render(Field field, Bitmap bmp)
        {
            using (Graphics gfx = Graphics.FromImage(bmp))
            using (Brush brush = new SolidBrush(Color.White))
            using (Pen pen = new Pen(Color.White))
            {

                gfx.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                // clear background
                gfx.Clear(ColorTranslator.FromHtml("#003366"));

                // draw stars
                float starRadius = 3;
                foreach (var star in field.Stars)
                {
                    var rect = new RectangleF(
                            x: star.X - starRadius,
                            y: star.Y - starRadius,
                            width: starRadius * 2,
                            height: starRadius * 2
                        );

                    gfx.FillEllipse(brush, rect);
                }

                // draw lines connecting close stars
                double connectDistance = 100;
                foreach (var star1 in field.Stars)
                {
                    foreach (var star2 in field.Stars)
                    {
                        double dX = Math.Abs(star1.X - star2.X);
                        double dY = Math.Abs(star1.Y - star2.Y);
                        if (dX > connectDistance || dY > connectDistance)
                            continue;
                        double distance = Math.Sqrt(dX * dX + dY * dY);
                        int alpha = (int)(255 - distance / connectDistance * 255);
                        alpha = Math.Min(alpha, 255);
                        alpha = Math.Max(alpha, 0);
                        pen.Color = Color.FromArgb(alpha, Color.White);
                        if (distance < connectDistance)
                            gfx.DrawLine(pen, star1.X, star1.Y, star2.X, star2.Y);
                    }
                }
            }
        }
    }
}
