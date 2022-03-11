---
title: Array to Image with System.Drawing
description: How to create an image from a multidimensional array of pixel values
date: 2022-03-08 7:38:00
updated: 2022-03-10 6:58:00
---

This page describes how to create an image from an array of RGB `byte` values using System.Drawing.

> ⚠️ **Warning: System.Drawing.Common now only supports Windows!**\
> See [Cross-Platform Support for System.Drawing.Common](../cross-platform) for more information and what you can do about it.

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

The following code will convert the 3D array to a `Bitmap` and save the output as an image file.


```cs
byte[,,] pixelArray = TestPattern();
using Bitmap bmp = GetBitmap(pixelArray);
bmp.Save("demo.png");
```

```cs
/// <summary>
/// Create an image from a 3D array of bytes arranged [width, height, channel]
/// </summary>
public static Bitmap GetBitmap(byte[,,] pixelArray)
{
    int width = pixelArray.GetLength(1);
    int height = pixelArray.GetLength(0);
    int stride = (width % 4 == 0) ? width : width + 4 - width % 4;
    int bytesPerPixel = 3;

    byte[] bytes = new byte[stride * height * bytesPerPixel];
    for (int y = 0; y < height; y++)
    {
        for (int x = 0; x < width; x++)
        {
            int offset = (y * stride + x) * bytesPerPixel;
            bytes[offset + 0] = pixelArray[y, x, 2]; // blue
            bytes[offset + 1] = pixelArray[y, x, 1]; // green
            bytes[offset + 2] = pixelArray[y, x, 0]; // red
        }
    }

    PixelFormat formatOutput = PixelFormat.Format24bppRgb;
    Rectangle rect = new(0, 0, width, height);
    Bitmap bmp = new(stride, height, formatOutput);
    BitmapData bmpData = bmp.LockBits(rect, ImageLockMode.ReadOnly, formatOutput);
    Marshal.Copy(bytes, 0, bmpData.Scan0, bytes.Length);
    bmp.UnlockBits(bmpData);

    Bitmap bmp2 = new(width, height, PixelFormat.Format32bppPArgb);
    Graphics gfx2 = Graphics.FromImage(bmp2);
    gfx2.DrawImage(bmp, 0, 0);

    return bmp2;
}
```

### Output

<img src="output.png" class="border shadow mt-2 mb-5">

## TODO: Image to Array

## Resources
* [C# Data Visualization on GitHib](https://github.com/swharden/Csharp-Data-Visualization)
* Multi-platform project source: [array-to-image](https://github.com/swharden/Csharp-Data-Visualization/tree/main/projects/array-to-image)
* Official documentation: [Accessing SkiaSharp bitmap pixel bits
](https://docs.microsoft.com/en-us/xamarin/xamarin-forms/user-interface/graphics/skiasharp/bitmaps/pixel-bits)