---
title: Animating Graphics with SkiaSharp and OpenGL
description: How to animate 2D graphics in C# using SkiaSharp & OpenGL
date: 2020-04-01
weight: 40
---

**This project uses C# and the SkiaSharp drawing library to create high-performance animations with hardware-acceleration provided by OpenGL.** We will use the same Starfield graphics model we created earlier in this series.

<img src="starfield.gif" class="d-block mx-auto my-4">

While high framerates are unsurprising at small screen sizes (System.Drawing didn't do too bad a job there), the strength of SkiaSharp and OpenGL really shines when this application is run at full-screen dimensions, especially on a 4K monitor. Unlike rendering libraries that get slow as image area increases, this system is largely insensitive to screen size.

## Code

This project is really just a mash-up of code from the [Animating Graphics in Windows Forms](../../system.drawing/animate-winforms/) and [GPU-Accelerated Drawing with SkiaSharp & OpenGL](../skiasharp-opengl/) pages which describe the techniques for creating animations and drawing image with SkiaSharp in more detail.

### Render on the `PaintSurface` Event

This program simply renders the starfield whenever the control is painted. We don't use the [Rendering Graphics without Blocking GUI Thread](../../system.drawing/threading/) method in this example, but we could have if we wanted to.

```cs
private void skglControl1_PaintSurface(object sender, SkiaSharp.Views.Desktop.SKPaintGLSurfaceEventArgs e)
{
    e.Surface.Canvas.Clear(SKColors.Black);
    var starColor = new SKColor(255, 255, 255, starAlpha);
    var starPaint = new SKPaint() { IsAntialias = true, Color = starColor };
    foreach (var star in field.GetStars())
    {
        float xPixel = (float)star.x * skglControl1.Width;
        float yPixel = (float)star.y * skglControl1.Height;
        float radius = (float)star.size - 1;
        var point = new SKPoint(xPixel, yPixel);
        e.Surface.Canvas.DrawCircle(point, radius, starPaint);
    }
}
```

### Trigger Renders with a Timer

The simplest way to achieve continuous animation is to trigger re-painting by invalidating the control from a Timer that ticks every 1ms. Even though our paint renders extremely fast (capable of achieving thousands of renders a second), using the timer this way limits our frame-rate to around 60 FPS.

```cs
private void timer1_Tick(object sender, EventArgs e)
{
    field.Advance();
    skglControl1.Invalidate();
}
```

### Enable VSync

In the SKGLControl properties ensure to set VSync to True. This will reduce frame tearing and skipping during animations. The result is very subtle, but enabling this will result in smoother animations.

## Conclusions

* SkiaSharp + OpenGL is extremely fast rendering reasonable numbers of objects
* Render speed is maintained even at full-screen sizes
* Large numbers of objects (100k) are still slow

## Source Code

GitHub: [WinFormsSkiaSharp](https://github.com/swharden/Csharp-Data-Visualization/tree/master/dev/old/drawing/starfield/Starfield.WinFormsSkiaSharp)