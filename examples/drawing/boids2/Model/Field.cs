using System;
using System.Threading.Tasks;

namespace Model
{
    public class Field
    {
        private readonly double Width;
        private readonly double Height;
        public readonly Boid[] Boids;

        public Field(double width, double height, int boidCount, int predatorCount, bool random)
        {
            Width = width;
            Height = height;
            Boids = new Boid[boidCount];
            RandomizeBoids(predatorCount, random);
        }

        private void RandomizeBoids(int predatorCount, bool random)
        {
            Random rand = random ? new Random() : new Random(0);
            for (int i = 0; i < Boids.Length; i++)
            {
                Boids[i] = new Boid(
                    x: rand.NextDouble() * Width,
                    y: rand.NextDouble() * Height,
                    xVel: (rand.NextDouble() - .5),
                    yVel: (rand.NextDouble() - .5),
                    targetSpeed: .5 + rand.NextDouble());
                if (i < predatorCount)
                {
                    Boids[i].IsPredator = true;
                    Boids[i].TargetSpeed += .5;
                }
            }
        }

        public double WeightFlock = 1.0;
        public double WeightAvoid = 1.0;
        public double WeightAlign = 1.0;
        public double Vision = 100;
        public double VisionRepel = 20;
        public void Advance(double stepSize = 1.0)
        {
            Parallel.ForEach(Boids, boid =>
                {
                    boid.FlockWithNeighbors(Boids, Vision, .0005 * WeightFlock);
                    boid.AvoidCloseBoids(Boids, VisionRepel, .005 * WeightAvoid);
                    boid.AlignWithNeighbors(Boids, Vision, .03 * WeightAlign);
                    boid.AvoidPredators(Boids, Vision, .002);
                    boid.AvoidWalls(Width, Height, Vision, .1);
                    boid.Advance(stepSize);
                }
            );
        }
    }
}
