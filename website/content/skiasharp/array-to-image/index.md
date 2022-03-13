---
title: Array to Image with SkiaSharp
description: How to create an image from a multidimensional array of pixel values
date: 2022-03-08 7:38:00
lastmod: 2022-03-10 6:58:00
---

This page describes how to create an image from an array of RGB `byte` values using SkiaSharp.

## Test Image Data

```cs
/// <summary>
/// Create a 3D array of image data arranged [width, height, channel]
/// </summary>
public byte[,,] TestPattern(int width, int height)
{
    byte[,,] pixelArray = new byte[height, width, 3];

    int period = 150;
    int thickness = 60;

    for (int y = 0; y < pixelArray.GetLength(0); y++)
    {
        for (int x = 0; x < pixelArray.GetLength(1); x++)
        {
            if ((x + y) % period < thickness)
                pixelArray[y, x, 0] = 255; // red

            if (y % period < thickness)
                pixelArray[y, x, 1] = 255; // green

            if (x % period < thickness)
                pixelArray[y, x, 2] = 255; // blue
        }
    }

    return pixelArray;
}
```

## Array to Image

The following code will convert the 3D array to a `SKBitmap` and save the output as an image file.

Notice that garbage collector handle's `AddrOfPinnedObject()` in combination with `InstallPixels()` allows this task to be accomplished without requiring an `unsafe` code block.

```cs
byte[,,] pixelArray = TestPattern();
SKBitmap bmp = GetBitmap(pixelArray);
using FileStream fs = new("demo.png", FileMode.Create);
bmp.Encode(fs, SKEncodedImageFormat.Png, quality: 100);
```

```cs
/// <summary>
/// Create an image from a 3D array of bytes arranged [width, height, channel]
/// </summary>
public static SKBitmap GetBitmap(byte[,,] pixelArray)
{
    int width = pixelArray.GetLength(1);
    int height = pixelArray.GetLength(0);

    uint[] pixelValues = new uint[width * height];
    for (int y = 0; y < height; y++)
    {
        for (int x = 0; x < width; x++)
        {
            byte alpha = 255;
            byte red = pixelArray[y, x, 0];
            byte green = pixelArray[y, x, 1];
            byte blue = pixelArray[y, x, 2];
            uint pixelValue = (uint)red + (uint)(green << 8) + (uint)(blue << 16) + (uint)(alpha << 24);
            pixelValues[y * width + x] = pixelValue;
        }
    }

    SKBitmap bitmap = new();
    GCHandle gcHandle = GCHandle.Alloc(pixelValues, GCHandleType.Pinned);
    SKImageInfo info = new(width, height, SKColorType.Rgba8888, SKAlphaType.Premul);

    IntPtr ptr = gcHandle.AddrOfPinnedObject();
    int rowBytes = info.RowBytes;
    bitmap.InstallPixels(info, ptr, rowBytes, delegate { gcHandle.Free(); });

    return bitmap;
}
```

### Output

<img src="output.png" class="border shadow mt-2 mb-5">

## What about Unsafe Code?

While the example above does not use the `unsafe` keyword, many of the examples in the [official documentation](https://docs.microsoft.com/en-us/xamarin/xamarin-forms/user-interface/graphics/skiasharp/bitmaps/pixel-bits) accomplish similar tasks using `unsafe` code blocks. This certainly works, and it's up to you whether you want to permit `unsafe` code in your project.

```cs
SKBitmap bitmap = new SKBitmap(256, 256);
IntPtr pixelsAddr = bitmap.GetPixels();

unsafe
{
    for (int rep = 0; rep < REPS; rep++)
    {
        byte* ptr = (byte*)pixelsAddr.ToPointer();
        for (int row = 0; row < 256; row++)
            for (int col = 0; col < 256; col++)
            {
                *ptr++ = (byte)(col);   // red
                *ptr++ = 0;             // green
                *ptr++ = (byte)(row);   // blue
                *ptr++ = 0xFF;          // alpha
            }
    }
}
```

## What about `SKBitmap.Pixels[]`?

It is true that `SKBitmap` has a `SKColor Pixels[]`, but interacting with this property is _extremely_ slow. This example is included for educational purposes, but in practice it is not useful beyond getting or setting an extremely small number of pixels.

```cs
[Obsolete("WARNING: This is extremely slow")]
private static SKBitmap GetBitmapSLOW(byte[,,] pixelArray)
{
    int width = pixelArray.GetLength(1);
    int height = pixelArray.GetLength(0);
    SKBitmap bitmap = new(width, height);

    for (int y = 0; y < height; y++)
    {
        Console.WriteLine($"Row {y}");
        for (int x = 0; x < width; x++)
        {
            byte r = pixelArray[y, x, 0];
            byte g = pixelArray[y, x, 1];
            byte b = pixelArray[y, x, 2];
            bitmap.Pixels[y * width + x] = new SKColor(r, g, b);
        }
    }

    return bitmap;
}
```

## TODO: Image to Array

## Resources
* [C# Data Visualization on GitHib](https://github.com/swharden/Csharp-Data-Visualization)
* Multi-platform project source: [array-to-image](https://github.com/swharden/Csharp-Data-Visualization/tree/main/projects/array-to-image)
* Official documentation: [Accessing SkiaSharp bitmap pixel bits
](https://docs.microsoft.com/en-us/xamarin/xamarin-forms/user-interface/graphics/skiasharp/bitmaps/pixel-bits)