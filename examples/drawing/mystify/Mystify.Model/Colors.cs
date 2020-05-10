using System;
using System.Collections.Generic;
using System.Text;

namespace Mystify.Model
{
    public static class Colors
    {
        public static Color RandomColor(Random rand, int minAlpha = 255)
        {
            return new Color(
                red: (byte)rand.Next(255),
                green: (byte)rand.Next(255),
                blue: (byte)rand.Next(255),
                alpha: (byte)rand.Next(minAlpha));
        }

        // input values can be 0-256*3 and RGB sum will be 255
        public static Color FromHue(int hue, bool brighten = true)
        {
            hue %= (256 * 3);

            double r, g, b;

            if (hue < 256)
            {
                // falling red, rising green, no blue
                double fracIntoA = (hue - 256 * 0) / (256d);
                r = 255 * (1 - fracIntoA);
                g = 255 * fracIntoA;
                b = 0;
            }
            else if (hue < 512)
            {
                // no red, falling green, rising blue
                double fracIntoB = (hue - 256 * 1) / (256d);
                r = 0;
                g = 255 * (1 - fracIntoB);
                b = 255 * fracIntoB;
            }
            else
            {
                // rising red, no green, falling blue
                double fracIntoC = (hue - 256 * 2) / (256d);
                r = 255 * fracIntoC;
                g = 0;
                b = 255 * (1 - fracIntoC);
            }

            if (brighten)
            {
                r += (255 - r) / 2;
                g += (255 - g) / 2;
                b += (255 - b) / 2;
            }

            return new Color((byte)r, (byte)g, (byte)b, 255);
        }

        public static Color RandomBrightColor(Random rand)
        {
            return FromHue(rand.Next(256 * 3));
        }
    }
}
