using Microsoft.Maui.Graphics;

namespace GraphicsModelLibrary.BallPit;

internal record Ball
{
    public float X;
    public float XVel;
    public float Y;
    public float YVel;
    public float Radius;
    public Color Color = Colors.Magenta;
}