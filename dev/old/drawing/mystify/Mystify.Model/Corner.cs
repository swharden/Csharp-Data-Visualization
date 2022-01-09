using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Mystify.Model
{
    public class Corner
    {
        public readonly double Width;
        public readonly double Height;

        public double X { get; private set; }
        public double Y { get; private set; }

        public double Xvel { get; private set; }
        public double Yvel { get; private set; }
        private const double minVel = .5;
        private const double maxVel = 1;

        private readonly Random Rand;

        public readonly List<Point> Points = new List<Point>();
        public readonly int PointsToKeep;

        public Point PointF { get { return new Point(X, Y); } }

        public Corner(Random rand, double width, double height, int pointsToKeep)
        {
            Rand = rand;
            Width = width;
            Height = height;
            PointsToKeep = pointsToKeep;
            X = rand.NextDouble() * width;
            Y = rand.NextDouble() * height;
            Xvel = RandomVelocity(true);
            Yvel = RandomVelocity(true);
        }

        private double RandomVelocity(bool canBeNegative = false)
        {
            double vel = minVel + Rand.NextDouble() * (maxVel - minVel);
            if (canBeNegative && Rand.NextDouble() < .5)
                vel *= -1;
            return vel;
        }

        public void Advance(double speed = 1)
        {
            X += Xvel * speed;
            Y += Yvel * speed;
            Points.Add(new Point(X, Y));
            if (Points.Count > PointsToKeep)
                Points.RemoveRange(0, Points.Count - PointsToKeep);
            BounceOffEdges();
        }

        private void BounceOffEdges()
        {
            if (X <= 0) Xvel = RandomVelocity();
            if (Y <= 0) Yvel = RandomVelocity();
            if (X > Width) Xvel = -RandomVelocity();
            if (Y > Height) Yvel = -RandomVelocity();
        }
    }
}
