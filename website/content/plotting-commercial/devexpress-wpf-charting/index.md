---
title: Plot Data with DevExpress WPF Charting
description: todo
date: 3333-01-01
---

The [WPF Charting](https://www.devexpress.com/Products/NET/Controls/WPF/Charting/) package by DevExpress provides user controls for creating interactive and animated charts in .NET applications.

<div align="center">

![](graphics/devexpress-line.png)

</div>

DevExpress makes many high-quality controls for [Windows Forms](https://www.devexpress.com/products/net/controls/winforms/) and [WPF](https://www.devexpress.com/products/net/controls/wpf/). Charting controls are just one part of their large collection of UI libraries DevExpress offers. When their charting controls are put under a magnifying glass and examined in isolation, they don't fare well against similar controls designed by teams who only produce charting software.

### Price
See the [DevExpress charting page](https://www.devexpress.com/Products/NET/Controls/WPF/Charting/#Pricing) for full details

* $900 for WPF controls
* $1,000 for WinForms controls
* $2,200 for full source code and all controls (including mobile)

### Examples and Documentation

The [WPF Controls Documentation](https://docs.devexpress.com/WPF/114223/controls-and-libraries/charts-suite/chart-control/fundamentals/series-fundamentals/2d-series-types) is an extensive collection of images and ~~code~~ notes describing how to create every graph type. 

From what I gather this is another MVC-style view system. You create some type of series object (model), pass it into a diagram type (view), then assign/modify the model's data. 

Unfortunately I'm struggling to find concise start-to-finish code examples that show complimentary XAML, CS, and an output figure.

The GitHub projects that demonstrate primary plot types are _almost_ useful, but since they have only source code and not an image of their output they're not particularly helpful unless they're downloaded, compiled, and executed.

### Animations

<div align="center">

![](graphics/devexpress-bar.gif)

</div>

Flipping through the demos on the [WPF Charting website](https://www.devexpress.com/Products/NET/Controls/WPF/Charting/) I'm struck by how much _movement_ these charts contain. I thought I'd know what a bar graph would look like, but when I click it to see the demo I see animated bars growing into position. When I click a scatter plot I see lines growing and markers popping into view. 

<div align="center">

![](graphics/devexpress-scatter.gif)

</div>

I respect the effort that goes into coding animated graphs like that, but I can't picture what type of application would actually benefit from _animated_ scatter plots appearing in view. It's hard to emphasize how off-putting this is. A graph that takes 5 seconds to animate to achieve its final position means I have to sit there and wait for the animation to stop before I can even assess the data represented by the graph. Such an effect may fit in as a score history chart in a video game, or maybe some metric in a weather app, but is noting I'd want to see in a serious scientific application.

### Demo Application

I was initially attracted to this project because the [WPF Charting](https://www.devexpress.com/Products/NET/Controls/WPF/Charting/) page looked so interesting. I tried for a very long time to run a meaningful demo to check out this charting library, but the only demos I could find weren't very impressive the charting department.

Most demos on the [DevExpress demo page](https://www.devexpress.com/support/demos/) require activating a trial subscription. I now see that demo links direct to `dxdemo://` URLs which apparently requires the DevExpress trial program to be installed on your system and configured in your browser. Absolutely Ridiculous.

Curiosity overcame my annoyance and I threw in the towel and downloaded and installed the trial so I could view the demos. Props to DevExpress for letting me opt-out of phone-home notifications to their server during the installation process.

![](graphics/devexpress-trial.png)

Let see what these demos have in store...

![](graphics/devexpress-office.gif)

oh my

![](graphics/devexpress-stock.gif)

hmm...

Okay I think I'm done here.

### UWP Demo

DevExpress has a demo program in the Microsoft Store. Maybe some of the charting controls will be prominently displayed in it...

![](graphics/devexpress-controls-store.jpg)

This app is crazy! I'm so confused right now. It scrolls horizontally instead of vertically, and I almost didn't notice! The vertical content goes off the page too and there's no way to access it. Maybe I need to buy a larger monitor.

![](graphics/devexpress-controls-app.jpg)

These are the only graphs in the app. I'm guessing this app isn't intended to showcase the charting controls after all.

## Conclusion

Due to the lack of a meaningful demo I wasn't really able to assess the charting controls. The lack of such a demo is... perhaps an indication the charting controls are under-developed, or at least not the primary focus of the development team.