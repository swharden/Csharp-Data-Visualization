using System;
using System.Drawing;

namespace DrawingQuickstartConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Random rand = new Random();
            using (var bmp = new Bitmap(600, 400))
            using (var gfx = Graphics.FromImage(bmp))
            using (var pen = new Pen(Color.White))
            {
                gfx.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                gfx.Clear(Color.Navy);
                for (int i = 0; i < 10_000; i++)
                {
                    pen.Color = Color.FromArgb(rand.Next());
                    var pt1 = new Point(rand.Next(bmp.Width), rand.Next(bmp.Height));
                    var pt2 = new Point(rand.Next(bmp.Width), rand.Next(bmp.Height));
                    gfx.DrawLine(pen, pt1, pt2);
                }
                bmp.Save("drawing-quickstart-console.png");
            }
        }
    }
}
