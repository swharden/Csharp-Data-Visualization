## SciChart

[SciChart](https://www.scichart.com) is a commercial charting library for .NET. Although they have mobile (iOS and Android) libraries, here we will take a closer look at their WPF charts library. It may be possible to use this control in Windows Forms applications with [ElementHost](https://docs.microsoft.com/en-us/previous-versions/dotnet/netframework-3.5/ms754008(v=vs.90)) or [more modern](https://docs.microsoft.com/en-us/dotnet/framework/wpf/advanced/walkthrough-hosting-a-wpf-composite-control-in-windows-forms) techniques, but it's clearly intended for use in WPF applications.

<div align="center">

![](graphics/scichart-demo.gif)

</div>

SciChart strikes a nice balance of performance and aesthetic. They use DirectX (Windows), OpenGL (Android), or Metal (iOS) hardware acceleration, while providing extensively themeable controls with many customizable behaviors. Their demo application features many plot types, and the intuitive controls, well-balanced visuals, and subtle animations are the best I've seen in this class of software.

### Default Controls
* double click: fit axes to data
* left-click-drag: zoom to region (if zoom mode)
* left-click-drag: zoom to region (if pan mode)
* scroll-wheel: zoom to cursor

### Price
Old screenshots suggest:
* $599 for binaries and 3 months of support
* $899 for binaries and 12 months of support
* $2,200 for full source code and mobile controls

But the [SciChart store](https://store.scichart.com/) now lists prices as a yearly fee, with different prices per year number, so I'm confused to the point where I don't know what the cost actually is or if the current software is a "software as a service" model or not.

### Demo Application

![](graphics/scichart-demos.jpg)

The SciChart demo application has tons of example charts, all with source code. Let's take a closer look at the line chart shown in the screenshot at the top of the page.

### Sample Code (Line Chart)

Here's the code associated with the line chart shown in the screenshot at the top of the page.

Most of the styling seems to be achieved by customizing the XAML.

```xml
<!--  The SciChartInteractionToolbar adds zoom, pan, zoom extents and rotate functionality  -->
<!--  to the chart and is included for example purposes.  -->
<!--  If you wish to know how to zoom and pan a chart then do a search for Zoom Pan in the Examples suite!  -->
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

The C# uses MVC-style programming to crate a series (model) and load it into a renderer (view). Modifying the series data will trigger updates to the view.

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

### Performance Demo

The SciChart demo has a performance example which simulates live incoming data (1,000 points every 20 ms). Comfortable interactive frame rates are achieved even when the data set has millions of points.

![](graphics/scichart-performance.gif) 

Interestingly they disallow mouse interaction while data is being updated and I'm not sure why. This is a strikingly impressive demo nonetheless!

The excellent performance of SciCharts is what makes it a strong contender in the charting space. Their demo application did an excellent thing by pairing interactive examples with their source code.

<!--
**I'm not a fan of the setup:** I was immediately disappointed I couldn't assess SciChart's capabilities by simply running a demo on my machine. Don't get me wrong - they do have a demo application - but it only comes bundled with the whole SciChart distribution. Worse, you can't even download it without creating an account (which I anticipate will put junk mail in my inbox). Worse still, you can't simply run a demo application after downloading the huge 200MB zip, you have to _install_ the SciChart application on your machine first. The installer requires you give it administrative privileges to run, and when it launches it obnoxiously occupies the full screen with a self-promoting advertisement. After installation, the demo program pops up, and I noticed "allow usage stats feedback to our surface" was enabled by default, hidden behind a settings screen. I could deselect it, but who knows what data was already sent before I could reach that menu. I'm not actually worried about my privacy here - I'll bet that is intended to control data transfer related to user feedback buttons in the app and it is probably described in the huge legal agreement I "read" when I installed the demo - but pulling a quick move like that didn't instill me with confidence that this group has my best interests in mind.

### Why to Favor SciChart Over Open-Source Options

I found SciChart's [Comparison of SciChart vs. Open Source Chart Controls](https://www.scichart.com/comparison-of-scichart-vs-open-source-chart-controls/) very interesting! As a developer of an open source chart control myself, I read their compelling article and summarized it like this:

**1. OSS projects have lots of issues.** This point is stated literally, calling out [OxyPlot](https://github.com/oxyplot/oxyplot/issues), [LiveCharts](https://github.com/Live-Charts/Live-Charts/issues), [MPAndroidChart](https://github.com/PhilJay/MPAndroidChart/issues), and [Microsoft Charts](https://github.com/danielgindi/Charts/issues) for having many open issues on GitHub. SciChart states it has thousands of _resolved_ issues, but can't provide a link to their issues page because their GitHub repository is not public.

**2. Use a simple library.** A hard to learn or hard to adapt "free" library will cost you by absorbing unnecessarily-large amounts of developer time, and possibly even delay shipment of your software product. There's something to be said for a choosing a library with a simple API, examples, documentation, tutorials, etc., because they save you time (and therefore money). There's an excellent case made here that a difficult-to-use library will set you back in time and money, so be sure to factor-in ease of adoption and integration when choosing which library is the best tool for the job.

**3. Many open-source projects become abandoned.** This is very true, and SciChart drives the point home by showing the commit history graphs of popular OSS charting libraries. One could argue that a stable libraries will taper-down their commits as they mature, but the four they list (OxyPlot, MPAndroidChart, LiveCharts, and Microsoft Charts) are indeed dead or dying projects.

**4. SciChart has lots of features.** They present a table where they compare SciChart's features to those of the popular open-source libraries. They make the point that many of the open-source options have limited capabilities, documentation, and support.

**5. Flexibility.** This point seems to be a bit nuanced but they point to some niche features of SciChart (they call "deep features") and argue that they can make it easier if your needs become more complex in the future. 

I can't help but comment on this one. SciChart argues their _extensibility_ is useful, as it can be adapted to accomplish obscure tasks. This is true, however one could also argue open-source software is _flexible_ in that its code can be modified as needed to accomplish obscure tasks. In fairness this is only true if the source code is well-written, easy to understand, and well-maintained, and open-source projects are the wild west of coding styles so you never know what you're going to get.

**6. Performance.** SciChart uses hardware acceleration (DirectX, OpenGL, and Metal) to produce some of the fastest charts in the world.

-->

### SciChart Resources
* [SciChart WPF SDK Documentation](https://www.scichart.com/documentation/win/current/SciChart_WPF_SDK_User_Manual.html)
* [SciChart WPF Tutorials](https://www.scichart.com/documentation/v5.x/Tutorial%2001%20-%20Referencing%20SciChart%20DLLs.html)
* [SciChart Community FAQs (Forums)](www.scichart.com/questions)
* [WPF Chart Examples with Source-Code](www.scichart.com/wpf-chart-examples)