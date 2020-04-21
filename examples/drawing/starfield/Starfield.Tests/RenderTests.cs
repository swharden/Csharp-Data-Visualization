using NUnit.Framework;
using System;
using System.Diagnostics;
using System.Drawing;

namespace Starfield.Tests
{
    public class RenderTests
    {
        [Test]
        public void Test_Field_Renders()
        {
            Bitmap bmp = new Bitmap(600, 400);
            var field = new Field(500);

            field.Render(bmp);

            string imageFilePath = System.IO.Path.GetFullPath("field_basic.bmp");
            bmp.Save(imageFilePath);
            Console.WriteLine($"Saved: {imageFilePath}");
        }

        [Test]
        public void Test_Field_Advances()
        {
            Bitmap bmp = new Bitmap(600, 400);
            var field = new Field(500);

            // primary render
            field.Render(bmp);
            string imageFilePath1 = System.IO.Path.GetFullPath("field_1.bmp");
            bmp.Save(imageFilePath1);
            Console.WriteLine($"Saved: {imageFilePath1}");

            // advance the model
            field.Advance();

            // secondary render
            field.Render(bmp);
            string imageFilePath2 = System.IO.Path.GetFullPath("field_2.bmp");
            bmp.Save(imageFilePath2);
            Console.WriteLine($"Saved: {imageFilePath2}");
        }

        [Test]
        public void Test_Star_Color()
        {
            Bitmap bmp = new Bitmap(600, 400);
            var field = new Field(500);

            field.Render(bmp, Color.Magenta);

            string imageFilePath = System.IO.Path.GetFullPath("field_magenta.bmp");
            bmp.Save(imageFilePath);
            Console.WriteLine($"Saved: {imageFilePath}");

            // launch the image in the default image viewer
            //Process.Start("explorer.exe", imageFilePath);
        }
    }
}