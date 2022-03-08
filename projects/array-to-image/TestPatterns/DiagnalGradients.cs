using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArrayToImage.TestPatterns;

public class DiagnalGradients : ITestPattern
{
    public string Name => "Diagnal Gradients";

    public byte[,,] Generate(int width, int height)
    {
        int channels = 3;
        byte[,,] pixelArray = new byte[height, width, channels];

        for (int y = 0; y < pixelArray.GetLength(0); y++)
        {
            for (int x = 0; x < pixelArray.GetLength(1); x++)
            {
                byte pixelValue = (byte)((x + y) * 2);
                pixelArray[y, x, 0] = pixelValue; // red
                pixelArray[y, x, 1] = pixelValue; // green
                pixelArray[y, x, 2] = pixelValue; // blue
            }
        }

        return pixelArray;
    }
}
