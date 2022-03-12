using System.Drawing;

using Bitmap bmp = new(600, 400);
using Graphics gfx = Graphics.FromImage(bmp);
gfx.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

gfx.Clear(Color.Navy);

Random rand = new(0);
using Pen pen = new(Color.White);
for (int i = 0; i < 10_000; i++)
{
    pen.Color = Color.FromArgb(rand.Next());
    Point pt1 = new(rand.Next(bmp.Width), rand.Next(bmp.Height));
    Point pt2 = new(rand.Next(bmp.Width), rand.Next(bmp.Height));
    gfx.DrawLine(pen, pt1, pt2);
}

bmp.Save("demo.png");