using System;
using System.Diagnostics;

namespace IntroAnimation
{
    public struct Color
    {
        public byte R, G, B, A;
        public Color(byte R, byte G, byte B, byte A = 255)
        {
            this.R = R;
            this.G = G;
            this.B = B;
            this.A = A;
        }
    }

    public struct Corner
    {
        public double X, Y;
        public double velX, velY;

        public Corner(double X, double Y, double velX, double velY)
        {
            this.X = X;
            this.Y = Y;
            this.velX = velX;
            this.velY = velY;
        }
    }

    public class Field
    {
        readonly Random rand = new Random();
        public readonly Corner[] corners;
        public readonly int width, height;

        public Field(int width, int height, int cornerCount = 50)
        {
            this.width = width;
            this.height = height;
            corners = new Corner[cornerCount];

            for (int i = 0; i < corners.Length; i++)
                corners[i] = RandomCorner();
        }

        public double GetDistance(int cornerIndexA, int cornerIndexB)
        {
            double dX = corners[cornerIndexA].X - corners[cornerIndexB].X;
            double dY = corners[cornerIndexA].Y - corners[cornerIndexB].Y;
            return Math.Sqrt(dX * dX + dY * dY);
        }

        readonly Stopwatch stopwatch = Stopwatch.StartNew();
        public string GetBenchmarkMessage()
        {
            double elapsedSeconds = (double)stopwatch.ElapsedMilliseconds / 1000;
            return string.Format("Rendered {0} frames in {1:0.00} seconds ({2:0.00} Hz)", stepCount, elapsedSeconds, stepCount / elapsedSeconds);
        }

        private int stepCount;
        public void StepForward(double stepSize = 1.0)
        {
            stepCount += 1;
            for (int i = 0; i < corners.Length; i++)
            {
                corners[i].X += corners[i].velX * stepSize;
                corners[i].Y += corners[i].velY * stepSize;
                if (corners[i].X < 0 || corners[i].X > width || corners[i].Y < 0 || corners[i].Y > height)
                    corners[i] = RandomCornerAtEdge();
            }
        }

        private readonly Color bgColor1 = new Color(0, 51, 102);
        private readonly Color bgColor2 = new Color(102, 0, 0);
        public Color GetBackgroundColor()
        {
            int stepsPerCycle = 300;
            double colorPhase = (double)(stepCount % stepsPerCycle) * 2 / stepsPerCycle;
            if (colorPhase > 1)
                colorPhase = 2 - colorPhase;
            double colorWeight1 = 1 - colorPhase;
            double colorWeight2 = colorPhase;
            byte R = (byte)(bgColor1.R * colorWeight1 + bgColor2.R * colorWeight2);
            byte G = (byte)(bgColor1.G * colorWeight1 + bgColor2.G * colorWeight2);
            byte B = (byte)(bgColor1.B * colorWeight1 + bgColor2.B * colorWeight2);
            return new Color(R, G, B);
        }

        public Corner RandomCorner()
        {
            return new Corner(
                X: rand.NextDouble() * width,
                Y: rand.NextDouble() * height,
                velX: rand.NextDouble() * 2 - 1,
                velY: rand.NextDouble() * 2 - 1
                );
        }

        public Corner RandomCornerAtEdge()
        {
            Corner corner = RandomCorner();

            if (rand.NextDouble() < .5)
            {
                // on vertical edge
                if (rand.NextDouble() < .5)
                {
                    // on left
                    corner.X = 0;
                    corner.velX = Math.Abs(corner.velX);
                }
                else
                {
                    // on right
                    corner.X = width;
                    corner.velX = -Math.Abs(corner.velX);
                }
            }
            else
            {
                // on horizontal edge
                if (rand.NextDouble() < .5)
                {
                    // on top
                    corner.Y = 0;
                    corner.velY = Math.Abs(corner.velY);
                }
                else
                {
                    // on bottom
                    corner.Y = height;
                    corner.velY = -Math.Abs(corner.velY);
                }
            }

            return corner;
        }
    }
}
