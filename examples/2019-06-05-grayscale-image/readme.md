# Setting Specific Pixel Intensity
When we want to display _data_ as an image, we almost always work in grayscale. Even if our source data is stored in double-precision floating-point memory it's best to convert it to an 8-bit (1 byte / pixel) grayscale image for display. This example shows how to assign pixel values for an 8-bit grayscale image.

![](screenshot.png)

## Core Concepts

### Bitmap _Width_ vs Bitmap _Span_

A huge trap for anyone getting started is that Bitmap widths _in memory_ are always multiples of 4 pixels. This width is called the bitmap _span_. This has to be considered when accessing values from specific memory locations. This is how to calculate the stride manually:

```cs
int stride = bmp.Width;
if ((bmp.Width % 4) > 0)
    stride += 4 - (bmp.Width % 4);
```

Now that we know the stride, we can assign pixel values to a value determined by its X and Y position run through a formula:
```cs
for (int row = 0; row < bmp.Height; row++)
{
    for (int col = 0; col < bmp.Width; col++)
    {
        int bytePos = row * stride + col;
        double value = Math.Sin((double)row / 20) + Math.Cos((double)col / 20);
        value = (value + 2) / 4;
        bmpBytes[bytePos] = (byte)(256 * value);
    }
}
```

### Output
![](screenshot.png)