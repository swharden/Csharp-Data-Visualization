# Plot Data with ScottPlot

[ScottPlot](https://swharden.com/scottplot) is an interactive plotting library for .NET published under the permissive MIT license. It's definitely the new kid on the block, with its first public release on NuGet in 2019. It has user controls for Windows Forms and WPF, and the core library has its own package allowing plots to be created in server environments or console applications and saved as images. ScottPlot targets .NET Standard 2.0 so it can be used in both .NET Core and .NET Framework applications.

<div align="center">

![](graphics/scottplot-quickstart.gif)

</div>

ScottPlot's API is argument-based (modeled after [Matplotlib](https://matplotlib.org/) for Python), and most plots can be created with a single line of code (customized as desired with optional arguments). 

### Interactive Controls
* left-click-drag: pan
* right-click-drag: zoom
* middle-click-drag: zoom region
* scroll-wheel: zoom
* middle-click: fit data
* right-click: deploy menu

### ScottPlot Cookbook

The [ScottPlot Cookbook](https://swharden.com/scottplot/cookbook) is an extensive collection of sample plots paired with the source code used to create them. Reviewing the cookbook is the best way to survey ScottPlot's capabilities and learn how to use it. An interactive version of each cookbook figure is presented in the [ScottPlot demo](https://swharden.com/scottplot/demo) application.

[![](graphics/scottplot-cookbook.png)](https://swharden.com/scottplot/cookbook)

## Code
* Install the `ScottPlot.WinForms` NuGet package
* Drag a `FormsPlot` from the toolbox onto your form

### Generate Sample Data

ScottPlot comes with a `DataGen` class for generating sample data so we don't have to write our own. We will use its `RandomWalk()` and `Consecutive()` methods. 

Let's prepare a `Random` object and store it at the class-level for use later.

```cs
Random rand = new Random(0);
```

### Scatter Plot

It's hard to get more self-explanatory than this!

<div align="center">

![](graphics/scottplot-quickstart-scatter-plot.png)

</div>

```cs
// generate some random X/Y data
int pointCount = 500;
double[] xs1 = ScottPlot.DataGen.RandomWalk(rand, pointCount);
double[] ys1 = ScottPlot.DataGen.RandomWalk(rand, pointCount);
double[] xs2 = ScottPlot.DataGen.RandomWalk(rand, pointCount);
double[] ys2 = ScottPlot.DataGen.RandomWalk(rand, pointCount);

// plot the data
formsPlot1.Reset();
formsPlot1.plt.PlotScatter(xs1, ys1);
formsPlot1.plt.PlotScatter(xs2, ys2);

// additional styling
formsPlot1.plt.Title($"Scatter Plot ({pointCount} points per group)");
formsPlot1.plt.XLabel("Horizontal Axis Label");
formsPlot1.plt.YLabel("Vertical Axis Label");
formsPlot1.Render();
```

### Line Plot

Here we plot data using `PlotSignal()` instead of `PlotScatter()`. Signal plots are performance-optimized to display evenly-spaced data (signals) and can interactively display arrays with tens of millions of data points. 

The `PlotSignalConst()` method achieves even greater performance by performing extra calculations when the data is first loaded to reducing the computational work required to render each frame.

<div align="center">

![](graphics/scottplot-quickstart-line-plot.png)

</div>

```cs
// generate some random Y data
int pointCount = 10_000;
double[] ys1 = ScottPlot.DataGen.RandomWalk(rand, pointCount);
double[] ys2 = ScottPlot.DataGen.RandomWalk(rand, pointCount);

// plot the data
formsPlot1.Reset();
formsPlot1.plt.PlotSignal(ys1);
formsPlot1.plt.PlotSignal(ys2);

// additional styling
formsPlot1.plt.Title($"Line Plot ({10_000:N0} points each)");
formsPlot1.plt.XLabel("Horizontal Axis Label");
formsPlot1.plt.YLabel("Vertical Axis Label");
formsPlot1.Render();
```

### Bar Graph

A single-series bar plot can be created like this:

<div align="center">

![](graphics/scottplot-quickstart-simple-bar-graph.png)

</div>

```cs
// generate some random data
int pointCount = 5;
double[] xs = ScottPlot.DataGen.Consecutive(pointCount);
double[] ys = ScottPlot.DataGen.RandomWalk(rand, pointCount, mult: 50, offset: 100);

// plot the data
formsPlot1.Reset();
formsPlot1.plt.PlotBar(xs, ys);

// additional styling
formsPlot1.plt.Title("Simple Bar Graph");
formsPlot1.plt.XLabel("Horizontal Axis Label");
formsPlot1.plt.YLabel("Vertical Axis Label");
formsPlot1.plt.Axis(y1: 0);
formsPlot1.Render();
```

### Grouped Bar Plot

A more advanced method exists for plotting _grouped_ series of bar graph data. To use it just pass-in labels for the groups and series, and a 2D array of the bar heights.

<div align="center">

![](graphics/scottplot-quickstart-bar-graph.png)

</div>

```cs
// generate some random Y data
int pointCount = 5;
double[] ys1 = ScottPlot.DataGen.RandomWalk(rand, pointCount, mult: 50, offset: 100);
double[] ys2 = ScottPlot.DataGen.RandomWalk(rand, pointCount, mult: 50, offset: 100);

// collect the data into groups
string[] groupLabels = { "One", "Two", "Three", "Four", "Five" };
string[] seriesLabels = { "Group A", "Group B" };
double[][] barHeights = { ys1, ys2 };

// plot the data
formsPlot1.Reset();
formsPlot1.plt.PlotBarGroups(groupLabels, seriesLabels, barHeights);

// additional styling
formsPlot1.plt.Title("Bar Graph");
formsPlot1.plt.XLabel("Horizontal Axis Label");
formsPlot1.plt.YLabel("Vertical Axis Label");
formsPlot1.plt.Legend(location: ScottPlot.legendLocation.upperLeft);
formsPlot1.plt.Axis(y1: 0);
formsPlot1.Render();
```

## Resources
* ScottPlot website: https://swharden.com/scottplot
  * Cookbook: https://swharden.com/scottplot/cookbook
  * Demo: https://swharden.com/scottplot/demo
* ScottPlot on GitHub: https://github.com/swharden/scottplot

## Source Code
* [/dev/old/plotting/scottplot](https://github.com/swharden/Csharp-Data-Visualization/blob/master/dev/old/plotting/scottplot)