---
title: Render Video with System.Drawing
description: How to draw graphics for each frame and use ffmpeg to save the result as a video file
date: 2020-05-02
weight: 100
---

**This page describes how to create video files using System.Drawing and ffmpeg.** The method described here uses [System.Drawing.Common](../cross-platform) which is no longer supported on non-Windows platforms. Check out the [rendering video with SkiaSharp](../../skiasharp/video) page for a cross-platform solution.

<video controls autoplay loop width="400" height="300" class="d-block mx-auto my-5 border shadow">
    <source src="output.webm"
            type="video/webm">
    Sorry, your browser doesn't support embedded videos.
</video>

### 1. Get FFMpegCore

Create a new project:

```bash
dotnet new console
```

Add the necessary packages to your project:

```bash
dotnet add package System.Drawing.Common
dotnet add package FFMpegCore
dotnet add package FFMpegCore.Extensions.System.Drawing.Common
```

### 2. Add Using Statements

```cs
using FFMpegCore;
using FFMpegCore.Pipes;
using FFMpegCore.Extensions.System.Drawing.Common;
```

### 3. Create a Frame Generator

Create a function to `yield` frames by creating and returning `Bitmap` images one at a time. This is where the logic goes that determines what will be drawn in each frame. This example draws a green rectangle that moves and grows as a function of frame number, and also displays frame information as text at the top of the image.

```cs
IEnumerable<BitmapVideoFrameWrapper> CreateFramesSD(int count, int width, int height)
{
    for (int i = 0; i < count; i++)
    {
        // create a Bitmap
        using Bitmap bmp = new(width, height, PixelFormat.Format24bppRgb);
        using Graphics gfx = Graphics.FromImage(bmp);

        // draw a blue background
        gfx.Clear(Color.Navy);

        // draw a growing green square
        Point pt = new(i, i);
        Size sz = new(i, i);
        Rectangle rect = new(pt, sz);
        gfx.FillRectangle(Brushes.Green, rect);

        // draw some text
        using Font fnt = new("consolas", 24);
        gfx.DrawString($"Frame: {i + 1:N0}", fnt, Brushes.Yellow, 2, 2);

        // yield the wrapped Bitmap
        using BitmapVideoFrameWrapper wrappedBitmap = new(bmp);
        yield return wrappedBitmap;
    }
}
```

### 4. Encode a Video File

This step cycles through your frame generator and encodes the video one frame at a time.

```cs
var frames = CreateFramesSD(count: 200, width: 400, height: 300);
RawVideoPipeSource source = new(frames) { FrameRate = 30 };
bool success = FFMpegArguments
    .FromPipeInput(source)
    .OutputToFile("output.webm", overwrite: true, options => options.WithVideoCodec("libvpx-vp9"))
    .ProcessSynchronously();
```

### Additional Configuration
* codec `mpeg4` created `output.mp4` that worked in media player but not my browser
* codec `libx264` created `output.mp4` that worked in my browser not in media player
* codec `libvpx-vp9` created `output.webm` that worked everywhere
* See the [FFMpegCore GitHub page](https://github.com/rosenbjerg/FFMpegCore) for more options

## Resources
* Source code for this project: [Program.cs](https://github.com/swharden/Csharp-Data-Visualization/blob/main/projects/system-drawing/video/GraphicsToVideo/Program.cs)
* NuGet: https://www.nuget.org/packages/FFMpegCore
* GitHub: https://github.com/rosenbjerg/FFMpegCore