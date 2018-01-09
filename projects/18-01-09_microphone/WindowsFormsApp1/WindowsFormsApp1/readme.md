# Audio Level Monitor

This is a sample project I made to help myself remember how to easily provide highspeed 
access to the microphone device.

![](screenshot.gif)

### Key Code

***Import NAudio:***
```c#
using NAudio.Wave; // installed with nuget
``` 

***Continuously listen to the microphone:***

```c#
int RATE=44100; // sample rate of the microphone
int BUFFER_SAMPLES=1024; // powers of two help for FFT

var waveIn = new WaveInEvent();
waveIn.DeviceNumber = 0; // change this to select different sound inputs
waveIn.WaveFormat = new NAudio.Wave.WaveFormat(RATE, 1); // 1 for mono
waveIn.DataAvailable += OnDataAvailable; // this function must exist
waveIn.BufferMilliseconds = (int)((double)BUFFER_SAMPLES / (double)RATE * 1000.0);
waveIn.StartRecording();
```

***Do this when a new buffer gets filled:***
```c#
private void OnDataAvailable(object sender, WaveInEventArgs args)
{
	// this example will display the peak audio value of the buffer
    float max = 0;

    // interpret as 16 bit audio
    for (int index = 0; index < args.BytesRecorded; index += 2)
    {
        short sample = (short)((args.Buffer[index + 1] << 8) |
                                args.Buffer[index + 0]);
        var sample32 = sample / 32768f; // to floating point
        if (sample32 < 0) sample32 = -sample32; // absolute value 
        if (sample32 > max) max = sample32; // is this the max value?
    }
	System.Console.WriteLine(max+"\n"); // display the peak audio value
}
```

### Useful Links
* [naudio GitHub page](https://github.com/naudio/NAudio)
* [naudio Recording Level Meter (docs)](https://github.com/naudio/NAudio/blob/master/Docs/RecordingLevelMeter.md)
* [naudio Recording a WAV (docs)](https://github.com/naudio/NAudio/blob/master/Docs/RecordWavFileWinFormsWaveIn.md)