using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mystify.Model
{
    public static class Field
    {
        public static void Render(IRenderer rend, Polygon[] polys, float width, bool fade)
        {
            rend.Clear(new Color(0, 0, 0));

            foreach (Polygon poly in polys)
            {
                for (int historyIndex = 0; historyIndex < poly.Corners[0].Points.Count(); historyIndex++)
                {
                    var historyFrac = (double)historyIndex / (poly.Corners[0].Points.Count() - 1);

                    byte alpha = (fade) ? (byte)(255 * historyFrac) : poly.Color.A;
                    Color color = new Color(poly.Color.R, poly.Color.G, poly.Color.B, alpha);

                    for (int cornerIndex = 0; cornerIndex < poly.Corners.Count(); cornerIndex++)
                    {
                        var cornerA = poly.Corners[cornerIndex];
                        var pointA = new Point(cornerA.Points[historyIndex].X, cornerA.Points[historyIndex].Y);

                        var cornerB = poly.Corners[(cornerIndex == poly.Corners.Count() - 1) ? 0 : cornerIndex + 1];
                        var pointB = new Point(cornerB.Points[historyIndex].X, cornerB.Points[historyIndex].Y);

                        rend.DrawLine(pointA, pointB, width, color);
                    }
                }
            }
        }
    }
}
