# Interactive Plot with Pan and Zoom
The [`ScottPlot` class](swharden_demo/ScottPlot.cs) now has an advanced axis system. This makes it easily to set the viewing window in unit coordinates (X1, X2, Y1, Y2) and also do things like zoom and pan. This example was made to demonstrate these functions, as well as compare the speed of interactive graph manipulation at different sizes and with different quality settings. Although the GUI has many features, [Form1.cs](swharden_demo/Form1.cs) is not overwhelmingly complex.

**BUG:** It seems if you click and drag too slowly, the thing thinks you stopped dragging and it stops responding until you click again. I'm pretty sure it's because I didn't set `mouseCalculating = false;` when [click and drag mouse movement is detected as zero](swharden_demo/Form1.cs#L91)...

![](demo.gif)
