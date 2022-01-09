using GraphicsModel;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewerGL
{
    class SkiaSharpRenderer : IRenderer
    {
        readonly SKCanvas Canvas;
        private SKPaint Paint;

        public SkiaSharpRenderer(SKCanvas canvas)
        {
            Canvas = canvas;
            Paint = new SKPaint() { IsAntialias = true };
        }

        public void Dispose()
        {
            Paint.Dispose();
        }

        private SKPoint Convert(GraphicsModel.Point pt)
        {
            return new SKPoint((float)pt.X, (float)pt.Y);
        }

        private SKColor Convert(GraphicsModel.Color color)
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
            Canvas.DrawLine(Convert(pt1), Convert(pt2), Paint);
        }

        public void FillCircle(Point center, double radius, Color color)
        {
            Paint.Color = Convert(color);
            Canvas.DrawCircle(Convert(center), (float)radius, Paint);
        }
    }
}
