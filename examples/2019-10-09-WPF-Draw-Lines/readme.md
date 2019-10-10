# Drawing Lines with WPF

Most of my code examples use `System.Drawing` to draw lines onto Bitmap images. This example uses `System.Windows.Media` to create lines (`LineGeometry` objects), style them (creating a `Path`), then add them to a `Canvas`.

![](screenshot.png)

### XAML
```XAML
<Canvas Grid.Row="1" Background="Gray" Name="myCanvas" />
```

### C#
```cs
myCanvas.Children.Clear();

for (int i=0; i<1_000; i++)
{
    LineGeometry myLineGeometry = new LineGeometry
    {
        myLineGeometry.StartPoint = new Point(randomX, randomY);
        myLineGeometry.EndPoint = new Point(randomX, randomY);
    }

    Path myPath = new Path
    {
        Stroke = Brushes.Black,
        StrokeThickness = 1,
        Data = myLineGeometry
    };

    myCanvas.Children.Add(myPath);
}
```

### Performance
* Full-screen rendering of just a few lines is extremely fast. This task would be slow with System.Drawing, which slows down as the resolution of the image increases.
* As more lines are added, speed decreases significantly. Around 1,000 lines at full screen resolution performance is so slow that System.Drawing is actually faster.
* If your task requires drawing lines and moving them around the screen, WPF would probably be faster. My task requires creating a large number of new lines on every render, which WPF doesn't seem able to do fast enough to be meaningfully interactive.