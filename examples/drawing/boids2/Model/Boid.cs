using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;

namespace Model
{
    public class Boid
    {
        public Position Pos;
        public Velocity Vel;
        public double TargetSpeed = 1.0;
        public bool IsPredator;

        public Boid(double x, double y, double xVel, double yVel, double targetSpeed)
        {
            Pos = new Position(x, y);
            Vel = new Velocity(xVel, yVel);
            TargetSpeed = targetSpeed;
        }

        public void FlockWithNeighbors(Boid[] boids, double vision, double weight)
        {
            // predators have double distance
            if (IsPredator)
                vision *= 2;

            // determine mean position of the flock
            int neighborCount = 0;
            double centerX = 0;
            double centerY = 0;
            foreach (Boid boid in boids)
            {
                if (boid.Pos.Distance(Pos) < vision)
                {
                    centerX += boid.Pos.X;
                    centerY += boid.Pos.Y;
                    neighborCount += 1;
                }
            }
            centerX /= neighborCount;
            centerY /= neighborCount;

            // steer toward the flock
            Vel.X += (centerX - Pos.X) * weight;
            Vel.Y += (centerY - Pos.Y) * weight;
        }

        public void AvoidCloseBoids(Boid[] boids, double vision, double weight)
        {
            foreach (Boid boid in boids)
            {
                if (boid.IsPredator == IsPredator) // like avoids like
                {
                    double closeness = vision - boid.Pos.Distance(Pos);
                    if (closeness > 0)
                    {
                        // avoid with a magnitude correlated to closeness
                        Vel.X -= (boid.Pos.X - Pos.X) * weight * closeness;
                        Vel.Y -= (boid.Pos.Y - Pos.Y) * weight * closeness;
                    }
                }
            }
        }

        public void AlignWithNeighbors(Boid[] boids, double vision, double weight)
        {
            // determine mean velocity of the flock
            int neighborCount = 0;
            double meanVelX = 0;
            double meanVelY = 0;
            foreach (Boid boid in boids)
            {
                if (boid.Pos.Distance(Pos) < vision)
                {
                    meanVelX += boid.Vel.X;
                    meanVelY += boid.Vel.Y;
                    neighborCount += 1;
                }
            }
            meanVelX /= neighborCount;
            meanVelY /= neighborCount;

            // steer toward the mean flock velocity
            Vel.X -= (Vel.X - meanVelX) * weight;
            Vel.Y -= (Vel.Y - meanVelY) * weight;
        }

        public void AvoidPredators(Boid[] boids, double vision, double weight)
        {
            for (int i = 0; i < boids.Length; i++)
            {
                Boid boid = boids[i];
                if (boid.IsPredator && boid.Pos.Distance(Pos) < vision)
                {
                    if (boid.Pos.Distance(Pos) < vision)
                    {
                        // steer away regardless of distance
                        Vel.X -= (boid.Pos.X - Pos.X) * weight;
                        Vel.Y -= (boid.Pos.Y - Pos.Y) * weight;
                    }
                }
            }
        }

        public void AvoidWalls(double width, double height, double pad, double turn)
        {
            if (Pos.X < pad) Vel.X += turn;
            if (Pos.Y < pad) Vel.Y += turn;
            if (Pos.X > width - pad) Vel.X -= turn;
            if (Pos.Y > height - pad) Vel.Y -= turn;
        }

        private readonly int positionsToRemember = 20;
        public List<Position> positions = new List<Position>();
        public void Advance(double stepSize)
        {
            Vel.SetSpeed(TargetSpeed);
            Pos.Move(Vel, stepSize);

            positions.Add(Pos);
            while (positions.Count > positionsToRemember)
                positions.RemoveAt(0);
        }
    }
}
