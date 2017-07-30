# Realtime Graphing of Microphone Audio with FFT Display using C# (Visual Studio 2017)

![](demo.gif)

**Project Demo on YouTube:** https://youtu.be/q9cRZuosrOs
* See notes related to [calculating FFT with C#](/notes/FFT.md)
* **WHY THIS PROJECT EXISTS:** This is intended to be a minimal case example how to capture audio data from the microphone and display it in real time. It is not a polished project. It is essentially a note-to-self project uploaded to provide code reference for the author in the future. I'm sharing it online so others may benefit from reviewing its code.
* **ScottPlot** is a user control I am currently developing. It will be its own GitHub project when it is completed, but it's not ready yet. I'm adding a _zip snapshot_ of the current development project frozen in time. If you are going to try to run this project as-is, you'll need to add ScottPlot as a reference.


# Code Notes

#### Set the audio device number (important if multiple inputs exist)
```c#
WaveIn wi = new WaveIn();
wi.DeviceNumber = 0;
wi.WaveFormat = new NAudio.Wave.WaveFormat(44100, 1);
```

#### Integrate a byte array into a 16-bit values
```c#
int frameSize = 1024; // how much buffer to read at once
var frames = new byte[frameSize]; // init the byte array
bwp.Read(frames, 0, frameSize); // read the buffer
int SAMPLE_RESOLUTION = 16; // this is for 16-bit integers in a byte array
int BYTES_PER_POINT = SAMPLE_RESOLUTION / 8;
Int32[] vals = new Int32[frames.Length/BYTES_PER_POINT];
for (int i=0; i<vals.Length; i++)
{
    byte hByte = frames[i * 2 + 1];
    byte lByte = frames[i * 2 + 0];
    vals[i] = (int)(short)((hByte << 8) | lByte);
}
```

#### Load data into ScottPlot user control
```c#
scottPlotUC1.Xs = Xs;
scottPlotUC1.Ys = Ys;
scottPlotUC1.UpdateGraph();
```
