# Csharp-Data-Visualization
This repository is a collection of minimal-case example projects to display data with Visual Studio. I'm currently developing with [Visual Studio Community 2017](https://www.visualstudio.com/downloads/), writing mostly in C# and only using free software.

Description | Screenshot
---|---
**[buffered line plot](/projects/17-06-24_buffered_line_plot)** graphs data by creating a bitmap buffer, drawing on it with `System.Drawing.Graphics` (mostly `DrawLines()`) with customizable pens and quality (anti-aliasing), then displaying it onto a frame. The frame is resizable, which also resizes the bitmap buffer. Screen updates are timed and reported (at the bottom) so performance at different sizes can be assessed. | ![](projects/17-06-24_buffered_line_plot/demo.gif)
