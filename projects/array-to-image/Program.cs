using System;
using System.IO;

namespace ArrayToImage;

public static class Program
{
    public static void Main()
    {
        IImageMaker[] makers = new IImageMaker[]
        {
            new ImageMakers.SystemDrawingImageMaker(),
            new ImageMakers.SkiaSharpImageMaker(),
        };

        ITestPattern[] patterns = new ITestPattern[]
        {
            new TestPatterns.HorizontalGradients(),
            new TestPatterns.VerticalGradients(),
            new TestPatterns.DiagnalGradients(),
            new TestPatterns.ColorStrips(),
        };

        string outputFolder = Path.GetFullPath("./output");
        if (!Directory.Exists(outputFolder))
            Directory.CreateDirectory(outputFolder);

        // use prime dimensions to prevent accidental alignment
        int width = 653;
        int height = 487;

        foreach (IImageMaker maker in makers)
        {
            foreach (ITestPattern pattern in patterns)
            {
                byte[,,] pixelArray = pattern.Generate(width, height);
                string filename = $"{maker.Name} - {pattern.Name}.png";
                string filePath = Path.Combine(outputFolder, filename);
                maker.SaveImageRgb(filePath, pixelArray);
                Console.WriteLine(filePath);
            }
        }
    }
}