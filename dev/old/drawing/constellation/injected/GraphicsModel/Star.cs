using System;
using System.Collections.Generic;
using System.Text;

namespace GraphicsModel
{
    public class Star
    {
        public float X;
        public float Y;
        public float Xvel;
        public float Yvel;

        public float Radius = 3;
        public Color Color = new Color(255, 255, 255);
        public Point Point { get { return new Point(X, Y); } }
    }
}
