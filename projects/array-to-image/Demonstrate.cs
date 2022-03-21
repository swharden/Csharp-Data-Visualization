using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArrayToImage;

public static class Demonstrate
{
    public static void ImageToArray(IGraphicsPlatform[] platforms, string outputFolder)
    {
        string testImagePath = Path.GetFullPath("../../../TestImages/bird.jpg");
        if (!File.Exists(testImagePath))
            throw new FileNotFoundException(testImagePath);

        foreach (IGraphicsPlatform platform in platforms)
        {
            // function tested
            byte[,,] pixelArray = platform.LoadImageRgb(testImagePath);

            // save the array as a new image to assess it
            string filename = $"{platform.Name} - ArrayToImage.png";
            string filePath = Path.Combine(outputFolder, filename);
            platform.SaveImageRgb(filePath, pixelArray);
        }
    }

    public static void ArrayToImage(IGraphicsPlatform[] platforms, string outputFolder)
    {
        ITestPattern[] patterns = new ITestPattern[]
        {
            new TestPatterns.HorizontalGradients(),
            new TestPatterns.VerticalGradients(),
            new TestPatterns.DiagnalGradients(),
            new TestPatterns.ColorStrips(),
        };

        // use prime dimensions to prevent accidental alignment
        int width = 653;
        int height = 487;

        foreach (IGraphicsPlatform platform in platforms)
        {
            foreach (ITestPattern pattern in patterns)
            {
                byte[,,] pixelArray = pattern.Generate(width, height);
                string filename = $"{platform.Name} - {pattern.Name}.png";
                string filePath = Path.Combine(outputFolder, filename);
                platform.SaveImageRgb(filePath, pixelArray);
                Console.WriteLine(filePath);
            }
        }
    }
}
