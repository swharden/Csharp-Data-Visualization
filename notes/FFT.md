# Calculating the FFT in C#
Here is a minimal-case example how to convert an array of doubles into the frequency domain using a [Fast Fourier transformation](https://en.wikipedia.org/wiki/Fast_Fourier_transform) in C# (Visual Studio Community 2017). It uses the [Accord .NET library](http://accord-framework.net). Add a reference to the Assembly Framework `System.Numerics`. Use NuGet to install `Accord.Audio`.
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
