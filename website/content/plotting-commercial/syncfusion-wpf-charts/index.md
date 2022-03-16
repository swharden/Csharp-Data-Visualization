---
title: Plot Data with Syncfusion WPF Charts
description: todo
date: 3333-01-01
---

The SfChart control was made by [Syncfusion](https://www.syncfusion.com/) and is available for many platforms including Windows Forms, WPF, UWP, Xamarin, and even JavaScript (Angular, React, and Vue). Their [demos page](https://www.syncfusion.com/demos) is quite impressive, and it's particularly entertaining that the same types of plots in the WPF controls are [viewable in your browser](https://ej2.syncfusion.com/vue/demos/#/material/chart/histogram.html) using JavaScript.

<div align="center">

![](graphics/wpf-charts-trend.png)

</div>

One thing I really like about SfChart is the [easy to browse collection of documentation](https://help.syncfusion.com/wpf/charts/series#scatter) showing how to generate all types of plots. They also have controls for more specialized charting, like the[SfSmithChart](https://help.syncfusion.com/wpf/smith-chart/overview) for making [Smith Charts](https://en.wikipedia.org/wiki/Smith_chart).


The [Migrating from Chart to SfChart in WPF Charts (SfChart)](https://help.syncfusion.com/wpf/charts/migrating-from-chart-to-sfchart) guide makes it easy to learn how to use SfChart for those who already have a strong grasp of Microsoft's Chart module.

### Price
It looks like the ability to use these controls depends on paying a yearly fee. The first year is roughly twice as expensive as subsequent years.

* All controls: $2,495 per developer for 1st year
* WinForms or WPF controls only: $995 per developer for 1st year

## WPF Demo

I wasn't able to download the WinForms demo (it wanted me to [download and compile it myself](https://github.com/syncfusion/winforms-demos)), but I did find a WPF demo [here](https://wpf.syncfusion.com/samples/18.1.0.42/ui/sfchart/sfchart.htm). It has most of the same chart types shown in the web browser demo.

From the demos it doesn't seem these graphs are mouse-interactive. I found one mouse-interactive chart called "stock zooming" which felt very smooth and performant. It's disappointing that most of the graphs aren't able to be zoomed or panned.

### Line Plot

Let's take a closer look at their "fast charts" line plot demo. 

![](graphics/wpf-charts-line.jpg)

Most of the styling and behavior of charts is controlled with XAML:

```xml
<chart:SfChart 
    x:Name="FastLine" 
    Height="450" 
    Width="750" 
    AreaBorderBrush="#8e8e8e" 
    HorizontalAlignment="Center">

    <chart:SfChart.Header>
        <TextBlock 
        FontSize="24" 
        Foreground="Black" 
        FontFamily="Segoe UI" 
        Margin="0,20,0,10">Weather Report</TextBlock>
    </chart:SfChart.Header>
    
    <chart:SfChart.Behaviors>
        <chart:ChartZoomPanBehavior/>
    </chart:SfChart.Behaviors>

    <chart:SfChart.PrimaryAxis>
        <chart:CategoryAxis 
            x:Name="axis1" 
            Header="Months"  
            LabelFormat="MMM/dd"  
            LabelTemplate="{StaticResource labelTemplate}"  
            HeaderTemplate="{StaticResource headerTemplate}">
        </chart:CategoryAxis>
    </chart:SfChart.PrimaryAxis>

    <chart:SfChart.SecondaryAxis>
        <chart:NumericalAxis 
            RangePadding="Round"  
            LabelFormat="0.00" 
            Header="Temperature" 
            HeaderTemplate="{StaticResource headerTemplate}"   
            Interval="5"
            LabelTemplate="{StaticResource labelTemplate}">
        </chart:NumericalAxis>
    </chart:SfChart.SecondaryAxis>

    <chart:FastLineSeries 
        x:Name="StackingAreaSeries" 
        ShowTooltip="True" 
        chart:ChartTooltip.EnableAnimation="True" 
        XBindingPath="Date" 
        YBindingPath="Value" 
        Label="Expenditures">
    </chart:FastLineSeries>

</chart:SfChart>
```

The C# under the hood is pretty simple. MVC-style.

```cs
DataGenerator _viewModel = new DataGenerator();
ObservableCollection<Data> collection = _viewModel.GenerateData();
FastLine.Series[0].ItemsSource = collection; 
```

## Resources

* [Getting Started with WPF Charts (SfChart)](https://help.syncfusion.com/wpf/charts/getting-started) _includes video_