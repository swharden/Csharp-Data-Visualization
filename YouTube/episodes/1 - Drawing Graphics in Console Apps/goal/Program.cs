using System;
using System.Drawing;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Random rand = new Random();

            Bitmap bmp = new Bitmap(800, 600);
            using (Graphics gfx = Graphics.FromImage(bmp))
            {
                gfx.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                gfx.Clear(Color.DarkBlue);
                Color myColor = Color.FromArgb(123, Color.White);
                Pen myPen = new Pen(myColor);
                //myPen.Width = 10;

                for (int i=0; i<100; i++)
                {
                    Point ptA = new Point(rand.Next(bmp.Width), rand.Next(bmp.Height));
                    Point ptB = new Point(rand.Next(bmp.Width), rand.Next(bmp.Height));
                    gfx.DrawLine(myPen, ptA, ptB);
                }
            }

            string filename = System.IO.Path.GetFullPath("test.bmp");
            bmp.Save(filename);
            Console.WriteLine($"Saved: {filename}");
        }
    }
}
