using System;
using System.Collections.Generic;
using System.Text;

namespace GraphicsModel
{
    public class Color
    {
        public readonly byte R, G, B, A;
        public Color(byte red, byte green, byte blue, byte alpha = 255)
        {
            (R, G, B, A) = (red, green, blue, alpha);
        }

        public Color(string hex, byte alpha = 255)
        {
            if ((hex.Length != 7) || (hex.Substring(0, 1) != "#"))
                throw new ArgumentException("invalid hex color string");

            R = Convert.ToByte(hex.Substring(1, 2), 16);
            G = Convert.ToByte(hex.Substring(3, 2), 16);
            B = Convert.ToByte(hex.Substring(5, 2), 16);
            A = alpha;
        }
    }
}
