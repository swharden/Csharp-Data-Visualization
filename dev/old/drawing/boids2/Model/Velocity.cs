using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public struct Velocity
    {
        public double X;
        public double Y;

        public Velocity(double x, double y)
        {
            X = x;
            Y = y;
        }

        public double GetAngle()
        {
            if (X == 0 && Y == 0)
                return 0;
            double angle = Math.Atan(Y / X) * 180 / Math.PI - 90;
            if (X < 0)
                angle += 180;
            return angle;
        }

        public void SetSpeed(double speed, bool absolute = false)
        {
            if (X == 0 && Y == 0)
                return;

            var currentSpeed = Math.Sqrt(X * X + Y * Y);

            double targetX = (X / currentSpeed) * speed;
            double targetY = (Y / currentSpeed) * speed;

            if (absolute)
            {
                X = targetX;
                Y = targetY;
            }
            else
            {
                X += (targetX - X) * .1;
                Y += (targetY - Y) * .1;
            }
        }
    }
}