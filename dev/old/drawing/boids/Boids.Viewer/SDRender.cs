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
                gfx.Clear(ColorTranslator.FromHtml("#003366"));
                for (int i = 0; i < field.Boids.Count(); i++)
                {
                    if (i < 3)
                        RenderBoid(gfx, field.Boids[i], Color.White);
                    else
                        RenderBoid(gfx, field.Boids[i], Color.LightGreen);
                }
            }
            return bmp;
        }

        private static void RenderBoid(Graphics gfx, Boid boid, Color color)
        {
            var boidOutline = new Point[]
            {
                new Point(0, 0),
                new Point(-4, -1),
                new Point(0, 8),
                new Point(4, -1),
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
