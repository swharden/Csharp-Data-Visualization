# Realtime Spectrograph

* **YouTube Demonstration: https://youtu.be/MS1Qgo710Vo**
* This examples uses [naudio](https://github.com/naudio/NAudio) library to access the sound card
* Unlike previous examples in this repository, the FFT is calculated using nAudio (not Accord)

![](spectrograph.gif)

## Example: FFT with NAudio
```c#
using NAudio.Wave; // for sound card access
using NAudio.Dsp; // for FastFourierTransform
```

```c#
// prepare the complex data which will be FFT'd
Complex[] fft_buffer = new Complex[fft_size];
for (int i=0; i < fft_size; i++)
{
    fft_buffer[i].X = (float)(unanalyzed_values[i] * FastFourierTransform.HammingWindow(i, fft_size));
    fft_buffer[i].Y = 0;
}

// perform the FFT
FastFourierTransform.FFT(true, (int)Math.Log(fft_size, 2.0), fft_buffer);

// a list with FFT values
List<double> new_data = new List<double>();
for (int i = 0; i < spec_data[spec_data.Count - 1].Count; i++)
{
    // should this be sqrt(X^2+Y^2)?
    double val;
    val = (double)fft_buffer[i].X + (double)fft_buffer[i].Y;
    val = Math.Abs(val);
    if (checkBox1.Checked) val = Math.Log(val);
    new_data.Add(val);
}
```
