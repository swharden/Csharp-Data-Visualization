# Drawing Graphics in C#
This project demonstrates a simple way to draw lines in a Windows Form. Here we create a `Bitmap` then use a `Graphics` object to draw lines on it. The `Bitmap` is then assigned to `PictureBox.Image` and displayed to the user.

<a href="screenshot.png"><img src="screenshot.png" width="400"></a>

## Core Concepts

### Initializing Bitmap and Graphics Objects
* Initializing a bitmap takes a little time, so for graphs which update frequently it is to your advantage to declare them as class-level variables and initialize them once when the program loads.
* In this example we initialize the bitmap to match the size of `PictureBox` which it will later be drawn on. 
* You'll have to re-initialize the bitmap and graphics objects if the size changes, but this can be done by adding a `SizeChanged` event handler to PictureBox1.
* By default the Bitmap has a transparent background. Use `gfx.Clear()` to fill the entire image with a solid color.
* If you want lines to be anti-aliased (a prettier output but a slight decrease in speed) set the `gfx.SmoothingMode`.

```cs
private Bitmap bmp;
private Graphics gfx;

private void InitializeBitmapAndGraphics()
{
    bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
    gfx = Graphics.FromImage(bmp);
    gfx.Clear(Color.White);
    gfx.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
    Console.WriteLine($"Created new bitmap {bmp.Size}");
    pictureBox1.Image = bmp;
}

```

### Drawing Random Lines
* To draw lines (or other shapes like rectangle, ellipse, or text) interact with the graphics object. After drawing you'll have to update the PictureBox Image with the updated bitmap.
* The null check ensures the bitmap and graphics objects have been initialized before attempting to draw on them.

```cs
private Random rand = new Random();
private void AddRandomLine()
{
    if (gfx != null)
    {
        Point pt1 = new Point(rand.Next(bmp.Width), rand.Next(bmp.Height));
        Point pt2 = new Point(rand.Next(bmp.Width), rand.Next(bmp.Height));
        float lineWidth = (float)(rand.NextDouble() * 3);
        Pen pen = new Pen(Color.Blue, lineWidth);
        Console.WriteLine($"Drawing {lineWidth} px line to connect {pt1} and {pt2}");
        gfx.DrawLine(pen, pt1, pt2);
        pictureBox1.Image = bmp;
    }
}
```