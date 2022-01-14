using Microsoft.Maui.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GraphicsModelLibrary.BallPit;

public class BallPitModel : IGraphicsModel
{
    public float Width { get; set; }
    public float Height { get; set; }

    readonly Ball[] Balls;
    readonly Random Rand = new();

    public BallPitModel(int ballCount = 100)
    {
        Balls = Enumerable.Range(0, ballCount).Select(x => new Ball()).ToArray();
    }

    public void Reset(float width, float height)
    {
        Resize(width, height);
        RandomizeBalls();
    }

    public void Resize(float width, float height)
    {
        Width = width;
        Height = height;
    }

    private void RandomizeBalls()
    {
        foreach (Ball ball in Balls)
        {
            ball.X = (float)Rand.NextDouble() * Width;
            ball.Y = (float)Rand.NextDouble() * Height;
            ball.Radius = (float)Rand.NextDouble() * 10 + 5;

            double direction = Rand.NextDouble() * 2 * Math.PI;
            float speed = (float)Rand.NextDouble() * 2 + .5f;
            ball.XVel = (float)Math.Sin(direction) * speed;
            ball.YVel = (float)Math.Cos(direction) * speed;

            ball.Color = Color.FromHsv((float)Rand.NextDouble(), .5f, 1);
        }
    }

    public void Advance(float time = 1)
    {
        foreach (Ball ball in Balls)
        {
            ball.X += ball.XVel;
            ball.Y += ball.YVel;

            // left edge
            if (ball.X < ball.Radius)
            {
                ball.X += ball.Radius - ball.X;
                ball.XVel *= -1;
            }

            // right edge
            if (ball.X > Width - ball.Radius)
            {
                ball.X -= ball.X - Width + ball.Radius;
                ball.XVel *= -1;
            }

            // top edge
            if (ball.Y < ball.Radius)
            {
                ball.Y += ball.Radius - ball.Y;
                ball.YVel *= -1;
            }

            // bottom edge
            if (ball.Y > Height - ball.Radius)
            {
                ball.Y -= ball.Y - Height + ball.Radius;
                ball.YVel *= -1;
            }
        }
    }

    public void Draw(ICanvas canvas, float width, float height)
    {
        canvas.FillColor = Colors.Navy;
        canvas.FillRectangle(0, 0, width, height);

        foreach (Ball ball in Balls)
        {
            canvas.FillColor = ball.Color;
            canvas.FillCircle(ball.X, ball.Y, ball.Radius);
        }
    }

    public void Draw(ICanvas canvas, RectangleF dirtyRect)
    {
        Draw(canvas, dirtyRect.Width, dirtyRect.Height);
    }
}