---
title: Maui.Graphics Console Quickstart
description: How to draw using Maui.Graphics in a Console Application
date: 2021-10-27
lastmod: 2022-03-12
weight: 1.1
---

This page describes how to draw using Maui.Graphics from a console application and save the output as an image file.

> **⚠️ Warning:** The Maui.Graphics package is currently in preview and its API may change as the library matures.

#### 1. Create a Console Application

```
dotnet new console
```

#### 2. Add NuGet Packages

```
dotnet add package Microsoft.Maui.Graphics
dotnet add package Microsoft.Maui.Graphics.Skia
```

#### 3. Draw Graphics

Create a bitmap export context, interact with its canvas, then save the output as an image file:

```cs
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Graphics.Skia;

SkiaBitmapExportContext bmp = new(600, 400, 1.0f);
ICanvas canvas = bmp.Canvas;

canvas.FillColor = Color.FromArgb("#003366");
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

<img src="console.png" class="border shadow mb-5">

## Resources

* [Download the source code for this project](https://github.com/swharden/Csharp-Data-Visualization/tree/main/projects/maui-graphics)
* [Use Microsoft.Maui.Graphics to Draw 2D Graphics in Any .NET Application](https://swharden.com/blog/2022-05-25-maui-graphics)
* [https://maui.graphics](https://maui.graphics)