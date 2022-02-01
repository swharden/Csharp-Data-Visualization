using Microsoft.Maui.Graphics;
using Microsoft.Maui.Graphics.Skia;

namespace RotatedRectangleDemo;

public partial class Form1 : Form
{
    Microsoft.Maui.Graphics.PointF MousePosition;

    public Form1()
    {
        InitializeComponent();
        GraphicsPlatform.RegisterGlobalService(SkiaGraphicsService.Instance);
        skglControl1_SizeChanged(null, null);
    }

    private void skglControl1_SizeChanged(object sender, EventArgs e)
    {
        nudX.Maximum = skglControl1.Width;
        nudX.Value = skglControl1.Width / 2;
        nudY.Maximum = skglControl1.Height;
        nudY.Value = skglControl1.Height / 2;
    }

    private void trackbarAngle_Scroll(object sender, EventArgs e) => skglControl1.Invalidate();

    private void trackbarDistance_Scroll(object sender, EventArgs e) => skglControl1.Invalidate();

    private void nudX_ValueChanged(object sender, EventArgs e) => skglControl1.Invalidate();

    private void nudY_ValueChanged(object sender, EventArgs e) => skglControl1.Invalidate();

    private void tbText_TextChanged(object sender, EventArgs e) => skglControl1.Invalidate();

    private void skglControl1_MouseMove(object sender, MouseEventArgs e)
    {
        MousePosition.X = e.X;
        MousePosition.Y = e.Y;
        skglControl1.Invalidate();
    }

    private void skglControl1_MouseLeave(object sender, EventArgs e)
    {
        MousePosition.X = float.NaN;
        MousePosition.Y = float.NaN;
        skglControl1.Invalidate();
    }

    private Microsoft.Maui.Graphics.PointF Rotate(Microsoft.Maui.Graphics.PointF origin, Microsoft.Maui.Graphics.PointF point, double theta)
    {
        double dx = (point.X - origin.X);
        double dy = (point.Y - origin.Y);
        double dx2 = point.X * Math.Cos(theta) - point.Y * Math.Sin(theta);
        double dy2 = point.X * Math.Sin(theta) + point.Y * Math.Cos(theta);
        return new Microsoft.Maui.Graphics.PointF(origin.X + (float)dx2, origin.Y + (float)dy2);
    }

    private void skglControl1_PaintSurface(object sender, SkiaSharp.Views.Desktop.SKPaintGLSurfaceEventArgs e)
    {
        double angleFraction = trackbarAngle.Value / (double)trackbarAngle.Maximum;
        double angleRadians = 2 * Math.PI * angleFraction;
        float angleDegrees = (float)(angleRadians * 180 / Math.PI);
        float fontSize = 96;
        string fontName = "Impact";

        ICanvas canvas = new SkiaCanvas() { Canvas = e.Surface.Canvas };
        canvas.FillColor = Colors.Navy;
        canvas.FillRectangle(0, 0, skglControl1.Width, skglControl1.Height);

        Microsoft.Maui.Graphics.SizeF stringSize = GraphicsPlatform.CurrentService.GetStringSize(tbText.Text, fontName, fontSize);
        float rectWidth = stringSize.Width;
        float rectHeight = stringSize.Height;

        Microsoft.Maui.Graphics.PointF[] rectCorners =
        {
            new(0, 0),
            new(rectWidth, 0),
            new(rectWidth, rectHeight),
            new(0, rectHeight),
        };

        Microsoft.Maui.Graphics.PointF origin = new((float)nudX.Value, (float)nudY.Value);
        Microsoft.Maui.Graphics.PointF[] rotatedCorners = rectCorners.Select(x => Rotate(origin, x, angleRadians)).ToArray();
        bool mouseIsInRectangle = IsPointInsideRectangle(MousePosition, rotatedCorners);

        // mouse position
        canvas.FillColor = Colors.Yellow;
        canvas.FillCircle(MousePosition, 5);

        // draw the rotated rectangle and string using translation and rotation
        canvas.Translate(origin.X, origin.Y);
        canvas.Rotate(angleDegrees);

        // rectangle
        canvas.FillColor = mouseIsInRectangle ? Colors.LightBlue : Colors.LightBlue.WithAlpha(.5f);
        canvas.FillRectangle(0, 0, rectWidth, rectHeight);

        // rotation origin
        canvas.FillColor = Colors.Magenta;
        canvas.FillCircle(0, 0, 5);

        // text
        canvas.FontColor = Colors.Yellow;
        canvas.FontSize = fontSize;
        canvas.FontName = fontName;
        canvas.DrawString(tbText.Text, 0, stringSize.Height, Microsoft.Maui.Graphics.HorizontalAlignment.Left);
    }

    public bool IsPointInsideRectangle(Microsoft.Maui.Graphics.PointF pt, Microsoft.Maui.Graphics.PointF[] rectCorners)
    {
        // adapted from https://math.stackexchange.com/q/190403

        double x1 = rectCorners[0].X;
        double x2 = rectCorners[1].X;
        double x3 = rectCorners[2].X;
        double x4 = rectCorners[3].X;

        double y1 = rectCorners[0].Y;
        double y2 = rectCorners[1].Y;
        double y3 = rectCorners[2].Y;
        double y4 = rectCorners[3].Y;

        double a1 = Math.Sqrt((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2));
        double a2 = Math.Sqrt((x2 - x3) * (x2 - x3) + (y2 - y3) * (y2 - y3));
        double a3 = Math.Sqrt((x3 - x4) * (x3 - x4) + (y3 - y4) * (y3 - y4));
        double a4 = Math.Sqrt((x4 - x1) * (x4 - x1) + (y4 - y1) * (y4 - y1));

        double b1 = Math.Sqrt((x1 - pt.X) * (x1 - pt.X) + (y1 - pt.Y) * (y1 - pt.Y));
        double b2 = Math.Sqrt((x2 - pt.X) * (x2 - pt.X) + (y2 - pt.Y) * (y2 - pt.Y));
        double b3 = Math.Sqrt((x3 - pt.X) * (x3 - pt.X) + (y3 - pt.Y) * (y3 - pt.Y));
        double b4 = Math.Sqrt((x4 - pt.X) * (x4 - pt.X) + (y4 - pt.Y) * (y4 - pt.Y));

        double u1 = (a1 + b1 + b2) / 2;
        double u2 = (a2 + b2 + b3) / 2;
        double u3 = (a3 + b3 + b4) / 2;
        double u4 = (a4 + b4 + b1) / 2;

        double A1 = Math.Sqrt(u1 * (u1 - a1) * (u1 - b1) * (u1 - b2));
        double A2 = Math.Sqrt(u2 * (u2 - a2) * (u2 - b2) * (u2 - b3));
        double A3 = Math.Sqrt(u3 * (u3 - a3) * (u3 - b3) * (u3 - b4));
        double A4 = Math.Sqrt(u4 * (u4 - a4) * (u4 - b4) * (u4 - b1));

        double distance = A1 + A2 + A3 + A4 - a1 * a2;
        return distance < 1;
    }
}
