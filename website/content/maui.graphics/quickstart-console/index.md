---
Title: Maui.Graphics Console Quickstart
Description: How to draw using Maui.Graphics in a Console Application
---

This page describes how to draw using Maui.Graphics from a console application and save the output as an image file.

> **⚠️ Warning:** The Maui.Graphics package is currently in preview and its API may change as the library matures.

#### 1. Create a Console Application

```
dotnet new console
```

#### 2. Add NuGet Packages

```
dotnet add package Microsoft.Maui.Graphics --prerelease
dotnet add package Microsoft.Maui.Graphics.Skia --prerelease
```

#### 3. Draw Graphics

Create a bitmap export context, interact with its canvas, then save the output as an image file:

```cs
using Microsoft.Maui.Graphics; // 6.0.200-preview.13.935
using Microsoft.Maui.Graphics.Skia; // 6.0.200-preview.13.935

SkiaBitmapExportContext bmp = new(600, 400, 1.0f);
ICanvas canvas = bmp.Canvas;

canvas.FillColor = Colors.Navy;
canvas.FillRectangle(0, 0, bmp.Width, bmp.Height);

Random rand = new(0);
canvas.StrokeColor = Colors.White.WithAlpha(.5f);
canvas.StrokeSize = 2;
for (int i = 0; i < 100; i++)
{
    float x = rand.Next(bmp.Width);
    float y = rand.Next(bmp.Height);
    float r = rand.Next(5, 50);
    canvas.DrawCircle(x, y, r);
}

bmp.WriteToFile("console.png");
```

### Output

<div class="text-center">

![](console.png)

</div>

## Resources

* [Download the source code for this project](https://github.com/swharden/Maui.Graphics/tree/main/projects)