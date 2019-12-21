using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TitleScreenGDI.Animations
{
    public static class RenderGDI
    {
        public static Bitmap Render(CirclesAndLines stuff, Bitmap bmp)
        {
            Graphics gfx = Graphics.FromImage(bmp);
            gfx.Clear(Color.DarkBlue);
            gfx.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            foreach (var circle in stuff.circles)
            {
                // draw lines to every other circle
                int brightestLineAlpha = 0;
                foreach (var circle2 in stuff.circles)
                {
                    if (circle2.Location == circle.Location)
                        continue;

                    var line = new Line(circle.Location, circle2.Location);
                    float lineLength = line.length / 2;
                    int alpha = (lineLength > 255) ? 0 : 255 - (int)lineLength;
                    brightestLineAlpha = Math.Max(brightestLineAlpha, alpha);

                    Pen linePen = new Pen(Color.FromArgb(alpha, 255, 255, 255));
                    gfx.DrawLine(linePen, circle.Location, circle2.Location);
                }


                // draw circle
                int circleSize = 5;
                Size size = new Size(circleSize, circleSize);
                Point location = circle.Location;
                location.X -= circleSize / 2;
                location.Y -= circleSize / 2;
                Rectangle rect = new Rectangle(location, size);
                Brush circleBrush = new SolidBrush(Color.FromArgb(brightestLineAlpha, 255, 255, 255));
                gfx.FillEllipse(circleBrush, rect);

            }

            return bmp;
        }
    }
}
