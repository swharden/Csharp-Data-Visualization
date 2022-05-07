public static class Program
{
    public static void Main()
    {
        ListDevices();
        DemoSingleChannel(-1);
        //DemoTwoChannel(-1);
    }

    public static void ListDevices()
    {
        for (int i = -1; i < NAudio.Wave.WaveIn.DeviceCount; i++)
        {
            var caps = NAudio.Wave.WaveIn.GetCapabilities(i);
            Console.WriteLine($"{i}: {caps.ProductName}");
        }
    }

    public static void DemoSingleChannel(int deviceID)
    {
        var waveIn = new NAudio.Wave.WaveInEvent
        {
            DeviceNumber = deviceID,
            WaveFormat = new NAudio.Wave.WaveFormat(rate: 44100, bits: 16, channels: 1),
            BufferMilliseconds = 20
        };
        waveIn.DataAvailable += WaveIn_DataAvailable;
        waveIn.StartRecording();

        Console.WriteLine("C# Audio Level Meter");
        Console.WriteLine("(press any key to exit)");
        Console.ReadKey();

        static void WaveIn_DataAvailable(object? sender, NAudio.Wave.WaveInEventArgs e)
        {
            // copy buffer into an array of integers
            Int16[] values = new Int16[e.Buffer.Length / 2];
            Buffer.BlockCopy(e.Buffer, 0, values, 0, e.Buffer.Length);

            // determine the highest value as a fraction of the maximum possible value
            float fraction = (float)values.Max() / (1 << 15);

            // print a level meter using the console
            string bar = new('#', (int)(fraction * 70));
            string meter = "[" + bar.PadRight(60, '-') + "]";
            Console.CursorLeft = 0;
            Console.CursorVisible = false;
            Console.Write($"{meter} {fraction * 100:00.0}%");
        }
    }

    public static void DemoTwoChannel(int deviceID)
    {
        var waveIn = new NAudio.Wave.WaveInEvent
        {
            DeviceNumber = deviceID, // indicates which microphone to use
            WaveFormat = new NAudio.Wave.WaveFormat(rate: 44100, bits: 16, channels: 2),
            BufferMilliseconds = 20
        };
        waveIn.DataAvailable += WaveIn_DataAvailable;
        waveIn.StartRecording();

        Console.WriteLine("C# Audio Level Meter");
        Console.WriteLine("(press any key to exit)");
        Console.ReadKey();

        static void WaveIn_DataAvailable(object? sender, NAudio.Wave.WaveInEventArgs e)
        {
            int bytesPerSample = 2;
            int channelCount = 2;
            int sampleCount = e.Buffer.Length / bytesPerSample / channelCount;

            Int16[] valuesL = new Int16[sampleCount];
            Int16[] valuesR = new Int16[sampleCount];

            for (int i = 0; i < sampleCount; i++)
            {
                int position = i * bytesPerSample * channelCount;
                valuesL[i] = BitConverter.ToInt16(e.Buffer, position);
                valuesR[i] = BitConverter.ToInt16(e.Buffer, position + 2);
            }

            Console.CursorLeft = 0;
            Console.CursorVisible = false;
            Console.Write($"L: {valuesL.Max() / 327.68:N}%   R:{valuesR.Max() / 327.680:N}%");
        }
    }
}