using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MystifyBlazor.Models
{
    public class Color
    {
        public double Hue;

        public Color(double hueFrac = .5) =>
            Hue = hueFrac;

        public override string ToString() =>
            ColorFromHSV(Hue * 360);

        /// <summary>
        /// Convert a hue (on 360º scale) to RGB hex string
        /// </summary>
        private static string ColorFromHSV(double hue, double saturation = 1, double value = 1)
        {
            hue %= 360;
            int hi = Convert.ToInt32(Math.Floor(hue / 60)) % 6;
            double f = hue / 60 - Math.Floor(hue / 60);

            value *= 255;
            int v = Convert.ToInt32(value);
            int p = Convert.ToInt32(value * (1 - saturation));
            int q = Convert.ToInt32(value * (1 - f * saturation));
            int t = Convert.ToInt32(value * (1 - (1 - f) * saturation));

            static string Hex(int r, int g, int b) => $"#{r:X2}{g:X2}{b:X2}";

            if (hi == 0)
                return Hex(v, t, p);
            else if (hi == 1)
                return Hex(q, v, p);
            else if (hi == 2)
                return Hex(p, v, t);
            else if (hi == 3)
                return Hex(p, q, v);
            else if (hi == 4)
                return Hex(t, p, v);
            else
                return Hex(v, p, q);
        }
    }
}
