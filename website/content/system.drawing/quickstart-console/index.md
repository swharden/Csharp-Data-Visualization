---
Title: "System.Drawing Console Quickstart"
Description: "How to use System.Drawing to create graphics in a C# console application"
date: 2020-05-16
lastmod: 2022-03-13 
weight: 10
---

This example .NET Console application uses `System.Drawing` to draw 10,000 random lines on a dark blue background and save the output as a PNG file. 

> ⚠️ **Warning: System.Drawing.Common now only supports Windows!**\
> See [Cross-Platform Support for `System.Drawing`](../cross-platform) for more information and what you can do about it.

### Code

_This example uses .NET 6 and version `4.*` of the [`System.Drawing.Common` package](https://www.nuget.org/packages/System.Drawing.Common/)._

```cs
using System.Drawing;

using Bitmap bmp = new(600, 400);
using Graphics gfx = Graphics.FromImage(bmp);
gfx.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

gfx.Clear(Color.Navy);

Random rand = new(0);
using Pen pen = new(Color.White);
for (int i = 0; i < 10_000; i++)
{
    pen.Color = Color.FromArgb(rand.Next());
    Point pt1 = new(rand.Next(bmp.Width), rand.Next(bmp.Height));
    Point pt2 = new(rand.Next(bmp.Width), rand.Next(bmp.Height));
    gfx.DrawLine(pen, pt1, pt2);
}

bmp.Save("demo.png");
```

### Output

<img src="drawing-quickstart-console.png" class="border shadow mb-5">

## Respect `IDisposable`

Many System.Drawing objects inherit from `IDisposable` and it is ***critical*** that they are disposed of properly to avoid memory issues. This means calling `Dispose()` after you're done using an object, or better yet throwing its instantiation inside a `using` statement.

> 💡 **Deep Dive:** Read Microsoft's documentation about [using objects that implement IDisposable](https://docs.microsoft.com/en-us/dotnet/standard/garbage-collection/using-objects)

Take care to properly dispose of these common System.Drawing objects:
* [`System.Drawing.Image`](https://docs.microsoft.com/en-us/dotnet/api/system.drawing.image)
* [`System.Drawing.Bitmap`](https://docs.microsoft.com/en-us/dotnet/api/system.drawing.bitmap)
* [`System.Drawing.Graphics`](https://docs.microsoft.com/en-us/dotnet/api/system.drawing.graphics)
* [`System.Drawing.Pen`](https://docs.microsoft.com/en-us/dotnet/api/system.drawing.pen)
* [`System.Drawing.Brush`](https://docs.microsoft.com/en-us/dotnet/api/system.drawing.brush)
* [`System.Drawing.Font`](https://docs.microsoft.com/en-us/dotnet/api/system.drawing.font)
* [`System.Drawing.FontFamily`](https://docs.microsoft.com/en-us/dotnet/api/system.drawing.fontfamily)
* [`System.Drawing.StringFormat`](https://docs.microsoft.com/en-us/dotnet/api/system.drawing.stringformat)


## Anti-Aliased Graphics and Text
**Anti-aliasing is OFF by default.** Enabling anti-aliasing significantly slows render time but produces superior images when drawing angled lines and edges. Anti-aliasing can be enabled separately for shapes and text.

<img src="anti-aliasing-example.png" class="d-block mx-auto my-4">

```cs
// Configure anti-aliasing mode for graphics
gfx.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
gfx.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;

// Configure anti-aliasing mode for text
gfx.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SingleBitPerPixelGridFit;
gfx.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
gfx.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
```

ClearType typically looks the best, but when drawn on a transparent background it looks poor ([because](https://devblogs.microsoft.com/oldnewthing/20150129-00/?p=44803) [reasons](https://devblogs.microsoft.com/oldnewthing/20060614-00/?p=30873)).

## Resources

* Source code: [projects/system-drawing/quickstart-console/](https://github.com/swharden/Csharp-Data-Visualization/tree/main/projects/system-drawing/quickstart-console)

* [Cross-Platform Support for `System.Drawing`](../cross-platform)

* [Official documentation for `System.Drawing`](https://docs.microsoft.com/en-us/dotnet/api/system.drawing)

* [Pitfalls of transparent rendering of anti-aliased fonts](https://devblogs.microsoft.com/oldnewthing/20060614-00/?p=30873)

* [Color-aware ClearType requires access to fixed background pixels, which is a problem if you don't know what the background pixels are, or if they aren't fixed](https://devblogs.microsoft.com/oldnewthing/20150129-00/?p=44803)