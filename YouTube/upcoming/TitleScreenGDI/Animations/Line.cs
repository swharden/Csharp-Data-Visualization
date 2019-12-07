using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TitleScreenGDI.Animations
{
    public class Line
    {
        public float x1, x2, y1, y2;

        public Line(Point pt1, Point pt2)
        {
            x1 = pt1.X;
            y1 = pt1.Y;
            x2 = pt2.X;
            y2 = pt2.Y;
        }

        public float deltaX { get { return x2 - x1; } }
        public float deltaY { get { return y2 - y1; } }
        public float length { get { return (float)Math.Sqrt(Math.Pow(deltaX, 2) + Math.Pow(deltaY, 2)); } }
    }
}
