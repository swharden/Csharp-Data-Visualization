---
title: Render Video with SkiaSharp
description: How to draw graphics for each frame and use ffmpeg to save the result as a video file
date: 2020-05-02
weight: 100
---

This page describes the method I use to create video files using SkiaSharp and ffmpeg.

<video controls autoplay loop width="400" height="300" class="d-block mx-auto my-5 border shadow">
    <source src="output.webm"
            type="video/webm">
    Sorry, your browser doesn't support embedded videos.
</video>

### 1. Get SkiaSharp and FFMpegCore

Add [SkiaSharp](https://www.nuget.org/packages/SkiaSharp/) and [FFMpegCore](https://www.nuget.org/packages/FFMpegCore/) packages to your project:

```
dotnet add package SkiaSharp
dotnet add package FFMpegCore
```

Add the necessary usings to your code:

```cs
using FFMpegCore;
using FFMpegCore.Pipes;
using SkiaSharp;
```

### 2. Create a Frame Converter

This class is used to convert `SKBitmap` images to `IVideoFrame` objects that can be used by ffmpeg.

```cs
public class SKBitmapFrame : IVideoFrame, IDisposable
{
    public int Width => Source.Width;
    public int Height => Source.Height;
    public string Format => "bgra";
    private readonly SKBitmap Source;

    public SKBitmapFrame(SKBitmap bmp)
    {
        if (bmp.ColorType != SKColorType.Bgra8888)
            throw new NotImplementedException("only 'bgra' color type is supported");
        Source = bmp;
    }

    public void Dispose() => Source.Dispose();

    public void Serialize(Stream stream) => stream.Write(Source.Bytes);

    public async Task SerializeAsync(Stream stream, CancellationToken token) => 
        await stream.WriteAsync(Source.Bytes, token).ConfigureAwait(false);
}
```

### 3. Create Frame Rendering Logic

This `IEnumerable` function is used to `yield` individual `IVideoFrame` objects as ffmpeg needs them. 

When a frame is requested it is rendered onto a `SKBitmap` and returned packaged inside an `IVideoFrame`.

This way frames are rendered just before they are needed, preventing memory accumulation during large rendering jobs.

```cs
static IEnumerable<IVideoFrame> CreateFrames(int count, int width, int height)
{
    using SKFont textFont = new(SKTypeface.FromFamilyName("consolas"), size: 32);
    using SKPaint textPaint = new(textFont) { Color = SKColors.Yellow, TextAlign = SKTextAlign.Center };
    using SKPaint rectanglePaint = new() { Color = SKColors.Green, Style = SKPaintStyle.Fill };
    SKColor backgroundColor = SKColors.Navy;

    for (int i = 0; i < count; i++)
    {
        Console.WriteLine($"Rendering frame {i + 1} of {count}");
        using SKBitmap bmp = new(width, height);
        using SKCanvas canvas = new(bmp);
        canvas.Clear(backgroundColor);
        canvas.DrawRect(i, i, i * 2, i * 2, rectanglePaint);
        canvas.DrawText("SkiaSharp", bmp.Width / 2, bmp.Height * .4f, textPaint);
        canvas.DrawText($"Frame {i}", bmp.Width / 2, bmp.Height * .6f, textPaint);

        using SKBitmapFrame frame = new(bmp);
        yield return frame;
    }
}
```


### 4. Render a Video File

Tying everything together, this code defines an enumerated 150-frame video and loads it into a pipe, then uses FFMpeg to encode the frames into a WEBM file (shown at the top of the page).

```cs
var frames = CreateFrames(count: 150, width: 400, height: 300);
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
* Source code for this project: [SkiaSharp/video](https://github.com/swharden/Csharp-Data-Visualization/tree/main/projects/skiasharp/video/)
* NuGet: https://www.nuget.org/packages/FFMpegCore
* GitHub: https://github.com/rosenbjerg/FFMpegCore