using Mystify.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mystify.Viewer
{
    class DrawingRenderer : IRenderer
    {
        private readonly Graphics Gfx;

        // modify a pen and brush rather than frequently create/destroy them
        private readonly Pen Pen = new Pen(System.Drawing.Color.White);
        private readonly SolidBrush Brush = new SolidBrush(System.Drawing.Color.Black);

        public DrawingRenderer(Bitmap bmp)
        {
            Gfx = Graphics.FromImage(bmp);
            Gfx.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
        }

        public void Dispose()
        {
            Pen.Dispose();
            Brush.Dispose();
            Gfx.Dispose();
        }

        // helper function to convert a Point
        private PointF Convert(Model.Point pt)
        {
            return new PointF(pt.X, pt.Y);
        }

        // helper function to convert a Color
        private System.Drawing.Color Convert(Model.Color color)
        {
            return System.Drawing.Color.FromArgb(color.A, color.R, color.G, color.B);
        }

        public void Clear(Model.Color color)
        {
            Gfx.Clear(Convert(color));
        }

        public void DrawLine(Model.Point pt1, Model.Point pt2, double lineWidth, Model.Color color)
        {
            Pen.Width = (float)lineWidth;
            Pen.Color = Convert(color);
            Gfx.DrawLine(Pen, Convert(pt1), Convert(pt2));
        }
    }
}
