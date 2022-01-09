using System;

namespace Mystify.Model
{
    public class Polygon
    {
        private int Hue;
        public Color Color;
        public readonly Corner[] Corners;

        public Polygon(Random rand, int cornerCount, double width, double height, int pointsToKeep, double? colorFrac = null)
        {
            if (cornerCount < 1)
                throw new ArgumentException("Polygons must have at least 2 corners");

            Hue = (colorFrac is null) ? rand.Next(256 * 3) : (int)(colorFrac * (256 * 3));
            Color = Colors.FromHue(Hue);
            Corners = new Corner[cornerCount];
            for (int i = 0; i < Corners.Length; i++)
                Corners[i] = new Corner(rand, width, height, pointsToKeep);
        }

        public void Advance(double speed = 1, bool shiftHue = false)
        {
            foreach (var corner in Corners)
                corner.Advance(speed);
            if (shiftHue)
            {
                Hue += 2;
                Color = Colors.FromHue(Hue);
            }
        }
    }
}
