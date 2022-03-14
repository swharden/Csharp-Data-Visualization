---
Title: Animating Graphics in Windows Forms
Description: Update and render a platform-agnostic graphics model using Windows Forms
date: 2020-04-20
---

This page demonstrates a simple method for animating graphics in Windows Forms. In this example we use the [Starfield graphics model](4-starfield.md.html) which keeps track of star positions and has a `Render()` method that will draw the field onto an existing `Bitmap`.

Controls on the Form let the user adjust the number of stars and the transparency of stars in real time. These controls serve a secondary purpose as tools to assess GUI responsiveness when the rendering system is under load.

<div align="center">

![](files/csharp-starfield-windows-forms.gif)

</div>

> ðŸ’¡ This architecture does a good job separating concerns: rendering tasks are performed in the graphics model, and GUI tasks (which are often platform-specific) are isolated in the GUI. By encapsulating the rendering code inside the graphics model, we can develop multiple GUI systems that share the same rendering module. For example, an identical WPF application will be created that uses the same rendering system.

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
    field.Advance();
    Bitmap bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
    byte alpha = (byte)(trackBar1.Value * 255 / 100);
    Color starColor = Color.FromArgb(alpha, Color.White);
    field.Render(bmp, starColor);
    pictureBox1.Image?.Dispose();
    pictureBox1.Image = bmp;
}
```
> **âš ï¸ Don't dispose your `Bitmap` after assigning it to a Picturebox `Image`** is because the Picturebox is double-buffered and will read from the assigned `Bitmap` later (after this function ends). Instead, leave the `Bitmap` in memory (assigned to the `Image` property) and dispose of it in the next tick immediately before you assign a new `Bitmap` to the same property.

### Theory

The `Timer` triggers this sequence of events:

* Advance the starfield model in time
* Create a `Bitmap` the size of the `Picturebox`
* Call the starfield's `Render()` method (passing-in the `Bitmap` to be drawn on)
* Apply the `Bitmap` to the `Picturebox.Image` property

> **âš ï¸ WARNING: This method blocks the GUI thread while rendering.** This is not noticeable when renders are fast (500 stars), but this results in an unresponsive application when the render takes a lot of CPU effort (100,000 stars). We will explore how to render graphics without blocking the GUI thread in a future article.

> ðŸ’¡ Assigning a `Bitmap` to the `Image` property of a `Picturebox` lets us take advantage of built-in double-buffering capabilities for flicker-free animations.

> ðŸ’¡ 1 ms is a good value to use for your render timer. Since the timer isn't multi-threaded, it will take as long as it needs to perform the render but leave 1 ms free between renders to respond to mouse events and other GUI updates.

> ðŸ’¡ Install the System.Drawing.Common NuGet package even if you don't think you need it. Using the common library instead of native System.Drawing will ensure your program can be compiled for .NET Core if you decide to upgrade later. It also ensures you can pass System.Drawing objects to .NET Standard libraries which use System.Drawing.Common under the hood.


## Download Source Code

_This code targets .NET Framework, but it will also compile using .NET Core_

* View on GitHub: [Form1.cs](https://github.com/swharden/Csharp-Data-Visualization/blob/master/dev/old/drawing/starfield/Starfield.WinForms/Form1.cs)

* Download this project: [starfield.zip](files/starfield.zip)