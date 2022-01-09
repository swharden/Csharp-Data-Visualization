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
    }
}
