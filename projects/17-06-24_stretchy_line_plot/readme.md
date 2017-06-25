# Stretchy Line Plot
In this demo some random points are generated and scrolled (similar to numpy's [roll](https://docs.scipy.org/doc/numpy-1.10.0/reference/generated/numpy.roll.html) method). 
* Although the result looks simple, there is some strong thought behind how this example is coded. 
  * All the graphing code is encapsulated by the `ScottPlot` class of [swhPlot.cs](pixelDrawDrag2/swhPlot.cs). 
  * The code of the GUI itself [Form1.cs](pixelDrawDrag2/Form1.cs) is virtually empty.
  * My thinking is that from here I'll work on the graphing class, keeping gui usage as simple as possible. 
* _Note: plotting 321 data points I'm getting about 300Hz drawing rate with anti-aliasing off and 100Hz with it on_

![](demo.gif)
