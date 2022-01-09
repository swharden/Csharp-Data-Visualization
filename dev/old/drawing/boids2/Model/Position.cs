using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public struct Position
    {
        public double X;
        public double Y;

        public Position(double x, double y)
        {
            X = x;
            Y = y;
        }

        public void Shift(Position pos)
        {
            X += pos.X;
            Y += pos.Y;
        }

        public void Move(Velocity vel, double stepSize)
        {
            X += vel.X * stepSize;
            Y += vel.Y * stepSize;
        }

        public (double x, double y) Delta(Position otherPosition)
        {
            return (otherPosition.X - X, otherPosition.Y - Y);
        }

        public double Distance(Position otherPosition)
        {
            (double dX, double dY) = Delta(otherPosition);
            return Math.Sqrt(dX * dX + dY * dY);
        }
    }
}
