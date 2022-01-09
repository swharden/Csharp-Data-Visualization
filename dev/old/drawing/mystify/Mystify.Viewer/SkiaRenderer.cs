using Mystify.Model;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mystify.Viewer
{
    class SkiaRenderer : IRenderer
    {
        readonly SKCanvas Canvas;
        private readonly SKPaint Paint;

        public SkiaRenderer(SKCanvas canvas)
        {
            Canvas = canvas;
            Paint = new SKPaint() { IsAntialias = true };
        }

        public void Dispose()
        {
            Paint.Dispose();
        }

        private SKPoint Convert(Point pt)
        {
            return new SKPoint(pt.X, pt.Y);
        }

        private SKColor Convert(Color color)
        {
            return new SKColor(color.R, color.G, color.B, color.A);
        }

        public void Clear(Color color)
        {
            Canvas.Clear(Convert(color));
        }

        public void DrawLine(Point pt1, Point pt2, double lineWidth, Color color)
        {
            Paint.Color = Convert(color);
            Paint.StrokeWidth = (float)lineWidth;
            Canvas.DrawLine(Convert(pt1), Convert(pt2), Paint);
        }
    }
}
