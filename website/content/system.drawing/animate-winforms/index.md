---
Title: Animating Graphics in Windows Forms
Description: Update and render a platform-agnostic graphics model using Windows Forms
date: 2020-04-20
weight: 50
---

**This page demonstrates a simple method for animating graphics in Windows Forms.** In this example we use the [Starfield graphics model](../animate-starfield) which keeps track of star positions and has a `Render()` method that will draw the field onto an existing `Bitmap`. Controls on the Form let the user adjust the number of stars and the transparency of stars in real time. These controls serve a secondary purpose as tools to assess GUI responsiveness when the rendering system is under load.

<img src="csharp-starfield-windows-forms.gif" class="d-block mx-auto my-5">

> 💡 **Separating graphics operations from GUI management facilitates testing and promotes maintainability.** In this design rendering tasks are performed in the graphics model and platform-specific GUI tasks are isolated in their own project. This strategy allows us to create identical WPF and WinForms applications that use the same `Render()` method in the graphics model.

## Code

By isolating all rendering methods in our graphics model library, our GUI code remains refreshingly simple. This is all it takes to continuously render our starfield animation and display it in the Form:

```cs
readonly Field field = new Field(500);

public Form1()
{
    InitializeComponent();
}

private void timer1_Tick(object sender, EventArgs e)
{
    // advance the graphics model and render a new Bitmap
    field.Advance();
    Bitmap bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
    byte alpha = (byte)(trackBar1.Value * 255 / 100);
    Color starColor = Color.FromArgb(alpha, Color.White);
    field.Render(bmp, starColor);

    // replace the old Bitmap and dispose of the old one
    var oldImage = pictureBox1.Image;
    pictureBox1.Image = bmp;
    oldImage?.Dispose();
}
```

> 💡 **Manual double-buffering may improve performance.** Creating a new `Bitmap` and disposing an old one on every `Render()` is costly. It's not a good idea to render directly on the `Bitmap` currently displayed in a `PictureBox` because it can produce tearing artifacts. Instead you could continuously hold two `Bitmap` objects in memory and alternate between the two (displaying one while drawing on the other). This strategy is called [multiple buffering](https://en.wikipedia.org/wiki/Multiple_buffering).

### Theory

The `Timer` triggers this sequence of events:

* Advance the starfield model in time
* Create a `Bitmap` the size of the `Picturebox`
* Call the starfield's `Render()` method (passing-in the `Bitmap` to be drawn on)
* Apply the `Bitmap` to the `Picturebox.Image` property

> **⚠️ WARNING: This method blocks the GUI thread while rendering.** This is not noticeable when renders are fast (500 stars), but this results in an unresponsive application when the render takes a lot of CPU effort (100,000 stars). We will explore how to render graphics without blocking the GUI thread in a future article.

> 💡 Assigning a `Bitmap` to the `Image` property of a `Picturebox` lets us take advantage of built-in double-buffering capabilities for flicker-free animations.

> 💡 1 ms is a good value to use for your render timer. Since the timer isn't multi-threaded, it will take as long as it needs to perform the render but leave 1 ms free between renders to respond to mouse events and other GUI updates.

> 💡 Install the `System.Drawing.Common` NuGet package even if you don't think you need it. Using the common library instead of native `System.Drawing` will ensure your program can be compiled for .NET Core if you decide to upgrade later. It also ensures you can pass `System.Drawing` objects to .NET Standard libraries which use `System.Drawing.Common` under the hood.

## Source Code

* GitHub: [Starfield Windows Forms GUI](https://github.com/swharden/Csharp-Data-Visualization/blob/master/dev/old/drawing/starfield/Starfield.WinForms)