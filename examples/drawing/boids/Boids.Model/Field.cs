using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;

namespace Boids.Model
{
    public class Field
    {
        public double Width;
        public double Height;

        public readonly List<Boid> boids = new List<Boid>();
        private readonly Random rand = new Random();

        public Field(double width, double height, int boidCount = 100)
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

        public void AdvanceOld(double? width = null, double? height = null)
        {
            Width = (width is null) ? Width : width.Value;
            Height = (height is null) ? Height : height.Value;

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

        public (double dX, double dY) DeltaPosition(Boid boid, List<Boid> neighbors)
        {
            double centerX = 0;
            double centerY = 0;
            foreach (var neighbor in neighbors)
            {
                centerX += neighbor.X;
                centerY += neighbor.Y;
            }
            centerX /= neighbors.Count;
            centerY /= neighbors.Count;
            return (centerX - boid.X, centerY - boid.Y);
        }

        public (double dX, double dY) DeltaVelocity(Boid boid, List<Boid> neighbors)
        {
            double velX = 0;
            double velY = 0;
            foreach (var neighbor in neighbors)
            {
                velX += neighbor.Xvel;
                velY += neighbor.Yvel;
            }
            velX /= neighbors.Count;
            velY /= neighbors.Count;
            return (velX - boid.Xvel, velY - boid.Yvel);
        }

        public void Advance(double? newWidth = null, double? newHeight = null)
        {
            Width = (newWidth is null) ? Width : newWidth.Value;
            Height = (newHeight is null) ? Height : newHeight.Value;

            double distFlock = 200; // move toward center of flock
            double distAlign = 40; // align with mean direction
            double distAvoid = 20; // avoid

            // steer all boids based on rules
            foreach (var boid1 in boids)
            {
                /// CATEGORIZE NEIGHBORS BY DISTANCE
                List<Boid> boidsFlock = new List<Boid>();
                List<Boid> boidsAlign = new List<Boid>();
                List<Boid> boidsAvoid = new List<Boid>();
                foreach (var boid2 in boids)
                {
                    var distance = boid1.Distance(boid2);
                    if (distance < distFlock) boidsFlock.Add(boid2);
                    if (distance < distAlign) boidsAlign.Add(boid2);
                    if (distance < distAvoid) boidsAvoid.Add(boid2);
                }

                // AVOID CLOSE, FLOCK, ALIGN
                (double deltaAvoidX, double deltaAvoidY) = DeltaPosition(boid1, boidsAvoid);
                (double deltaFlockX, double deltaFlockY) = DeltaPosition(boid1, boidsFlock);
                (double deltaAlignX, double deltaAlignY) = DeltaVelocity(boid1, boidsAlign);
                boid1.Xvel += deltaFlockX * .1 - deltaAvoidX * .01 + deltaAlignX * .001;
                boid1.Yvel += deltaFlockY * .1 - deltaAvoidY * .01 + deltaAlignY * .001;

                // ACCELERATE TO tARGET SPEED
                double targetSpeed = 200;
                if (boid1.GetSpeed() < targetSpeed)
                    boid1.Accelerate(1.01);
                else
                    boid1.Accelerate(.99);

                // TURN FROM EDGE
                double pad = 50;
                if (boid1.X < pad) boid1.Xvel += 20;
                if (boid1.Y < pad) boid1.Yvel += 20;
                if (boid1.X > Width - pad) boid1.Xvel -= 20;
                if (boid1.Y > Height - pad) boid1.Yvel -= 20;

                // AVOID PREDATOR
                Boid predator = boids[0];
                if (boid1 != predator)
                {
                    double predatorDistance = distFlock / 2;
                    double distanceFromPredator = boid1.Distance(predator);
                    if (distanceFromPredator < predatorDistance)
                    {
                        double dX = predator.X - boid1.X;
                        double dY = predator.Y - boid1.Y;
                        boid1.Xvel -= dX * .7;
                        boid1.Yvel -= dY * .7;
                    }
                }
            }

            // move all boids forward
            double simulationSpeed = .01;
            foreach (var boid1 in boids)
            {
                boid1.X += boid1.Xvel * simulationSpeed;
                boid1.Y += boid1.Yvel * simulationSpeed;
            }

            // roll boids that fell off the page
            foreach (var boid1 in boids)
            {
                if (boid1.X < 0) boid1.X = Width;
                if (boid1.Y < 0) boid1.Y = Height;
                if (boid1.X > Width) boid1.X = 0;
                if (boid1.Y > Height) boid1.Y = 0;
            }
        }
    }
}
