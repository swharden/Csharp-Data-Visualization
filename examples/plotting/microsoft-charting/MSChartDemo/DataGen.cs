using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSChartDemo
{
    public static class DataGen
    {
        public static double[] Random(int count, Random rand)
        {
            double[] data = new double[count];
            for (int i = 0; i < count; i++)
                data[i] = rand.NextDouble();
            return data;
        }
    }
}
