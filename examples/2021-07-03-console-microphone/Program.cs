using System;

namespace console_microphone
{
    class Program
    {
        static void Main(string[] args)
        {
            var waveIn = new NAudio.Wave.WaveInEvent
            {
                DeviceNumber = 0, // customize this to select your microphone device
                WaveFormat = new NAudio.Wave.WaveFormat(rate: 44100, bits: 16, channels: 2),
                BufferMilliseconds = 50
            };
            waveIn.DataAvailable += ShowPeakStereo;
            waveIn.StartRecording();
            while (true) { }
        }

        private static string GetBars(double fraction, int barCount = 35)
        {
            int barsOn = (int)(barCount * fraction);
            int barsOff = barCount - barsOn;
            return new string('#', barsOn) + new string('-', barsOff);
        }

        private static void ShowPeakMono(object sender, NAudio.Wave.WaveInEventArgs args)
        {
            float maxValue = 32767;
            int peakValue = 0;
            int bytesPerSample = 2;
            for (int index = 0; index < args.BytesRecorded; index += bytesPerSample)
            {
                int value = BitConverter.ToInt16(args.Buffer, index);
                peakValue = Math.Max(peakValue, value);
            }

            Console.WriteLine("L=" + GetBars(peakValue / maxValue));
        }

        private static void ShowPeakStereo(object sender, NAudio.Wave.WaveInEventArgs args)
        {
            float maxValue = 32767;
            int peakL = 0;
            int peakR = 0;
            int bytesPerSample = 4;
            for (int index = 0; index < args.BytesRecorded; index += bytesPerSample)
            {
                int valueL = BitConverter.ToInt16(args.Buffer, index);
                peakL = Math.Max(peakL, valueL);
                int valueR = BitConverter.ToInt16(args.Buffer, index + 2);
                peakR = Math.Max(peakR, valueR);
            }

            Console.Write("L=" + GetBars(peakL / maxValue));
            Console.Write(" ");
            Console.Write("R=" + GetBars(peakR / maxValue));
            Console.Write("\n");
        }
    }
}
