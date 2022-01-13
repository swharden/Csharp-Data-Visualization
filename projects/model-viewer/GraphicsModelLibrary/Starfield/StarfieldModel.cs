using Microsoft.Maui.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicsModelLibrary.Starfield
{
    public class Star
    {
        public float X;
        public float Y;
        public float Size;
        public Color Color;
    }

    public class StarfieldModel : IGraphicsModel
    {
        readonly Random Rand = new();
        readonly Star[] Stars;
        float Width;
        float Height;

        public StarfieldModel(int stars = 1000)
        {
            Stars = new Star[stars];
        }

        private Star CreateRandomStar()
        {
            return new Star()
            {
                X = (float)Rand.NextDouble() * Width,
                Y = (float)Rand.NextDouble() * Height,
                Size = .1f,
                Color = Color.FromHsv(h: (float)Rand.NextDouble(), s: .5f, v: 1),
            };
        }

        public void Reset(float width, float height)
        {
            Resize(width, height);
            for (int i = 0; i < Stars.Length; i++)
                Stars[i] = CreateRandomStar();
        }

        public void Resize(float width, float height)
        {
            Width = width;
            Height = height;
        }

        public void Advance(float time = 1)
        {
            float speed = .005f;
            float distance = time * speed;

            for (int i = 0; i < Stars.Length; i++)
            {
                Star star = Stars[i];
                float dX = star.X - Width / 2;
                float dY = star.Y - Height / 2;

                star.X += dX * distance;
                star.Y += dY * distance;
                star.Size += .01f;

                if (IsOutOfBounds(star))
                    Stars[i] = CreateRandomStar();
            }
        }

        private bool IsOutOfBounds(Star star) =>
            star.X - star.Size < 0 ||
            star.X + star.Size >= Width ||
            star.Y - star.Size < 0 || 
            star.Y + star.Size >= Height;

        public void Draw(ICanvas canvas, float width, float height)
        {
            Draw(canvas, new RectangleF(0, 0, width, height));
        }

        public void Draw(ICanvas canvas, RectangleF dirtyRect)
        {
            canvas.FillColor = Colors.Black;
            canvas.FillRectangle(dirtyRect);

            foreach (Star star in Stars)
            {
                canvas.FillColor = star.Color;
                canvas.FillCircle(star.X, star.Y, star.Size);
            }
        }
    }
}
