using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TitleScreenGDI.Animations
{
    public class CirclesAndLines
    {
        public readonly float width, height;
        readonly int circleCount;

        public readonly List<Circle> circles = new List<Circle>();

        Random rand = new Random();

        public CirclesAndLines(float width, float height, int circleCount = 10)
        {
            this.width = width;
            this.height = height;
            this.circleCount = circleCount;

            while (circles.Count < circleCount)
                circles.Add(GetNewCircleOnEdge());
        }

        public void Iterate(double multiplier = 1)
        {
            for (int i=0; i<circles.Count; i++)
            {
                var circle = circles[i];
                circle.X += (float)(circle.velX * multiplier);
                circle.Y += (float)(circle.velY * multiplier);

                if (circle.X < 0 || circle.X > width || circle.Y < 0 || circle.Y > height)
                    circles[i] = GetNewCircleOnEdge();
            }
        }

        private Circle GetNewCircleOnEdge()
        {
            Circle circle = new Circle
            {
                X = (float)rand.NextDouble() * width,
                Y = (float)rand.NextDouble() * height
            };

            // place the circle on an edge
            if (rand.NextDouble() < .5)
            {
                circle.Y = (rand.NextDouble() < .5) ? 0 : height;
            }
            else
            {
                circle.X = (rand.NextDouble() < .5) ? 0 : width;
            }

            // start with a random velocity
            circle.velX = rand.NextDouble();
            circle.velY = rand.NextDouble();

            // invert velocity polarity based on which edge the circle is on
            if (circle.X != 0) circle.velX *= -1;
            if (circle.Y != 0) circle.velY *= -1;

            return circle;
        }
    }
}
