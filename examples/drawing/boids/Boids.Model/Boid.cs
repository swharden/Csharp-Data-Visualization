using System;
using System.Collections.Generic;
using System.Text;

namespace Boids.Model
{
    public class Boid
    {
        public double X;
        public double Y;
        public double Xvel;
        public double Yvel;

        public Boid(double x, double y, double xVel, double yVel)
        {
            (X, Y, Xvel, Yvel) = (x, y, xVel, yVel);
        }

        public Boid(Random rand, double width, double height)
        {
            X = rand.NextDouble() * width;
            Y = rand.NextDouble() * height;
            Xvel = (rand.NextDouble() - .5);
            Yvel = (rand.NextDouble() - .5);
        }

        public void MoveForward(double minSpeed = 1, double maxSpeed = 5)
        {
            X += Xvel;
            Y += Yvel;

            var speed = GetSpeed();
            if (speed > maxSpeed)
            {
                Xvel = (Xvel / speed) * maxSpeed;
                Yvel = (Yvel / speed) * maxSpeed;
            }
            else if (speed < minSpeed)
            {
                Xvel = (Xvel / speed) * minSpeed;
                Yvel = (Yvel / speed) * minSpeed;
            }

            if (double.IsNaN(Xvel))
                Xvel = 0;
            if (double.IsNaN(Yvel))
                Yvel = 0;
        }

        public (double x, double y) GetPosition(double time)
        {
            return (X + Xvel * time, Y + Yvel * time);
        }

        public void Accelerate(double scale = 1.0)
        {
            Xvel *= scale;
            Yvel *= scale;
        }

        public double GetAngle()
        {
            if (double.IsNaN(Xvel) || double.IsNaN(Yvel))
                return 0;
            if (Xvel == 0 && Yvel == 0)
                return 0;
            double angle = Math.Atan(Yvel / Xvel) * 180 / Math.PI - 90;
            if (Xvel < 0)
                angle += 180;
            return angle;
        }

        public double GetSpeed()
        {
            return Math.Sqrt(Xvel * Xvel + Yvel * Yvel);
        }

        public double GetDistance(Boid otherBoid)
        {
            double dX = otherBoid.X - X;
            double dY = otherBoid.Y - Y;
            double dist = Math.Sqrt(dX * dX + dY * dY);
            return dist;
        }
    }
}
