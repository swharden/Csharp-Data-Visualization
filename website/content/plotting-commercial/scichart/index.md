---
title: SciChart
description: A closer look at interactive scientific charting provided by SciChart
date: 2020-04-01
lastmod: 2022-03-17
---

**SciChart is a commercial charting library for .NET for high performance interactive charting.** Although they have mobile (iOS and Android) libraries, here we will take a closer look at their WPF charts library. It may be possible to use this control in Windows Forms applications with [ElementHost](https://docs.microsoft.com/en-us/previous-versions/dotnet/netframework-3.5/ms754008(v=vs.90)) or [more modern](https://docs.microsoft.com/en-us/dotnet/framework/wpf/advanced/walkthrough-hosting-a-wpf-composite-control-in-windows-forms) techniques, but their core product is intended for use in WPF applications.

<img src="scichart-demo.gif" class="d-block mx-auto my-4">

**SciChart strikes a nice balance of performance and aesthetic.** These charts benefit from DirectX (Windows), OpenGL (Android), or Metal (iOS) hardware acceleration, and controls are extensively themeable and have many customizable behaviors. The SciChart trial application demonstrates many plot types, and the intuitive controls, well-balanced visuals, and subtle animations are the best I've seen in this class of software.

## Price
According to the [SciChart store](https://store.scichart.com/) in 2022:

* $1,699 / year per developer for binaries for 2D charts for Windows
* $1,999 / year per developer for binaries for 2D charts for all platforms
* $2,299 / year per developer for binaries for 2D and 3D charts for Windows
* $2,999 / year per developer for binaries for 2D and 3D charts for all platforms
* $3,999 / year per developer for full source code

## Demo

The [SciChart examples website](https://www.scichart.com/example/) shows many sample charts with source code. Let's take a closer look at the [line chart example](https://www.scichart.com/example/wpf-line-chart-example/) shown in the screenshot at the top of the page. 

> **Warning:** It appears you are not able to evaluate the [demos](https://www.scichart.com/examples/wpf-chart/) on your machine without registering your personal information to obtain a trial application ☠️

<img src="scichart-demos.jpg" class="d-block mx-auto shadow my-4 border">

Most styling and behavior customization is achieved by writing XAML:

```xml
<ext:SciChartInteractionToolbar TargetSurface="{Binding Source={x:Reference Name=sciChart}}"/>

<!--  Create the chart surface  -->
<s:SciChartSurface x:Name="sciChart" Grid.Column="1">

    <!--  Declare RenderableSeries  -->
    <s:SciChartSurface.RenderableSeries>
        <s:FastLineRenderableSeries x:Name="lineRenderSeries" Stroke="#FF99EE99" StrokeThickness="2">
            <s:FastLineRenderableSeries.SeriesAnimation>
                <s:SweepAnimation AnimationDelay="0:0:1" Duration="0:0:5"/>
            </s:FastLineRenderableSeries.SeriesAnimation>
        </s:FastLineRenderableSeries>
    </s:SciChartSurface.RenderableSeries>

    <!--  Create an X Axis with GrowBy  -->
    <s:SciChartSurface.XAxis>
        <s:NumericAxis DrawMajorBands="True" FlipCoordinates="True" GrowBy="0.1, 0.1"/>
    </s:SciChartSurface.XAxis>

    <!--  Create a Y Axis with GrowBy. Optional bands give a cool look and feel for minimal performance impact  -->
    <s:SciChartSurface.YAxis>
        <s:NumericAxis DrawMajorBands="True" GrowBy="0.5, 0.5"/>
    </s:SciChartSurface.YAxis>

</s:SciChartSurface>
```

Data must be loaded into custom objects to be displayed in the chart:

```cs
private void LineChartExampleView_OnLoaded(object sender, RoutedEventArgs e)
{            
    // Create a DataSeries of type X=double, Y=double
    var dataSeries = new XyDataSeries<double, double>();

    lineRenderSeries.DataSeries = dataSeries;

    var data = DataManager.Instance.GetFourierSeries(1.0, 0.1);

    // Append data to series. SciChart automatically redraws
    dataSeries.Append(data.XData, data.YData);
    
    sciChart.ZoomExtents();
}
```

## Performance

The SciChart demo has a performance example which simulates live incoming data (1,000 points every 20 ms). Comfortable interactive frame rates are achieved even when the dataset has millions of points. This example disables mouse interaction while data is being updated and it's not entirely clear to me why, but overall these demos are quite impressive!

<img src="scichart-performance.gif" class="d-block mx-auto my-4">

## Conclusions

**SciChart's excellent performance and extensive documentation makes it a very strong contender in the .NET charting space.** It is very expensive, but this may be a good fit for large companies with big budgets or niche software products with high profit margins.

* I like how their demo application pairs working examples with source code, but I am disappointed you are required to share your personal information and sign-up for a trial just to run it.

* Their model of customization through extensive XAML editing makes sense, but requires extensive domain knowledge to get started and is obviously targeted at WPF experts.

* I found [Comparison of SciChart vs. Open Source Chart controls](https://www.scichart.com/comparison-of-scichart-vs-open-source-chart-controls/) fascinating. They raise some compelling arguments why you may _not_ want use open-source software. As an open-source maintainer it's difficult for me to evaluate these arguments without bias. Personally, I'm not particularly impressed by how many bugs have been fixed in a product, or how quick a company is to sell me technical support. I would prefer a software product that is simple, does not require frequent bug fixes, and is easy enough to use that it does not require additional paid support. I'm somewhat suspicious of library authors who write complex libraries that are difficult to use then charge for consultancy.

* The [SciChart home page](https://www.scichart.com/) advertises the project contains 3,490,000 lines of code. I come from an environment where lines of code are considered a metric of technical debt, and well-designed software minimizes the amount of code that must be maintained. Perhaps this is a reasonable number for their project, I just find it interesting they advertise it.

## Resources
* [SciChart WPF SDK Documentation](https://www.scichart.com/documentation/win/current/SciChart_WPF_SDK_User_Manual.html)
* [SciChart WPF Tutorials](https://www.scichart.com/documentation/v5.x/Tutorial%2001%20-%20Referencing%20SciChart%20DLLs.html)
* [SciChart Community FAQs (Forums)](www.scichart.com/questions)
* [WPF Chart Examples with Source-Code](www.scichart.com/wpf-chart-examples)