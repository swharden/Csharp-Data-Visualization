## Create Graph Image from Console Application
This 6-line program will generate 5,000 random data points and save the output graph as a figure.

```
private static void Main(string[] args)
{
	ScottPlot2.ScottPlot SP = new ScottPlot2.ScottPlot();
	ScottPlot2.Generate SPgen = new ScottPlot2.Generate();

	SP.SetSize(1500, 400);
	SP.AddLine(SPgen.Sequence(5000), SPgen.Sine(5000));

	SP.Render();
	SP.SaveFig("test.jpg");
}
```

![](test.jpg)

## Interactive GUI
The axis labels are getting really smart. This makes live panning and zooming a breeze!
