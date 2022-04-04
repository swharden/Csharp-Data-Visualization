using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoMaker.GraphicsModels
{
    public class BallField : IGraphicsModel
    {
        private readonly List<Ball> Balls = new();
        public SKBitmap Bitmap { get; private set; }
        public SKCanvas Canvas { get; private set; }
        public int Width => Bitmap.Width;
        public int Height => Bitmap.Height;
        private int FrameCount = 0;

        public BallField(int width, int height, int ballCount)
        {
            Bitmap = new SKBitmap(width, height);
            Canvas = new SKCanvas(Bitmap);
            Reset(ballCount);
        }

        public void Dispose()
        {
            Canvas.Dispose();
            Bitmap.Dispose();
        }

        public void Reset(int count)
        {
            Random rand = new();
            Balls.Clear();
            for (int i = 0; i < count; i++)
            {
                Ball p = new()
                {
                    X = (float)rand.NextDouble() * Width,
                    Y = (float)rand.NextDouble() * Height,
                    VelocityX = (float)(rand.NextDouble() - .5) * 5 + 1,
                    VelocityY = (float)(rand.NextDouble() - .5) * 5 + 1,
                    Radius = (float)rand.NextDouble() * 10 + 3,
                    Color = new SKColor(
                        red: (byte)rand.Next(255),
                        green: (byte)rand.Next(255),
                        blue: (byte)rand.Next(255),
                        alpha: 255),
                };

                Balls.Add(p);
            }
        }

        public void Advance(float delta)
        {
            Balls.ForEach(x => x.Advance(delta, Width, Height));
        }

        public void Draw()
        {
            Canvas.Clear(SKColor.Parse("#003366"));

            using SKPaint ballPaint = new()
            {
                Style = SKPaintStyle.Fill,
                IsAntialias = true,
            };

            foreach (Ball ball in Balls)
            {
                ballPaint.Color = ball.Color;
                SKPoint pt = new(ball.X, ball.Y);
                Canvas.DrawCircle(pt, ball.Radius, ballPaint);
            }

            using SKFont textFont = new(SKTypeface.FromFamilyName("consolas"), size: 32);
            using SKPaint textPaint = new(textFont)
            {
                Color = SKColors.Yellow,
                TextAlign = SKTextAlign.Left
            };
            Canvas.DrawText($"Frame {++FrameCount}", 10, 30, textPaint);
        }
    }
}
