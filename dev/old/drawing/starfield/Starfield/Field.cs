using System;
using System.Collections.Generic;
using System.Drawing;

namespace Starfield
{
    public class Field
    {
        public struct Star
        {
            public double x;
            public double y;
            public double size;
        }

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
                stars[i].x += (stars[i].x - .5) * stars[i].size * step;
                stars[i].y += (stars[i].y - .5) * stars[i].size * step;
                stars[i].size += stars[i].size * step * 2;

                // reset stars that went out of bounds
                if (stars[i].x < 0 || stars[i].x > 1 ||
                    stars[i].y < 0 || stars[i].y > 1)
                    stars[i] = GetRandomStar(randomSize: false);
            }
        }

        public void Render(Bitmap bmp, Color? starColor = null)
        {
            starColor = starColor ?? Color.White;
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

        public Star[] GetStars()
        {
            return stars;
        }
    }
}
