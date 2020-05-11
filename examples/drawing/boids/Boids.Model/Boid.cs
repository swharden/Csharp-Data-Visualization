using System;
using System.Collections.Generic;
using System.Text;

namespace Boids.Model
{
    public class Boid
    {
        public double X, Y;
        public double Xvel, Yvel;

        public Boid(double x, double y, double xVel = 0, double yVel = 0)
        {
            (X, Y, Xvel, Yvel) = (x, y, xVel, yVel);
        }

        public (double x, double y) FuturePosition(double distance)
        {
            return (X + Xvel * distance, Y + Yvel * distance);
        }

        public double GetSpeed()
        {
            return Math.Sqrt(Xvel * Xvel + Yvel * Yvel);
        }

        public void Accelerate(double scale = 1.0)
        {
            Xvel *= scale;
            Yvel *= scale;
        }

        public double GetAngle()
        {
            if (Xvel == 0 && Yvel == 0)
                return 0;
            double angle = Math.Atan(Yvel / Xvel) * 180 / Math.PI - 90;
            if (Xvel < 0)
                angle += 180;
            return angle;
        }

        public double Distance(Boid boid)
        {
            double dX = boid.X - X;
            double dY = boid.Y - Y;
            double dist = Math.Sqrt(dX * dX + dY * dY);
            return dist;
        }
    }
}
