using System;
using System.Collections.Generic;
using System.Text;

namespace Mystify.Model
{
    public interface IRenderer
    {
        void Clear(Color color);
        void DrawLine(Point pt1, Point pt2, double lineWidth, Color color);
    }
}
