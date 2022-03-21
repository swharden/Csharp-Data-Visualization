using System;
using System.IO;

namespace ArrayToImage;

public static class Program
{
    public static void Main()
    {
        IGraphicsPlatform[] platforms = new IGraphicsPlatform[]
        {
            //new ImageMakers.SystemDrawingImageMaker(),
            new ImageMakers.SkiaSharpImageMaker(),
        };

        string outputFolder = Path.GetFullPath("./output");
        if (!Directory.Exists(outputFolder))
            Directory.CreateDirectory(outputFolder);

        Demonstrate.ImageToArray(platforms, outputFolder);
        //Demonstrate.ArrayToImage(platforms, outputFolder);
    }

}