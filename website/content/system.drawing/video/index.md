---
title: Render Video with System.Drawing
description: How to draw graphics for each frame and use ffmpeg to save the result as a video file
date: 2020-05-02
weight: 100
---

This page describes the method I use to create video files using System.Drawing and ffmpeg.

> ⚠️ **Warning: System.Drawing.Common now only supports Windows!**\
> See [Cross-Platform Support for `System.Drawing`](../cross-platform) for more information and what you can do about it.

<video controls autoplay loop width="400" height="300" class="d-block mx-auto my-5 border shadow">
    <source src="output.webm"
            type="video/webm">
    Sorry, your browser doesn't support embedded videos.
</video>

### 1. Get FFMpegCore

Add the [FFMpegCore package](https://www.nuget.org/packages/FFMpegCore/) to your project:

```
dotnet add package FFMpegCore
```

If you're not working in a .NET Framework application, you may need to install [System.Drawing.Common](https://www.nuget.org/packages/System.Drawing.Common/)

```
dotnet add package System.Drawing.Common
```

Add the necessary usings to your code:

```cs
using FFMpegCore;
using FFMpegCore.Enums;
using FFMpegCore.Extend;
using FFMpegCore.Pipes;
using System.Drawing;
using System.Drawing.Imaging;
```

### 2. Create a Frame Generator

Create a function to `yield` frames by creating and returning bitmap images one at a time. The `FFMpegCore.Extend` namespace contains a `BitmapVideoFrameWrapper` that accepts a `System.Drawing.Bitmap` and returns an object `FFMpegCore` can use as a pipe source when encoding video.

This example draws a green rectangle that moves and grows as a function of frame number, and also displays frame information as text at the top of the image.

```cs
static IEnumerable<BitmapVideoFrameWrapper> CreateFramesSD(int count, int width, int height)
{
    for (int i = 0; i < count; i++)
    {
        using Bitmap bmp = new(width, height, PixelFormat.Format24bppRgb);
        using Graphics gfx = Graphics.FromImage(bmp);
        gfx.Clear(Color.Navy);

        Point pt = new(i, i);
        Size sz = new(i, i);
        Rectangle rect = new(pt, sz);
        gfx.FillRectangle(Brushes.Green, rect);

        using Font fnt = new("consolas", 24);
        gfx.DrawString($"Frame: {i + 1:N0}", fnt, Brushes.Yellow, 2, 2);

        using BitmapVideoFrameWrapper wrappedBitmap = new(bmp);
        yield return wrappedBitmap;
    }
}
```

### 3. Encode a Video File

```cs
var frames = CreateFramesSD(count: 200, width: 400, height: 300);
var videoFramesSource = new RawVideoPipeSource(frames) { FrameRate = 30 };
var success = FFMpegArguments
    .FromPipeInput(videoFramesSource)
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