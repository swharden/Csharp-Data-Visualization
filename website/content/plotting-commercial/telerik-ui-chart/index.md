---
title: Plot Data with Telerik UI Chart
description: todo
date: 3333-01-01
---

Telerik has UI controls for [WPF](https://www.telerik.com/products/wpf/chart.aspx) and [WinForms](https://www.telerik.com/products/winforms.aspx), one of which is a chart module. Although it looks like this control was built for easy theming and supports little animations for simple data, it doesn't seem to have been built with performance as a primary design goal. I did find it interesting they support multiple rendering systems including Bitmap and Direct2D which.

![](graphics/telerik-ui-wpf-demo.jpg)

### Price

## Demo

The Telerik UI for WPF has a demo you have to download and install. I chose to take a look at the WPF control because the screenshots of the Windows Forms control didn't look very chart-rich.

![](graphics/telerik-live-data.gif)

The `ChartView` control has several example uses. In all cases the plots are "pretty", but look like they're designed more for simple appearance and theming support rather than performance. I don't think this charting library is suitable for high-performance scientific charting, though I did find the demo to be very insightful and a good way to inspect all the controls provided with this package.

## Controls

* left-click-drag: zoom region (or pan)
* scroll-wheel: zoom

## Resources
* https://www.telerik.com/products/wpf/chart.aspx