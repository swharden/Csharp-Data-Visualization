using System;

namespace Mystify.Model
{
    public class Polygon
    {
        private readonly Random rand = new Random();
        public readonly Corner[] Corners;
        public Polygon(int cornerCount, double width, double height)
        {
            if (cornerCount < 1)
                throw new ArgumentException("Polygons must have at least 2 corners");

            Corners = new Corner[cornerCount];
            for (int i = 0; i < Corners.Length; i++)
                Corners[i] = new Corner(rand, width, height);
        }

        public void Advance(double speed = 1)
        {
            foreach (var corner in Corners)
                corner.Advance(speed);
        }
    }
}
