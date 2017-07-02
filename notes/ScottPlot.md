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

SP.Size(this.pictureBox1.Width, this.pictureBox1.Height);

SP.AxisAuto(Xs, Ys);

SP.PlotVline(X, color, width);
SP.PlotHline(Y, color, width);

SP.PlotVspan(X1, X2, color, width);
SP.PlotHspan(Y1, Y2, color, width);

SP.PlotLine(Ys, Xs, color, width);
SP.PlotScatter(Ys, Xs, color, width);

pictureBox1.BackgroundImage = SP.Render();
pictureBox1.BackgroundImage = SP.Render1d(.001, 300); // for evenly spaced data (give it spacing and offset)

this.Refresh(); // force the window to redraw
```

### Recyclable axis object
```C#
AX = new ScottPlot.Axis();
AX.Size(Xpx, Ypx);

// permanently change axis
AX.Change(X1, X2, Y1, Y2); // permanently change this axis
AX.Zoom(Xscale, Yscale, centerXfrac, centerYfrac);
AX.Pan(Xpx, Ypx);

// temporarially change axis
AX.MouseDown(X, Y);
AX.MousePan(X, Y);
AX.MouseZoom(X, Y);
AX.MouseUp(X, Y); // calls AX.Change

AX.View() // get uncommitted mouse drag (X1, X2, Y1, Y2)
```
