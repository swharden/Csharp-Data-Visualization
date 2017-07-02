# ScottPlot Brainstorming

## Package Format
portable library? custom widget / thing?

# API Examples (C#)
I'll first write the usage examples, then code around them:

## Version 1: single X/Y
```C#
internal ScottPlot.ScottPlot SP = new ScottPlot.ScottPlot(); // uses default height/width
SP.Ys = SP.ListSin(); // create some sine wave data as a demo
SP.Xs = SP.ListSequence(); // make the Xs just an ascending series of numbers
pictureBox1.BackgroundImage = SP.Render(this.pictureBox1.Width, this.pictureBox1.Height);
this.Refresh(); // force the window to redraw
```

## Version 2: multiple lines
* Plots are NOT stored in memory, they're drawn at the time they're called.
* Therefore complex plots cannot be dynamically resized.
```C#
internal ScottPlot.ScottPlot SP = new ScottPlot.ScottPlot();

SP.PlotVline(X, color, width);
SP.PlotHline(Y, color, width);

SP.PlotVspan(X, color, width);
SP.PlotHspan(Y, color, width);

SP.PlotLine(Ys, Xs, color, width);
SP.PlotScatter(Ys, Xs, color, width);

pictureBox1.BackgroundImage = SP.Render(this.pictureBox1.Width, this.pictureBox1.Height);
this.Refresh(); // force the window to redraw
```
