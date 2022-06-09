---
title: "SkiaSharp Console Quickstart"
description: "Draw Graphics with SkiaSharp in a C# Console App"
date: 2022-03-10 8:08:00
weight: 10
---

This article describes how to draw graphics from a C# console application and save the output as an image file using SkiaSharp.

### 1. Create a Console App
```
dotnet new console
```

### 2. Add NuGet Packages
```
dotnet add package SkiaSharp
```

### 3. Draw some Graphics

_This is the full code for a .NET 6 console app_

```cs
using SkiaSharp;

// Create an image and fill it blue
SKBitmap bmp = new(640, 480);
using SKCanvas canvas = new(bmp);
canvas.Clear(SKColor.Parse("#003366"));

// Draw lines with random positions and thicknesses
Random rand = new(0);
SKPaint paint = new() { Color = SKColors.White.WithAlpha(100), IsAntialias = true };
for (int i = 0; i < 100; i++)
{
    SKPoint pt1 = new(rand.Next(bmp.Width), rand.Next(bmp.Height));
    SKPoint pt2 = new(rand.Next(bmp.Width), rand.Next(bmp.Height));
    paint.StrokeWidth = rand.Next(1, 10);
    canvas.DrawLine(pt1, pt2, paint);
}

// Save the image to disk
SKFileWStream fs = new("quickstart.jpg");
bmp.Encode(fs, SKEncodedImageFormat.Jpeg, quality: 85);
```

### Output

<img src="quickstart.jpg" class="border shadow mt-3 mb-5">

## Resources
* Source code: [projects/skiasharp-quickstart](https://github.com/swharden/Csharp-Data-Visualization/tree/main/projects/skiasharp-quickstart)
* NuGet: [SkiaSharp](https://www.nuget.org/packages/SkiaSharp)
* Official: [Creating and drawing on SkiaSharp bitmaps](https://docs.microsoft.com/en-us/xamarin/xamarin-forms/user-interface/graphics/skiasharp/bitmaps/drawing) and [Saving SkiaSharp bitmaps to files
](https://docs.microsoft.com/en-us/xamarin/xamarin-forms/user-interface/graphics/skiasharp/bitmaps/saving)