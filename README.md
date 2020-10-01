# C# Data Visualization

This repository is a collection of minimal-case example projects to display data with Visual Studio. Code here is mostly written in C# using [Visual Studio Community](https://www.visualstudio.com/downloads/) and only uses free software and plugins.


Most examples use `System.Drawing` (available as a .NET Standard library using the [System.Drawing.Common](https://www.nuget.org/packages/System.Drawing.Common/) NuGet package), but some use alternative methods like WPF and Skia (SkiaSharp).

## Child Projects

Some code examples started here have matured into their own projects:

Project | Screenshot
---|---
**[ScottPlot](http://swharden.com/scottplot/)** is an interactive plotting library for .NET. If you're just looking for an easy way to interactively display some data on a graph using C#, ScottPlot might be for you! | ![](https://raw.githubusercontent.com/swharden/ScottPlot/master/dev/nuget/ScottPlot.gif)
**[Spectrogram](https://github.com/swharden/Spectrogram)** is a simple spectrogram library for .NET. Specrogram converts signals (typically audio) into the frequency-domain and makes it easy to display spectrograms as 2D images. Spectrogram is fast enough to display the audio spectrum in real time. | ![](https://raw.githubusercontent.com/swharden/Spectrogram/master/dev/spectrogram.png)
**[Sound Card ECG](https://github.com/swharden/SoundCardECG)** uses scottplot to interactively display the soundcard signal in real time| ![](https://raw.githubusercontent.com/swharden/SoundCardECG/master/src/SoundCardECG/screenshot.png)
**[HHSharp](https://github.com/swharden/HHSharp)** is an interactive Hodgkin-Huxley neuron simulator|![](https://raw.githubusercontent.com/swharden/HHSharp/master/dev/screenshot-sEPSCs.gif)


## Instructional Code Examples
Each of these example projects introduces an important concept in data visualization and has well-documented code examples to demonstrate them. These examples can be useful individually, but are best appreciated if fully reviewed in top-down order.

> **⚠️ WARNING:** This project is being actively restructured (April, 2020) to better organize project code in this repository and move commentary to the [C# Data Visualization Website](http://swharden.com/CsharpDataVis/). Projects below this line have not yet been upgraded to this new format.


### Drawing / Graphing

Description | Screenshots
---|---
**[Drawing Lines](examples/2019-06-01-graphics-basics/readme.md)** - This project demonstrates a simple way to draw lines in a Windows Form. Here we create a Bitmap then use a Graphics object to draw lines on it. The Bitmap is then assigned to PictureBox.Image and displayed to the user. | ![](/examples/2019-06-01-graphics-basics/screenshot.png)
**[Drawing with the Mouse](examples/2019-06-02-drawing-with-mouse/readme.md)** - This project uses a PictureBox's MouseMove event handler to create a MSPaint-like drawing surface with only a few lines of code. | ![](examples/2019-06-02-drawing-with-mouse/screenshot.png)
**[Plotting on a 2D Coordinate System](/examples/2019-06-03-coordinate-system/readme.md)** - A simple but challenging task when plotting data on a bitmap is the conversion between 2D data space and bitmap pixel coordinates. If your axis limits are -10 and +10 (horizontally and vertically), what pixel position on the bitmap corresponds to (-1.23, 3.21)? This example demonstrates a minimal-case unit-to-pixel method and uses it to plot X/Y data on a bitmap. | ![](/examples/2019-06-03-coordinate-system/screenshot.png)

### Bitmap Pixel Manipulation

Description | Screenshot
---|---
**[Modifying Bitmap Data in Memory](/examples/2019-06-04-pixel-setting/readme.md)** - Bitmaps in memory have a certain number of bytes per pixel, so they're easy to convert to/from byte arrays. This example shows how to convert a Bitmap to a byte array, fill the array with random values, and convert it back to a Bitmap to display in a PictureBox. This method can be faster than using drawing methods like GetPixel and PutPixel. | ![](/examples/2019-06-04-pixel-setting/screenshot.png)
**[Setting Pixel Intensity from a Value](/examples/2019-06-05-grayscale-image/readme.md)** - This example shows how to create an 8-bit grayscale image where pixel intensities are calculated from a formula (but could easily be assigned from a data array). This example also demonstrates the important difference between Bitmap _width_ and _span_ when working with byte positions in memory. | ![](/examples/2019-06-05-grayscale-image/screenshot.png)

### Drawing in WPF Applications

Description | Screenshot
---|---
**[Drawing Lines with WPF](examples/2019-10-09-WPF-Draw-Lines)** is a minimal-case example to demonstrate how to add primitive shapes to a canvas in a WPF application | ![](examples/2019-10-09-WPF-Draw-Lines/screenshot.png)

### Hardware-Accelerated Drawing with SkiaSharp and OpenGL

**GDI+:** Calls to `System.Drawing` use the GDI+ backend to draw on the screen. It is convenient because it is been around forever and is easily supported by .NET, but it does not perform well in parallel environments or with large bitmaps (e.g., full screen). Most examples on this page use GDI+ to create images.

**Skia:** The [Skia Graphics Library](https://skia.org/) is an open-source 2D drawing library developed by Google. [SkiaSharp](https://github.com/mono/SkiaSharp) is a cross-platform .NET API for drawing with Skia.

**Hardware Acceleration:** GDI+ is processor-based. Skia supports hardware acceleration with the GPU with the OpenGL back-end. You don't have to know anything about OpenGL to use this feature, you just have to properly configure Skia to use OpenGL then interact with Skia normally.

Description | Screenshot
---|---
**[Drawing with SkiaSharp and OpenGL](/examples/2019-09-08-SkiaSharp-openGL)** - This program demonstrates how to vastly outperform GDI+ (System.Drawing) when drawing thousands of semi-transparent lines at full-screen window sizes. | ![](/examples/2019-09-08-SkiaSharp-openGL/screenshot.jpg)

See also: **[QuickPlot](https://github.com/swharden/QuickPlot)** - an interactive .NET plotting library using SkiaSharp

### Audio

Description | Screenshot
---|---
**[Plotting Audio Amplitude](/examples/2019-06-06-audio-level-monitor/readme.md)** - This example uses [NAudio](https://github.com/naudio/NAudio) to access the sound card, calculates the amplitude of short recordings, then graphs them continuously in real time with [ScottPlot](https://github.com/swharden/ScottPlot). This project is a good place to get started to see how to interface audio input devices. | ![](/examples/2019-06-06-audio-level-monitor/screenshot.png)
**[Plotting Audio Values](/examples/2019-06-07-audio-visualizer/readme.md)** - This example uses [NAudio](https://github.com/naudio/NAudio) to access the sound card and plots raw PCM values with [ScottPlot](https://github.com/swharden/ScottPlot). These graphs contain tens of thousands of data points, but remain fully interactive even as they are being updated in real time. | ![](/examples/2019-06-07-audio-visualizer/screenshot.gif)
**[Plotting Audio FFT](/examples/2019-06-08-audio-fft)** - This example continuously plots the frequency component of an audio input device. The [NAudio](https://github.com/naudio/NAudio) library is used to acquire the audio data and process the FFT and [ScottPlot](https://github.com/swharden/ScottPlot) is used for the plotting. | ![](/examples/2019-06-08-audio-fft/screenshot.gif)

## Older Projects
* These projects are first-pass implementations of ideas, but they work, so learn and from them what you can and take whatever you find useful! They're not as polished as the ones in the previous section.
* Each example below is a standalone Visual Studio solution
* Projects typically increase in complexity from bottom to top
* Only completed projects are listed in the table below.
* The [projects folder](projects) contains even more in-progress and unfinished projects. 

Project Description | Screenshots
---|---
**[Graphing Data with GnuPlot from C++](https://github.com/swharden/code-notes/tree/master/Cpp/projects/2018-09-27%20hello%20gnuplot%20world)** isn't Csharp-specific, but can be translated to any programming language. It demonstrates how easy it is to graph data from any programming language by saving it as a text file then launching gnuplot on it. Advanced data control and styling can be set with command line arguments (compiled-in), or defined in script files which give the end user the ability to modify styling without modifying the source code. | ![](https://github.com/swharden/code-notes/blob/master/Cpp/projects/2018-09-27%20hello%20gnuplot%20world/doc/interactive.png)
**[Realtime Microphone FFT Analysis](projects/18-09-19_microphone_FFT_revisited)** is a new version of an older concept. This project uses a modern [ScottPlot](https://github.com/swharden/ScottPlot/) which has many improvements over older projects listed here. | ![](projects/18-09-19_microphone_FFT_revisited/screenshot.png)
**[DataView 1.0](/projects/18-01-15_form_drawing/)** is an interactive plotting control written using only the standard library. It allows panning/zooming by left-click-dragging the axis labels, moving the scrollbars, clicking the buttons, and also through right-click menus on the axis labels. Interactive draggable markers are also included. This control was designed to look similar to the commercial software ClampFit. I have decided to re-code this project from the ground-up, but the solution is frozen as-is (in a quite useful state) and the project page contains many notes of considerations and insights I had while developing it. | ![](/projects/18-01-15_form_drawing/screenshot2.png)
**[QRSS Spectrograph](/projects/18-01-14_qrss/)** produces spectrographs which are very large (thousands of pixels) and very high frequency resolution (fractions of a Hz) intended to be used to decode slow-speed (1 letter per minute) frequency-shifting Morse code radio signals, a transmission mode known as QRSS. While functional as it is, this project is intended to be a jumping-off point for anybody interested in making a feature-rich QRSS viewer.|![](/projects/18-01-14_qrss/screenshot_qrss.png)
**[realtime audio spectrograph](/projects/18-01-11_microphone_spectrograph/)** listens to your default recording device (microphone or StereoMix) and creates a 2d time vs. frequency plot where pixel values are relative to frequency power (in a linear or log scale). This project is demonstrated in a YouTube video. This example is not optimized for speed, but it is optimized for simplicity and should be very easy to learn from.|![](/projects/18-01-11_microphone_spectrograph/spectrograph.gif)
**[realtime audio level meter](/projects/18-01-09_microphone_level_meter/)** uses NAudio to provide highspeed access to the microphone or recording device. This project is a minimal-case project intended to remind the author how to effeciently interact with incoming audio data.|![](/projects/18-01-09_microphone_level_meter/screenshot.gif)
**[realtime graph of microphone audio (RAW and FFT)](/projects/17-07-16_microphone/)** Here I demonstrate a minimal-case example using the interactive graphing framework (below) to display audio values sampled from the microphone in real time. FFT () is also calculated and displayed interactively. [See this project demonstrated on YouTube](https://youtu.be/qUlCImYOC8c). Audio capture is achieved with nAudio and FFT with Accord. See [FFT notes](/notes/FFT.md) for additional details. | ![](/projects/17-07-16_microphone/demo.gif)
**[linear data speed rendering](/projects/17-07-03_wav_speed_rendering/)** I greatly increased speed by drawing only single vertical lines (of small range min and max values) when the point density exceeds the horizontal pixel density. This is only suitable for evenly-spaced linear data (which is exactly what my target applications will be plotting). Performance is great, and there is plenty of room for improvement on the coding side too. `AddLineXY()` will be used to manually draw a line between every X,Y point in a list. `AddLineSignal()` graphs data from huge amounts of linear data by only graphing vertical lines.| ![](/projects/17-07-03_wav_speed_rendering/demo.gif)
**[intelligent axis labels](/projects/17-07-02_nice_axis)** This from-scratch re-code has separate classes for core plotting routines, data generation, and axis manipulation. Tick marks are quite intelligent as well. Included is a GUI demo (shown) as well as a 6 line console application which does the same thing (saving the output to a .jpg file instead of displaying it interactively).| ![](/projects/17-07-02_nice_axis/demo.gif)
**[interactive electrophysiology data](/projects/17-06-26_abf_data)** Nearly identical to the previous example, except that there is a CSV button which loads an arbitrary string of values from `data.csv` if it is saved in the same folder as the exe. With minimal effort this program could be modified to directly load from ATF (Axon Text Format) files. With a little more effort, you could interface ABF files with the [Axon pCLAMP ABF SDK](http://mdc.custhelp.com/app/answers/detail/a_id/18881/~/axon%E2%84%A2-pclamp%C2%AE-abf-file-support-pack-download-page). | ![](projects/17-06-26_abf_data/demo.jpg)
**[interactive pan and zoom](/projects/17-06-25_pan_and_zoom)** The ScottPlot class now has an advanced axis system. This makes it easily to set the viewing window in unit coordinates (X1, X2, Y1, Y2) and also do things like zoom and pan. This example was made to demonstrate these functions, as well as compare the speed of interactive graph manipulation at different sizes and with different quality settings. Although the GUI has many features, [Form1.cs](projects/17-06-25_pan_and_zoom/swharden_demo/Form1.cs) is not overwhelmingly complex. | ![](projects/17-06-25_pan_and_zoom/demo.gif)
**[stretchy line plot](/projects/17-06-24_stretchy_line_plot/)** In this demo some random points are generated and scrolled (similar to numpy's [roll](https://docs.scipy.org/doc/numpy-1.10.0/reference/generated/numpy.roll.html) method). Although the result looks simple, there is some strong thought behind how this example is coded. All the graphing code is encapsulated by the ScottPlot class of [swhPlot.cs](projects/17-06-24_stretchy_line_plot/pixelDrawDrag2/swhPlot.cs). The code of the GUI itself [Form1.cs](projects/17-06-24_stretchy_line_plot/pixelDrawDrag2/Form1.cs) is virtually empty. My thinking is that from here I'll work on the graphing class, keeping gui usage as simple as possible. _Note: plotting 321 data points I'm getting about 300Hz drawing rate with anti-aliasing off and 100Hz with it on_ | ![](/projects/17-06-24_stretchy_line_plot/demo.gif)
**[basic buffered line plot](/projects/17-06-24_buffered_line_plot)** graphs data by creating a bitmap buffer, drawing on it with `System.Drawing.Graphics` (mostly `DrawLines()`) with customizable pens and quality (anti-aliasing), then displaying it onto a frame. The frame is resizable, which also resizes the bitmap buffer. Screen updates are timed and reported (at the bottom) so performance at different sizes can be assessed. | ![](projects/17-06-24_buffered_line_plot/demo.gif)
**[highspeed bitmap pixel access](/projects/18-01-10_fast_pixel_bitmap/)** requires some consideration. This minimal-case project demonstrates how to set individual pixels of a bitmap buffer using the slower (simpler) setpixel method and the faster (but more complex) lockbits method. Once a bitmap buffer is modified, it is then applied to a pictutremap. | ![](/projects/18-01-10_fast_pixel_bitmap/screenshot.png)

