using System;
using System.Drawing;

namespace Starfield
{
    public class Field
    {
        Random rand = new Random();

        Star[] stars;

        public Field(int starCount)
        {
            Reset(starCount);
        }

        public void Reset(int starCount)
        {
            stars = new Star[starCount];
            for (int i = 0; i < starCount; i++)
                stars[i] = GetRandomStar();
        }

        private Star GetRandomStar(bool randomSize = true)
        {
            double starSize = 1;
            if (randomSize)
                starSize += rand.NextDouble() * 5;

            return new Star
            {
                x = rand.NextDouble(),
                y = rand.NextDouble(),
                size = starSize
            };
        }

        public void Advance(double step = .01)
        {
            for (int i = 0; i < stars.Length; i++)
            {
                var star = stars[i];
                star.x += (star.x - .5) * star.size * step;
                star.y += (star.y - .5) * star.size * step;
                star.size += star.size * step * 2;

                // reset stars that went out of bounds
                if (star.x < 0 || star.x > 1 ||
                    star.y < 0 || star.y > 1)
                    stars[i] = GetRandomStar(randomSize: false);
            }
        }

        public void Render(Bitmap bmp, Color? starColor = null)
        {
            starColor = (starColor is null) ? Color.White : starColor.Value;
            using (var brush = new SolidBrush(starColor.Value))
            using (var gfx = Graphics.FromImage(bmp))
            {
                gfx.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                gfx.Clear(Color.Black);
                for (int i = 0; i < stars.Length; i++)
                {
                    var star = stars[i];
                    float xPixel = (float)star.x * bmp.Width;
                    float yPixel = (float)star.y * bmp.Height;
                    float radius = (float)star.size - 1;
                    float diameter = radius * 2;
                    gfx.FillEllipse(brush, xPixel - radius, yPixel - radius, diameter, diameter);
                }
            }
        }
    }
}
