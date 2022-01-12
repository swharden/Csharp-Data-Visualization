using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiGraphicsApp
{
    public class TestDrawable : IDrawable
    {
        public Random Rand = new();

        public void Draw(ICanvas canvas, RectangleF dirtyRect)
        {
            StandardGraphics.Render.TestImage(Rand, canvas, dirtyRect.Width, dirtyRect.Height);
        }
    }
}
