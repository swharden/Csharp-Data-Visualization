using System;
using System.Collections.Generic;
using System.Linq;

namespace BlazorBoids.Models
{
    public class Field
    {
        public double Width { get; private set; }
        public double Height { get; private set; }
        public readonly List<Boid> Boids = new List<Boid>();
        public int PredatorCount;
        private readonly Random Rand = new Random();

        public Field(double width, double height, int boidCount, int predatorCount)
        {
            (Width, Height) = (width, height);
            for (int i = 0; i < boidCount; i++)
                Boids.Add(new Boid(Rand, width, height));
            PredatorCount = predatorCount;
        }

        public void Resize(double width, double height) => (Width, Height) = (width, height);

        public void Advance(bool bounceOffWalls = true, bool wrapAroundEdges = false)
        {
            // update void speed and direction (velocity) based on rules
            foreach (var boid in Boids)
            {
                (double flockXvel, double flockYvel) = Flock(boid, 50, .0003);
                (double alignXvel, double alignYvel) = Align(boid, 50, .01);
                (double avoidXvel, double avoidYvel) = Avoid(boid, 20, .001);
                (double predXvel, double predYval) = Predator(boid, 150, .00005);
                boid.Xvel += flockXvel + avoidXvel + alignXvel + predXvel;
                boid.Yvel += flockYvel + avoidYvel + alignYvel + predYval;
            }

            // move all boids forward in time
            foreach (var boid in Boids)
            {
                boid.MoveForward();
                if (bounceOffWalls)
                    BounceOffWalls(boid);
                if (wrapAroundEdges)
                    WrapAround(boid);
            }
        }

        private (double xVel, double yVel) Flock(Boid boid, double distance, double power)
        {
            // point toward the center of the flock (mean flock boid position)
            var neighbors = Boids.Where(x => x.GetDistance(boid) < distance);
            double meanX = neighbors.Sum(x => x.X) / neighbors.Count();
            double meanY = neighbors.Sum(x => x.Y) / neighbors.Count();
            double deltaCenterX = meanX - boid.X;
            double deltaCenterY = meanY - boid.Y;
            return (deltaCenterX * power, deltaCenterY * power);
        }

        private (double xVel, double yVel) Avoid(Boid boid, double distance, double power)
        {
            // point away as boids get close
            var neighbors = Boids.Where(x => x.GetDistance(boid) < distance);
            (double sumClosenessX, double sumClosenessY) = (0, 0);
            foreach (var neighbor in neighbors)
            {
                double closeness = distance - boid.GetDistance(neighbor);
                sumClosenessX += (boid.X - neighbor.X) * closeness;
                sumClosenessY += (boid.Y - neighbor.Y) * closeness;
            }
            return (sumClosenessX * power, sumClosenessY * power);
        }

        private (double xVel, double yVel) Predator(Boid boid, double distance, double power)
        {
            // point away as predators get close
            (double sumClosenessX, double sumClosenessY) = (0, 0);
            for (int i = 0; i < PredatorCount; i++)
            {
                Boid predator = Boids[i];
                double distanceAway = boid.GetDistance(predator);
                if (distanceAway < distance)
                {
                    double closeness = distance - distanceAway;
                    sumClosenessX += (boid.X - predator.X) * closeness;
                    sumClosenessY += (boid.Y - predator.Y) * closeness;
                }
            }
            return (sumClosenessX * power, sumClosenessY * power);
        }

        private (double xVel, double yVel) Align(Boid boid, double distance, double power)
        {
            // point toward the center of the flock (mean flock boid position)
            var neighbors = Boids.Where(x => x.GetDistance(boid) < distance);
            double meanXvel = neighbors.Sum(x => x.Xvel) / neighbors.Count();
            double meanYvel = neighbors.Sum(x => x.Yvel) / neighbors.Count();
            double dXvel = meanXvel - boid.Xvel;
            double dYvel = meanYvel - boid.Yvel;
            return (dXvel * power, dYvel * power);
        }

        private void BounceOffWalls(Boid boid)
        {
            double pad = 50;
            double turn = .5;
            if (boid.X < pad)
                boid.Xvel += turn;
            if (boid.X > Width - pad)
                boid.Xvel -= turn;
            if (boid.Y < pad)
                boid.Yvel += turn;
            if (boid.Y > Height - pad)
                boid.Yvel -= turn;
        }

        private void WrapAround(Boid boid)
        {
            if (boid.X < 0)
                boid.X += Width;
            if (boid.X > Width)
                boid.X -= Width;
            if (boid.Y < 0)
                boid.Y += Height;
            if (boid.Y > Height)
                boid.Y -= Height;
        }
    }
}
