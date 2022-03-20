---
title: Plot Data with OxyPlot
description: How to plot data in C# projects using OxyPlot
date: 2020-05-17
lastmod: 2022-03-16
weight: 20
---

**OxyPlot is a 2D plotting library for .NET that has been actively developed since 2010.** OxyPlot is MIT-licensed and has components for an impressive number of modern platforms (WinForms, WPF, UWP, Xamarin, XWT) and some legacy ones too (Silveright and Windows Phone). The WinForms control ([PlotView.cs](https://github.com/oxyplot/oxyplot/blob/develop/Source/OxyPlot.WindowsForms/PlotView.cs)) renders using System.Drawing, but a rendering systems using SkiaSharp and ImageSharp also exist. OxyPlot was created to plot 2D data which is why it has "xy" in its name.

<img src="oxyplot-quickstart.gif" class="d-block mx-auto my-3">

### Interactive Controls

* left click to show X/Y value of the point under the cursor
* right-click-drag to pan
* mouse-wheel-scroll to zoom
* mouse-wheel-scroll over an axis to zoom in one axis

## Quickstart

* Create a Windows Forms application

* Add the `OxyPlot.WindowsForms` NuGet package

* Drag a `PlotView` from the Toolbox onto your form

### Generate Sample Data

This code generates random data we can practice plotting

```cs
private Random rand = new Random(0);
private double[] RandomWalk(int points = 5, double start = 100, double mult = 50)
{
    // return an array of difting random numbers
    double[] values = new double[points];
    values[0] = start;
    for (int i = 1; i < points; i++)
        values[i] = values[i - 1] + (rand.NextDouble() - .5) * mult;
    return values;
}
```

### Scatter Plot

**Interacting with OxyPlot is achieved by constructing data objects and passing them around.** Graphing data requires creating data series objects (like lines and bars), populating them with data, then putting them into a plot model, then loading that model into a view (like a user control).

<img src="oxyplot-quickstart-scatter-plot.png" class="d-block mx-auto shadow my-5">

```cs
// generate some random XY data
int pointCount = 1_000;
double[] xs1 = RandomWalk(pointCount);
double[] ys1 = RandomWalk(pointCount);
double[] xs2 = RandomWalk(pointCount);
double[] ys2 = RandomWalk(pointCount);

// create lines and fill them with data points
var line1 = new OxyPlot.Series.LineSeries()
{
    Title = $"Series 1",
    Color = OxyPlot.OxyColors.Blue,
    StrokeThickness = 1,
    MarkerSize = 2,
    MarkerType = OxyPlot.MarkerType.Circle
};

var line2 = new OxyPlot.Series.LineSeries()
{
    Title = $"Series 2",
    Color = OxyPlot.OxyColors.Red,
    StrokeThickness = 1,
    MarkerSize = 2,
    MarkerType = OxyPlot.MarkerType.Circle
};

for (int i = 0; i < pointCount; i++)
{
    line1.Points.Add(new OxyPlot.DataPoint(xs1[i], ys1[i]));
    line2.Points.Add(new OxyPlot.DataPoint(xs2[i], ys2[i]));
}

// create the model and add the lines to it
var model = new OxyPlot.PlotModel
{
    Title = $"Scatter Plot ({pointCount:N0} points each)"
};
model.Series.Add(line1);
model.Series.Add(line2);

// load the model into the user control
plotView1.Model = model;
```

### Bar Graph

<img src="oxyplot-quickstart-bar-graph.png" class="d-block mx-auto shadow my-5">

```cs
// generate some random Y data
int pointCount = 5;
double[] ys1 = RandomWalk(pointCount);
double[] ys2 = RandomWalk(pointCount);

// create a series of bars and populate them with data
var seriesA = new OxyPlot.Series.ColumnSeries()
{
    Title = "Series A",
    StrokeColor = OxyPlot.OxyColors.Black,
    FillColor = OxyPlot.OxyColors.Red,
    StrokeThickness = 1
};

var seriesB = new OxyPlot.Series.ColumnSeries()
{
    Title = "Series B",
    StrokeColor = OxyPlot.OxyColors.Black,
    FillColor = OxyPlot.OxyColors.Blue,
    StrokeThickness = 1
};

for (int i = 0; i < pointCount; i++)
{
    seriesA.Items.Add(new OxyPlot.Series.ColumnItem(ys1[i], i));
    seriesB.Items.Add(new OxyPlot.Series.ColumnItem(ys2[i], i));
}

// create a model and add the bars into it
var model = new OxyPlot.PlotModel
{
    Title = "Bar Graph (Column Series)"
};
model.Axes.Add(new OxyPlot.Axes.CategoryAxis());
model.Series.Add(seriesA);
model.Series.Add(seriesB);

// load the model into the user control
plotView1.Model = model;
```

## Performance

I found the line plot to be relatively fast (1 million points display at a rate of few FPS).

<img src="oxyplot-quickstart-line-plot.png" class="d-block mx-auto shadow my-5">

```cs
// generate some random Y data
int pointCount = 1000_000;
double[] xs = Consecutive(pointCount);
double[] ys1 = RandomWalk(pointCount);
double[] ys2 = RandomWalk(pointCount);

// create lines and fill them with data points
var line1 = new OxyPlot.Series.LineSeries()
{
    Title = $"Series 1",
    Color = OxyPlot.OxyColors.Blue,
    StrokeThickness = 1,
};

var line2 = new OxyPlot.Series.LineSeries()
{
    Title = $"Series 2",
    Color = OxyPlot.OxyColors.Red,
    StrokeThickness = 1,
};

for (int i = 0; i < pointCount; i++)
{
    line1.Points.Add(new OxyPlot.DataPoint(xs[i], ys1[i]));
    line2.Points.Add(new OxyPlot.DataPoint(xs[i], ys2[i]));
}

// create the model and add the lines to it
var model = new OxyPlot.PlotModel
{
    Title = $"Line Plot ({pointCount:N0} points each)"
};
model.Series.Add(line1);
model.Series.Add(line2);

// load the model into the user control
plotView1.Model = model;
```

## Create Plots in Console Applications

To use OxyPlot in a console application create your model the same as before, but use a file exporter to save your model to a file rather than display it in a user control.

```cs
OxyPlot.WindowsForms.PngExporter.Export(model, "test.png", 400, 300, OxyPlot.OxyColors.White);
```

A platform-specific `PngExporter` is provided with whatever NuGet package you installed. This code example uses the `PngExporter` in the `OxyPlot.WindowsForms` package, but installing `OxyPlot.Core.Drawing` may be the way to go for true console applications.

<img src="oxyplot-console-quickstart.png" class="d-block mx-auto shadow my-5 border">

## Resources
* Documentation: [Introduction to OxyPlot](https://oxyplot.readthedocs.io/en/latest/introduction/introduction.html)
* GitHub: [OxyPlot](https://github.com/oxyplot/oxyplot)
* Graph types: [/models/index.html](https://oxyplot.readthedocs.io/en/latest/models/index.html)
* Documentation: GitHub [oxyplot/documentation-examples](https://github.com/oxyplot/documentation-examples)
* Note: the domain oxyplot.org is no longer associated with this project

## Source Code
* [oxyplot quickstart](https://github.com/swharden/Csharp-Data-Visualization/tree/main/dev/old/plotting/oxyplot/OxyPlotQuickstart)