---
title: System.Drawing WPF Quickstart
Description: Using System.Drawing to Draw Graphics in WPF Applications
date: 2020-04-20
weight: 30
---
**This example demonstrates how to draw graphics in WPF.** We lay-out a `Canvas` with an `Image` inside it, then create a render method which creates a `Bitmap` the size of the canvas, draws on it using a `Graphics` object, then copies the output from a `Bitmap` (which System.Drawing produces) to a `BitmapImage` (which WPF can display).

<img src="drawing-in-wpf.png" class="d-block mx-auto">

## Code

### 1. Add a `Canvas` to your `Window`

Add a `Canvas` to your layout and inside it include an `Image`. 

Add add a `SizeChanged` method to your `Window` and `Loaded` and `MouseDown` methods in your `Canvas`. We will use these methods later to trigger renders.

```xml
<Grid>
    <Canvas Name="myCanvas" MouseDown="myCanvas_MouseDown" Loaded="myCanvas_Loaded">
        <Image Name="myImage"/>
    </Canvas>
</Grid>
```

```xml
<Window ... SizeChanged="Window_SizeChanged">
```

### 2. Create a Method to Convert `Bitmap` to `BitmapImage`

System.Drawing produces a `Bitmap` but WPF can only display `BitmapImage` objects. To facilitate converting between the two, add the following method to your source code.

```cs
private BitmapImage BmpImageFromBmp(Bitmap bmp)
{
    using (var memory = new System.IO.MemoryStream())
    {
        bmp.Save(memory, System.Drawing.Imaging.ImageFormat.Png);
        memory.Position = 0;

        var bitmapImage = new BitmapImage();
        bitmapImage.BeginInit();
        bitmapImage.StreamSource = memory;
        bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
        bitmapImage.EndInit();
        bitmapImage.Freeze();

        return bitmapImage;
    }
}
```

> **💡 NOTE:** This method uses the PNG encoder to convert between formats to support transparency. Higher frame rates may be achieved using different encoders (at the expense of memory).

### 3. Create a `Render()` Method

Here's the star of the show! It's nearly identical to the [Windows Forms quickstart](../quickstart-winforms/) example, with key differences being:

* Bitmap size is determined by the `ActualWidth` and `ActualHeight` of the `Canvas`
* At the end of the render we copy the output to the `Source` property of an `Image`.

```cs
Random rand = new Random();
private void Render()
{
    using (var bmp = new Bitmap((int)myCanvas.ActualWidth, (int)myCanvas.ActualHeight))
    using (var gfx = Graphics.FromImage(bmp))
    using (var pen = new System.Drawing.Pen(System.Drawing.Color.White))
    {
        // draw one thousand random white lines on a dark blue background
        gfx.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
        gfx.Clear(System.Drawing.Color.Navy);
        for (int i = 0; i < 1000; i++)
        {
            var pt1 = new System.Drawing.Point(rand.Next(bmp.Width), rand.Next(bmp.Height));
            var pt2 = new System.Drawing.Point(rand.Next(bmp.Width), rand.Next(bmp.Height));
            gfx.DrawLine(pen, pt1, pt2);
        }

        myImage.Source = BmpImageFromBmp(bmp);
    }
}
```

### Call `Render()`

The Render method won't run unless we tell it to. Let's have it run when the program launches, when the window is resized, and when we click on the canvas.

```cs
private void myCanvas_Loaded(object sender, RoutedEventArgs e) => Render();
private void Window_SizeChanged(object sender, SizeChangedEventArgs e) => Render();
private void myCanvas_MouseDown(object sender, MouseButtonEventArgs e) => Render();
```

## Improve this Application with Threading

This is a good start at drawing in a WPF application, but it has a flaw: calls to the render method block the GUI thread. This isn't a problem if your renderer is very fast, but as it gets slow your GUI will become less responsive. A later article will demonstrate how to move the renderer into another thread so you can draw and animate graphics without blocking the GUI thread.

## Source Code

* [MainWindow.xaml](https://github.com/swharden/Csharp-Data-Visualization/blob/main/dev/old/drawing/quickstart-wpf/MainWindow.xaml)
* [MainWindow.xaml.cs](https://github.com/swharden/Csharp-Data-Visualization/blob/main/dev/old/drawing/quickstart-wpf/MainWindow.xaml.cs)