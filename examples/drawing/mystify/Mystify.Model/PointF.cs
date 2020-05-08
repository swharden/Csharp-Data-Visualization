using System;
using System.Collections.Generic;
using System.Text;

namespace Mystify.Model
{
    public struct PointF
    {
        public float X;
        public float Y;

        public PointF(double x, double y)
        {
            X = (float)x;
            Y = (float)y;
        }
    }
}
