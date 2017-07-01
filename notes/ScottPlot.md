# ScottPlot Brainstorming

## Package Format
portable library? custom widget / thing?

# API Examples (C#)
I'll first write the usage examples, then code around them:
```C#
internal ScottPlot.ScottPlot SP = new ScottPlot.ScottPlot(); // uses default height/width
SP.Ys = SP.ListSin(); // create some sine wave data as a demo
SP.Xs = SP.ListSequence(); // make the Xs just an ascending series of numbers
pictureBox1.BackgroundImage = SP.Render(this.pictureBox1.Width, this.pictureBox1.Height);
this.Refresh(); // force the window to redraw
```
