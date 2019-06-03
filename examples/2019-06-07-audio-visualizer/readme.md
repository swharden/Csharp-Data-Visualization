# Realtime Audio FFT Graph
This program builds on the previous example ([graphing audio levels](/examples/2019-06-06-audio-level-monitor/readme.md)) but uses [ScottPlot](https://github.com/swharden/ScottPlot)'s PlotSignal function to continuously display 10 seconds of raw PCM audio values. Notice that even though the audio data is continuously updating, the graph remains fully interactive...

![](screenshot.gif)

## Core Concepts

### FFT