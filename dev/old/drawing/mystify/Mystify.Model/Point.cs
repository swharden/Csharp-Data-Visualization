using System;
using System.Collections.Generic;
using System.Text;

namespace Mystify.Model
{
    public struct Point
    {
        public float X;
        public float Y;

        public Point(double x, double y)
        {
            X = (float)x;
            Y = (float)y;
        }
    }
}
