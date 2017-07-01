# ScottPlot Brainstorming

## Package Format
portable library? custom widget / thing?

# API Examples (C#)
I'll first write the usage examples, then code around them:
```C#
internal ScottPlot.ScottPlot SP = new ScottPlot.ScottPlot();
SP.FigureInit(this.pictureBox1.Width, this.pictureBox1.Height);
SP.Ys = SP.ListSin();
SP.Xs = SP.ListSequence();
pictureBox1.BackgroundImage = SP.Render();
this.Refresh();
```
