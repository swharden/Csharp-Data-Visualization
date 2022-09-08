using SkiaSharp;

namespace skiasharp_wrap;

public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent();
        skglControl1.PaintSurface += (s, e) => PaintSurface(e.Surface);
        trackBar1.ValueChanged += (s, e) => skglControl1.Invalidate();
    }

    private void PaintSurface(SKSurface surface)
    {
        SKCanvas canvas = surface.Canvas;
        SKRect figureRect = canvas.DeviceClipBounds;
        float padding = 30;
        float testRectWidth = figureRect.Width - padding * 2;
        testRectWidth *= (float)trackBar1.Value / trackBar1.Maximum;
        SKRect testRect = new(padding, padding, padding + testRectWidth, figureRect.Height - padding);

        using SKPaint paint = new() { Color = SKColors.Navy };
        canvas.DrawRect(figureRect, paint);

        paint.Color = SKColors.Yellow.WithAlpha(200);
        paint.IsStroke = true;
        canvas.DrawRect(testRect, paint);

        paint.IsStroke = false;
        paint.Color = SKColors.White;
        paint.TextSize = 16;
        paint.IsAntialias = true;
        DrawText(canvas, SampleData.SkiaInfo, testRect, paint);
    }

    private static void DrawText(SKCanvas canvas, string text, SKRect rect, SKPaint paint)
    {
        float spaceWidth = paint.MeasureText(" ");
        float wordX = rect.Left;
        float wordY = rect.Top + paint.TextSize;
        foreach (string word in text.Split(' '))
        {
            float wordWidth = paint.MeasureText(word);
            if (wordWidth <= rect.Right - wordX)
            {
                canvas.DrawText(word, wordX, wordY, paint);
                wordX += wordWidth + spaceWidth;
            }
            else
            {
                wordY += paint.FontSpacing;
                wordX = rect.Left;
            }
        }
    }
}
