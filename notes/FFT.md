# Calculating the FFT in C#

## With NAudio (light)
[example project: spectrograph](/projects/18-01-11_microphone_spectrograph)

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

## With Accord (heavy)
Here is a minimal-case example how to convert an array of doubles into the frequency domain using a [Fast Fourier transformation](https://en.wikipedia.org/wiki/Fast_Fourier_transform) in C# (Visual Studio Community 2017). It uses the [Accord .NET library](http://accord-framework.net). Add a reference to the Assembly Framework `System.Numerics`. Use NuGet to install `Accord.Audio`. [example project: microphone FFT](/projects/17-07-16_microphone)
```C#
using System.Numerics;

public double[] FFT(double[] data)
{
    int nPoints = data.Length; // whatever we measure must be a power of 2
    for (int i = 0; i < data.Length; i++) data[i] = Math.Sin(i); // fill it with some data
    double[] fft = new double[nPoints]; // this is where we will store the output (fft)
    Complex[] fftComplex = new Complex[nPoints]; // the FFT function requires complex format
    for (int i = 0; i < data.Length; i++)
        fftComplex[i] = new Complex(data[i], 0.0); // make it complex format
    Accord.Math.FourierTransform.FFT(fftComplex, Accord.Math.FourierTransform.Direction.Forward);
    for (int i = 0; i < data.Length; i++)
        fft[i] = fftComplex[i].Magnitude; // back to double
    return fft;
    //todo: this could be much faster by reusing variables
}
```
