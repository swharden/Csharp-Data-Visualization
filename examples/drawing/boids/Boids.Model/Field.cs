using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Boids.Model
{
    public class Field
    {
        private double Width;
        private double Height;

        public readonly List<Boid> boids = new List<Boid>();
        private readonly Random rand = new Random();

        public Field(double width, double height, int boidCount)
        {
            Width = width;
            Height = height;
            for (int i = 0; i < boidCount; i++)
                boids.Add(RandomBoid());
        }

        private Boid RandomBoid()
        {
            return new Boid(
                x: Width * rand.NextDouble(),
                y: Height * rand.NextDouble(),
                xVel: (rand.NextDouble() - .5) * 10,
                yVel: (rand.NextDouble() - .5) * 10);
        }

        public void Resize(double width, double height)
        {
            Width = width;
            Height = height;
        }

        public void Advance()
        {
            double closeDistance = Width / 20;
            double flockDistance = Width / 5;
            double maxSpeed = Width / 250;
            double minSpeed = Width / 500;

            // modify boid1 to react to boid 2
            foreach (var boid1 in boids)
            {
                foreach (var boid2 in boids)
                {
                    if (boid1.Y >= boid2.Y)
                        continue;

                    double dX = boid2.X - boid1.X;
                    double dY = boid2.Y - boid1.Y;
                    double dXvel = boid2.Xvel - boid1.Xvel;
                    double dYvel = boid2.Yvel - boid1.Yvel;

                    double distance = Math.Sqrt(dX * dX + dY * dY);
                    bool tooClose = (distance < closeDistance);
                    bool sameFlock = (distance < flockDistance);

                    // move away from boids that are too close
                    if (tooClose)
                    {
                        double closeness = (1 - distance / closeDistance);
                        double repulsion = .01;
                        boid1.Xvel -= dX * closeness * repulsion;
                        boid1.Yvel -= dY * closeness * repulsion;
                    }

                    // move toward birds within the flock distance
                    if (sameFlock)
                    {
                        double flockStrength = .0002;
                        boid1.Xvel += dX * flockStrength;
                        boid1.Yvel += dY * flockStrength;
                    }

                    // velocity is influenced by other boids in the flock
                    if (sameFlock)
                    {
                        double flockStrength = .001;
                        boid1.Xvel += dXvel * flockStrength;
                        boid1.Yvel += dYvel * flockStrength;
                    }

                    // random velocity adjustment
                    //double randomness = .02;
                    //boid1.Xvel += (rand.NextDouble() - .5) * randomness;
                    //boid1.Yvel += (rand.NextDouble() - .5) * randomness;
                }

                // apply speed limits
                var speed = boid1.GetSpeed();
                if (speed > maxSpeed)
                {
                    boid1.Xvel *= (maxSpeed / speed);
                    boid1.Yvel *= (maxSpeed / speed);
                }
                if (speed < minSpeed)
                {
                    boid1.Xvel *= (minSpeed / speed);
                    boid1.Yvel *= (minSpeed / speed);
                }
            }

            // move all boids forward
            foreach (var boid in boids)
            {
                (double x, double y) = boid.FuturePosition(.5);
                boid.X = x;
                boid.Y = y;

                if (boid.X < 0) boid.X = Width + boid.X;
                if (boid.Y < 0) boid.Y = Height + boid.Y;
                if (boid.X > Width) boid.X -= Width;
                if (boid.Y > Height) boid.Y -= Height;
            }
        }
    }
}
