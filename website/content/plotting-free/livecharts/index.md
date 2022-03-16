---
title: Plot Data with LiveCharts
description: How to plot data in a .NET application using the LiveCharts library
date: 3333-01-01
---

LiveCharts is a .NET charing library that is free (under a MIT license), but sells an enhancement called the "Geared package" which improves performance using DirectX rendering (provided by [SharpDX](http://sharpdx.org/) which is now abandoned). LiveCharts tries to produce clean-looking animated charts and is open about the fact that performance is a secondary concern. 

<div align="center">

![](graphics/livecharts-quickstart.gif)

</div>

The [LiveCharts GitHub page](https://github.com/Live-Charts/Live-Charts) claims version 1.0 is about to be released, but the last commit on the was in 2018 so it seems development has stalled or been abandoned. The issues page has a growing list of hundreds of issues, but no issues have been closed for a long time.

## Quickstart

LiveCharts is designed to work in WPF applications, but for consistency with the other examples in this series the quickstart will be made using Windows Forms

* Install the `LiveCharts.WinForms` package
* Drag a `CartesianChart` from the toolbox onto your form

### Sample Data

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

### Bar Graph

<div align="center">

![](graphics/livecharts-quickstart-bar-graph.png)

</div>

```cs
// generate some random Y data
int pointCount = 5;
double[] ys1 = RandomWalk(pointCount);
double[] ys2 = RandomWalk(pointCount);

// create series and populate them with data
var series1 = new LiveCharts.Wpf.ColumnSeries
{
    Title = "Group A",
    Values = new LiveCharts.ChartValues<double>(ys1)
};

var series2 = new LiveCharts.Wpf.ColumnSeries()
{
    Title = "Group B",
    Values = new LiveCharts.ChartValues<double>(ys2)
};

// display the series in the chart control
cartesianChart1.Series.Clear();
cartesianChart1.Series.Add(series1);
cartesianChart1.Series.Add(series2);
```

### Scatter Plot

<div align="center">

![](graphics/livecharts-quickstart-scatter-plot.png)

</div>

```cs
// generate some random XY data
int pointCount = 100;
double[] xs1 = RandomWalk(pointCount);
double[] ys1 = RandomWalk(pointCount);
double[] xs2 = RandomWalk(pointCount);
double[] ys2 = RandomWalk(pointCount);

// create series and populate them with data
var series1 = new LiveCharts.Wpf.ScatterSeries
{
    Title = "Group A",
    Values = new LiveCharts.ChartValues<LiveCharts.Defaults.ObservablePoint>(),
    PointGeometry = LiveCharts.Wpf.DefaultGeometries.Circle
};

var series2 = new LiveCharts.Wpf.ScatterSeries()
{
    Title = "Group B",
    Values = new LiveCharts.ChartValues<LiveCharts.Defaults.ObservablePoint>(),
    PointGeometry = LiveCharts.Wpf.DefaultGeometries.Circle
};

for (int i = 0; i < pointCount; i++)
{
    series1.Values.Add(new LiveCharts.Defaults.ObservablePoint(xs1[i], ys1[i]));
    series2.Values.Add(new LiveCharts.Defaults.ObservablePoint(xs2[i], ys2[i]));
}

// display the series in the chart control
cartesianChart1.Series.Clear();
cartesianChart1.Series.Add(series1);
cartesianChart1.Series.Add(series2);
```

### Line Plot

<div align="center">

![](graphics/livecharts-quickstart-line-plot.png)

</div>

```cs
// generate some random Y data
int pointCount = 200;
double[] ys1 = RandomWalk(pointCount);
double[] ys2 = RandomWalk(pointCount);

// create series and populate them with data
var series1 = new LiveCharts.Wpf.LineSeries()
{
    Title = "Group A",
    Values = new LiveCharts.ChartValues<double>(ys1),
};

var series2 = new LiveCharts.Wpf.LineSeries()
{
    Title = "Group B",
    Values = new LiveCharts.ChartValues<double>(ys2),
};

// display the series in the chart control
cartesianChart1.Series.Clear();
cartesianChart1.Series.Add(series1);
cartesianChart1.Series.Add(series2);
```

## Zooming and Panning

You can enable zooming and panning by modifying the user control after a series is plotted like this:

```cs
cartesianChart1.Zoom = LiveCharts.ZoomingOptions.Xy;
```

However mouse interaction is so sluggish I did not find it useful and intentionally omitted it from the quickstart example.

## Resources
* https://lvcharts.net/

## Source Code

* [/dev/old/plotting/livecharts](https://github.com/swharden/Csharp-Data-Visualization/tree/master/dev/old/plotting/livecharts)