using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TitleScreenGDI.Animations
{
    public class Circle
    {
        public float X, Y;
        public double velX, velY;
        public PointF LocationF { get { return new PointF(X, Y); } }
        public Point Location { get { return Point.Round(LocationF); } }
    }
}
