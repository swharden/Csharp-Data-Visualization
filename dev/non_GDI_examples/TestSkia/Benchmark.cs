using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSkia
{
    public class Benchmark : IDisposable
    {
        System.Diagnostics.Stopwatch stopwatch;
        bool silent;

        public Benchmark(bool silent = false)
        {
            stopwatch = System.Diagnostics.Stopwatch.StartNew();
            this.silent = silent;
        }

        public double elapsedMilliseconds
        {
            get
            {
                return stopwatch.ElapsedTicks * 1000.0 / System.Diagnostics.Stopwatch.Frequency;
            }
        }

        public double hertz
        {
            get
            {
                return 1000.0 / elapsedMilliseconds;
            }
        }

        public string GetMessage(string description = "completed")
        {
            stopwatch.Stop();
            return string.Format(description + " in {0:0.00} ms {1:0.00 Hz}", elapsedMilliseconds, hertz);
        }

        public void Dispose()
        {
            stopwatch.Stop();
            if (!silent)
                Console.WriteLine(GetMessage());
        }
    }
}
