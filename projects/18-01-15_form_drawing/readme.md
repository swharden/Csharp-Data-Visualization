# DataView Control

The goal of this project is to create a user control to easily navigate evenly-spaced data. This is ideal for visualizing temperature over time, audio data, electrophysiological recordings, etc. I will attempt to re-create ClampFit's data interactive interface in facetious detail.

## Inspection of ClampFit Interface

![](clampfit2.png)

This is what a data window looks like inside ClampFit. Although there is a toolbar on the top, all of the data is displayed in a sunken panel. 

<img src="clampfit-needs-graphing.png" width="400"><img src="clampfit-graph.png" width="400">

The image inside the data area of this sunken panel is extremely similar to what [ScottPlot](https://github.com/swharden/ScottPlot) can do, so with a little tweaking we should be able to create a problem that mimics this one. 

<img src="plan.PNG">

This is how I intend to break-up my form. I'll keep as much as possible as windows form events (labels, scrollbars, etc) and minimize the size and complexity of the bitmap I'll be updating with the axis and data changes. I suspect I can do this with two primary table layout controls. This would almost work perfectly as a single table layout (grid), but the label on the bottom right spans two columns so if I intend to be exact to the ClampFit screenshot, I'll stick with nested layouts.
