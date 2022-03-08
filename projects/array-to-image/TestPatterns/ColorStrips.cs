using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArrayToImage.TestPatterns;

public class ColorStrips : ITestPattern
{
    public string Name => "Color Strips";

    public byte[,,] Generate(int width, int height)
    {
        int channels = 3;
        byte[,,] pixelArray = new byte[height, width, channels];

        int period = 150;
        int thickness = 60;

        for (int y = 0; y < pixelArray.GetLength(0); y++)
        {
            for (int x = 0; x < pixelArray.GetLength(1); x++)
            {
                if ((x + y) % period < thickness)
                    pixelArray[y, x, 0] = 255; // red

                if (y % period < thickness)
                    pixelArray[y, x, 1] = 255; // green

                if (x % period < thickness)
                    pixelArray[y, x, 2] = 255; // blue
            }
        }

        return pixelArray;
    }
}
