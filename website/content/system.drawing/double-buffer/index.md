---
title: Double-Buffering with System.Drawing
description: How to improve GUI performance by using two images
date: 2020-04-01
---

If a `Bitmap` is displayed while it is being drawn on, it may give a flickering appearance. For example, white text moving on a black background may appear to flicker if any frames are shown where the black background is drawn but the text isn't rendered yet.

Double buffering is a strategy to ensure that only finished images are displayed. This is achieved by using two `Bitmap` objects, and displaying one while drawing on the other.

## Code

In this code example I render double-buffered graphics on a Label. I create two Bitmaps then alternate between them as I draw on the "active" one while displaying the "inactive" one.

_Note that I am not drawing on a Picturebox because those are natively double-buffered._

### 1. Create two `Bitmap` objects

I start by creating two `Bitmap` objects sized to my Label, then launch a thread to continuously call my render function

```cs
public static Bitmap bmpA;
public static Bitmap bmpB;

public Form2()
{
    InitializeComponent();

    bmpA = new Bitmap(label1.Width, label1.Height);
    bmpB = new Bitmap(label1.Width, label1.Height);

    var t = new Thread(new ThreadStart(RenderContinuously));
    t.Start();
}
```

### 2. Display the "Inactive" Bitmap

I use a timer in my Form to trigger display of the inactive bitmap. Notice here that I'm painting directly on the label, not assigning to the `Image` property like I typically do with Picturebox.

```cs
private void timer1_Tick(object sender, EventArgs e)
{
    Bitmap bmpDisplay = (drawingOnA) ? bmpB : bmpA;
    lock (bmpDisplay)
    {
        using (var gfx = label1.CreateGraphics())
            gfx.DrawImage(bmpDisplay, 0, 0);
    }
}
```

### 3. Render on the "Active" Bitmap

Rendering is pretty similar to what we've seen before. The main difference here is that every time we render we determine which Bitmap is active and lock it. Then, after the render, we flip the bool to swap between active Bitmaps.

```cs
public static bool drawingOnA = true;
public static Random rand = new Random();
public static void RenderContinuously()
{
    while (true)
    {
        // determine which Bitmap is the active one
        Bitmap bmpRender = (drawingOnA) ? bmpA : bmpB;
        lock (bmpRender)
        {
            using (var gfx = Graphics.FromImage(bmpRender))
            using (var pen = new Pen(Color.White))
            {
                // draw ten thousand random color anti-aliased lines will be drawn (slow)
                gfx.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                gfx.Clear(Color.Black);
                for (int i = 0; i < 10_000; i++)
                {
                    pen.Color = Color.FromArgb(rand.Next());
                    gfx.DrawLine(pen,
                        rand.Next(bmpRender.Width), rand.Next(bmpRender.Height),
                        rand.Next(bmpRender.Width), rand.Next(bmpRender.Height));
                }
            }

            // Swap active bitmaps
            drawingOnA = !drawingOnA;
        }
    }
}
```

## Notes

* You can improve the responsiveness of your application by [animating graphics without blocking the GUI thread](../threading/).

* System.Drawing actually has a [`BufferedGraphics`](https://docs.microsoft.com/en-us/dotnet/api/system.drawing.bufferedgraphics) which may be useful. I frequently choose not to use it because it has more complexity than my applications require.

* You probably shouldn't manually double-buffer then assign the Bitmap to a Picturebox (because it's already double-buffered). However if you do, keep in mind that assigning to a Picturebox's `Image` property is different than rendering in that you can't control when the paint happens, so locking the Bitmap in the timer function isn't useful. The best solution here is to assign a _cloned_ Bitmap (disposing the previous Bitmap) each time.