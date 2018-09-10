# Realtime Microphone FFT with ScottPlot

This project demonstrates how to plot microphone data (and its FFT) with a modern version of [ScottPlot](https://github.com/swharden/ScottPlot). It is written with readability and reproducibility in mind.

### Download as EXE

* **[Release.zip](Release.zip)**


### Screenshot
![](screenshot.png)

### Re-creating this Project from Scratch
These are the steps I did to make this project
* I cloned [ScottPlot](https://github.com/swharden/ScottPlot) into [ScottPlot-2018-09-09/](ScottPlot-2018-09-09) so it will always work with this project even if the latest ScottPlot API changes.
* Add Project (ScottPlot.csproj) to the solution
* ScottPlotUC showed up in the toolbox, so I could drag/drop onto a form
* Add reference to the `System.Numerics` assembly
* Used NuGet to install `NAudio`
* Used NuGet to install `Accord.Audio`
* Fill out the code on Form1.cs
* Run