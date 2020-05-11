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

        public double GetAngle()
        {
            double angle = Math.Atan(Yvel / Xvel) * 180 / Math.PI - 90;
            if (Xvel < 0)
                angle += 180;
            return angle;
        }
    }
}
