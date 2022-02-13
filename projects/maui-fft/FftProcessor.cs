namespace MauiFft;

public class FftProcessor : IDisposable
{
    readonly List<double> Values = new();
    readonly NAudio.Wave.WaveInEvent WaveIn;

    public FftProcessor(int device = 0)
    {
        WaveIn = new()
        {
            DeviceNumber = device,
            WaveFormat = new NAudio.Wave.WaveFormat(rate: 12_000, bits: 16, channels: 1),
            BufferMilliseconds = 10,
        };
        WaveIn.DataAvailable += WaveIn_DataAvailable; ;
        WaveIn.StartRecording();
    }

    public void Dispose()
    {
        WaveIn.DataAvailable -= WaveIn_DataAvailable;
        WaveIn.StopRecording();
        WaveIn.Dispose();
    }

    private void WaveIn_DataAvailable(object? sender, NAudio.Wave.WaveInEventArgs e)
    {
        for (int index = 0; index < e.BytesRecorded; index += 2)
        {
            double value = BitConverter.ToInt16(e.Buffer, index);
            Values.Add(value);
        }
    }

    public double[]? GetFft(int pow = 10, double stepFrac = 0.1)
    {
        int sampleCount = 1 << pow;
        if (Values.Count < sampleCount)
            return null;

        double[] values = new double[sampleCount];
        Values.CopyTo(Values.Count - sampleCount, values, 0, sampleCount);

        int pointsToKeep = (int)((1 - stepFrac) * sampleCount);
        Values.RemoveRange(0, Values.Count - pointsToKeep);

        double[] fft = FftSharp.Transform.FFTmagnitude(values);
        return fft;
    }
}
