using GraphicsModel;
using System;
using System.Drawing;

namespace Viewer
{
    class SystemDrawingRenderer : IRenderer
    {
        private readonly System.Drawing.Graphics Gfx;
        private System.Drawing.Pen Pen = new Pen(System.Drawing.Color.White);
        private System.Drawing.SolidBrush Brush = new SolidBrush(System.Drawing.Color.Black);

        public SystemDrawingRenderer(System.Drawing.Bitmap bmp)
        {
            Gfx = System.Drawing.Graphics.FromImage(bmp);
            Gfx.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
        }

        public void Dispose()
        {
            Pen.Dispose();
            Brush.Dispose();
            Gfx.Dispose();
        }

        private System.Drawing.PointF Convert(GraphicsModel.Point pt)
        {
            return new PointF((float)pt.X, (float)pt.Y);
        }

        private System.Drawing.Color Convert(GraphicsModel.Color color)
        {
            return System.Drawing.Color.FromArgb(color.A, color.R, color.G, color.B);
        }

        private System.Drawing.RectangleF Circle(GraphicsModel.Point center, double radius)
        {
            return new RectangleF(
                x: (float)(center.X - radius),
                y: (float)(center.Y - radius),
                width: (float)(radius * 2),
                height: (float)(radius * 2));
        }

        public void Clear(GraphicsModel.Color color)
        {
            Gfx.Clear(Convert(color));
        }

        public void DrawLine(GraphicsModel.Point pt1, GraphicsModel.Point pt2, double lineWidth, GraphicsModel.Color color)
        {
            Pen.Width = (float)lineWidth;
            Pen.Color = Convert(color);
            Gfx.DrawLine(Pen, Convert(pt1), Convert(pt2));
        }

        public void FillCircle(GraphicsModel.Point center, double radius, GraphicsModel.Color color)
        {
            Brush.Color = Convert(color);
            Gfx.FillEllipse(Brush, Circle(center, radius));
        }
    }
}
