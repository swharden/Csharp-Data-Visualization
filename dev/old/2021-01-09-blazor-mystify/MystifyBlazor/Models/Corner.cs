using System;

namespace MystifyBlazor.Models
{
    /// <summary>
    /// A moving corner has a position and velocity
    /// </summary>
    public class Corner
    {
        public double X;
        public double Y;
        public double XVel;
        public double YVel;

        public Corner() { }

        public static Corner RandomPoint(Random rand, double width, double height) =>
            new Corner()
            {
                X = width * rand.NextDouble(),
                Y = height * rand.NextDouble(),
                XVel = rand.NextDouble() - .5,
                YVel = rand.NextDouble() - .5,
            };

        public Corner(double x, double y, double xVel, double yVel) =>
            (X, Y, XVel, YVel) = (x, y, xVel, yVel);

        public Corner NextPoint(double delta, double width, double height)
        {
            Corner pt = new Corner(X, Y, XVel, YVel);
            pt.Advance(delta, width, height);
            return pt;
        }

        public void Advance(double delta, double width, double height)
        {
            X += XVel * delta;
            Y += YVel * delta;

            if (X < 0 || X > width)
                XVel *= -1;
            if (Y < 0 || Y > height)
                YVel *= -1;

            if (X < 0)
                X += 0 - X;
            else if (X > width)
                X -= X - width;

            if (Y < 0)
                Y += 0 - Y;
            if (Y > height)
                Y -= Y - height;
        }
    }
}
