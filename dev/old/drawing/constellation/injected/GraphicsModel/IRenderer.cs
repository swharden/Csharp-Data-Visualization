using System;
using System.Collections.Generic;
using System.Text;

namespace GraphicsModel
{
    public interface IRenderer : IDisposable
    {
        void Clear(Color color);
        void DrawLine(Point pt1, Point pt2, double lineWidth, Color color);
        void FillCircle(Point center, double radius, Color color);
    }
}
