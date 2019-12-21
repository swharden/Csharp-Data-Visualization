using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp39.Starfield
{
    public struct Star
    {
        public float X;
        public float Y;
        public float Distance;
        public float Size { get { return 50 / Distance; } }
    }

    public class Field
    {
        public Star[] stars;

        readonly int width, height;
        Random rand = new Random();

        public Field(int starCount, int width, int height)
        {
            this.width = width;
            this.height = height;
            stars = new Star[starCount];

            for (int i = 0; i < stars.Length; i++)
                stars[i] = RandomStar();
        }

        private Star RandomStar()
        {
            return new Star()
            {
                X = rand.Next(width),
                Y = rand.Next(height),
                Distance = rand.Next(10, 100)
            };
        }

        public void StepForward(float stepSize = 1)
        {
            for (int i = 0; i < stars.Length; i++)
            {
                stars[i].Y += stars[i].Size * stepSize;

                if (stars[i].Y >= height)
                {
                    stars[i].X = rand.Next(width);
                    stars[i].Y = 0;
                }
            }
        }
    }
}
