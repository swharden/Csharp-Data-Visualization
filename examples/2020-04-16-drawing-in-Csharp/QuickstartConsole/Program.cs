using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace QuickstartConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Random rand = new Random();

            using (var brush = new SolidBrush(Color.White))
            using (var pen = new Pen(brush))
            using (var bmp = new Bitmap(600, 400))
            using (var gfx = Graphics.FromImage(bmp))
            {
                gfx.SmoothingMode = SmoothingMode.AntiAlias;

                gfx.Clear(Color.Navy);
                for (int i = 0; i < 1000; i++)
                {
                    gfx.DrawLine(pen: pen,
                        x1: rand.Next(bmp.Width),
                        y1: rand.Next(bmp.Height),
                        x2: rand.Next(bmp.Width),
                        y2: rand.Next(bmp.Height));
                }

                string saveFilePath = System.IO.Path.GetFullPath("console.png");
                bmp.Save(saveFilePath, ImageFormat.Png);
                Console.WriteLine($"Saved: {saveFilePath}");
            }

            Console.WriteLine("press ENTER to exit...");
            System.Console.ReadLine();
        }
    }
}
