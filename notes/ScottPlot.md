# ScottPlot Brainstorming

## Package Format
portable library? custom widget / thing?

# API Examples (C#)
I'll first write the usage examples, then code around them:


## Basics 

##### Start a new ScottPlot instance
```c#
using ScottPlot;
var SP = new ScottPlot(); // defaults to 800x600
```

##### Add X/Y data
```c#
SP.Xs = new List<double>() {10,20,30,40};
SP.Ys = new List<double>() {76,54,12,96};
```

#### Draw the Graph (to a bitmap in memory)
```c#
SP.Graph(); // clears and redraws entire graph
SP.GraphAdd(); // adds new Xs and Ys on top of existing raph
SP.GraphData(); // clears and redraws the data area only
```

## Displaying the Graph

##### Launch a pop-up window
```c#
SP.Show();
```

##### Graph to a pictureBox
```c#
pictureBox1.BackgroundImage = SP.Graph_buffer;
```

##### Save to file
```c#
SP.SaveFig("out.jpg");
```

## Axis / Axes

##### Manually define (or read) axis limits
```c#
SP.AxisX1=40;
SP.AxisX2=60;
SP.AxisY1=0.5;
SP.AxisY1=1.5;
```

##### Automatically determine axis by margin
```c#
SP.AxisMargins(0, 0.1); // 0% horizontal margin, 10% vertical padding
SP.AxisAuto(); // determins padding based on optimal grid lines
```

##### Interactive panning and zooming
```c#
SP.translateXpx=5;
SP.translateYpx=-15;
```

```c#
SP.zoomXpx=5;
SP.zoomYpx=-15;
```

## Misc
```c#
// Automatically generate Xs if you only have Ys
SP.GenerateXs(); // [1, 2, 3, ...]
SP.GenerateXs(5); // [5, 10, 15, ...]
```

```c#
SP.GenerateYsSin(1000); // creates time-based sine wave with 1000 points
SP.GenerateYsRand(1000); // creates 1000 points of random data
```
