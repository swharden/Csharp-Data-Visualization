using Boids.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boids.Viewer
{
    public static class SDRender
    {
        public static Bitmap RenderField(Field field)
        {
            Bitmap bmp = new Bitmap((int)field.Width, (int)field.Height);
            using (Graphics gfx = Graphics.FromImage(bmp))
            {
                gfx.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                //gfx.Clear(ColorTranslator.FromHtml("#2f3539"));
                gfx.Clear(ColorTranslator.FromHtml("#003366"));

                for (int i=0; i<field.boids.Count; i++)
                {
                    Color color = (i == 0) ? Color.White : Color.LightGreen;
                    RenderBoid(gfx, field.boids[i], color);
                }
            }
            return bmp;
        }

        private static void RenderBoid(Graphics gfx, Boid boid, Color color)
        {
            var boidOutline = new Point[]
            {
                new Point(0, 0),
                new Point(-5, -1),
                new Point(0, 10),
                new Point(5, -1),
                new Point(0, 0),
            };

            using (var brush = new SolidBrush(color))
            {
                gfx.TranslateTransform((float)boid.X, (float)boid.Y);
                gfx.RotateTransform((float)boid.GetAngle());
                gfx.FillClosedCurve(brush, boidOutline);
                gfx.ResetTransform();
            }
        }
    }
}
