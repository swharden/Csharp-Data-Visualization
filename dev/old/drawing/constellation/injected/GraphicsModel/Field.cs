using System;
using System.Security.Cryptography;
using System.Threading;

namespace GraphicsModel
{
    public class Field : IDisposable
    {
        public readonly Star[] Stars;
        public readonly float Width;
        public readonly float Height;
        readonly Random rand = new Random();

        Thread advancerThread;
        public Field(float width, float height, double density = 1)
        {
            double pxPerStar = 1e4 / density;
            int starCount = (int)(width * height / pxPerStar);
            Stars = new Star[starCount];
            Width = width;
            Height = height;
            Randomize();
            advancerThread = new Thread(new ThreadStart(AdvanceForever));
            advancerThread.Start();
        }

        bool keepAdvancing = true;
        void AdvanceForever()
        {
            while (keepAdvancing)
            {
                Advance();
                Thread.Sleep(1);
            }
        }

        public void Dispose()
        {
            keepAdvancing = false;
        }

        void Randomize(float minVelocity = .1f)
        {
            for (int i = 0; i < Stars.Length; i++)
            {
                Stars[i] = new Star()
                {
                    X = (float)rand.NextDouble() * Width,
                    Xvel = minVelocity + (float)rand.NextDouble(),
                    Y = (float)rand.NextDouble() * Height,
                    Yvel = minVelocity + (float)rand.NextDouble(),
                };
                if (rand.NextDouble() > .5)
                    Stars[i].Xvel *= -1;
                if (rand.NextDouble() > .5)
                    Stars[i].Yvel *= -1;
            }
        }

        void Advance(float multiplier = .1f)
        {
            for (int i = 0; i < Stars.Length; i++)
            {
                Stars[i].X += Stars[i].Xvel * multiplier;
                Stars[i].Y += Stars[i].Yvel * multiplier;

                bool outOfBoundsX = (Stars[i].X < 0) || (Stars[i].X > Width);
                bool outOfBoundsY = (Stars[i].Y < 0) || (Stars[i].Y >= Height);

                if (outOfBoundsX) Stars[i].Xvel *= -1;
                if (outOfBoundsY) Stars[i].Yvel *= -1;
            }
        }

        public void Render(IRenderer renderer)
        {
            // clear background
            var backColor = new Color("#003366");
            renderer.Clear(backColor);

            // draw stars
            foreach (var star in Stars)
                renderer.FillCircle(star.Point, star.Radius, star.Color);

            // draw lines connecting close stars
            double connectDistance = 100;
            foreach (var star1 in Stars)
            {
                foreach (var star2 in Stars)
                {
                    // prevent duplicate lines
                    if (star1.Y >= star2.Y)
                        continue;

                    // determine star distance
                    double dX = Math.Abs(star1.X - star2.X);
                    double dY = Math.Abs(star1.Y - star2.Y);
                    if (dX > connectDistance || dY > connectDistance)
                        continue;
                    double distance = Math.Sqrt(dX * dX + dY * dY);

                    // set line alpha based on distance
                    int alpha = (int)(255 - distance / connectDistance * 255) * 2;
                    alpha = Math.Min(alpha, 255);
                    alpha = Math.Max(alpha, 0);
                    var lineColor = new Color(255, 255, 255, (byte)alpha);
                    if (distance < connectDistance)
                        renderer.DrawLine(star1.Point, star2.Point, 1, lineColor);
                }
            }
        }
    }
}
