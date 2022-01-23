using NUnit.Framework;
using System;
using System.Linq;
using System.Text;

namespace InterpolationTests
{
    public class Tests
    {
        private (double[] xs, double[] ys) RandomWalks(int count)
        {
            Random rand = new(0);
            double[] xs = Enumerable.Range(0, count).Select(x => (rand.NextDouble() - .5) * 100).ToArray();
            double[] ys = Enumerable.Range(0, count).Select(x => (rand.NextDouble() - .5) * 100).ToArray();
            return (xs, ys);
        }

        [TestCase(5)]
        [TestCase(7)]
        [TestCase(10)]
        [TestCase(15)]
        [TestCase(20)]
        [TestCase(21)]
        [TestCase(22)]
        [TestCase(23)]
        [TestCase(24)]
        [TestCase(100)]
        public void Test_InputLengths(int count)
        {
            (double[] xs, double[] ys) = RandomWalks(count);
            (double[] interpolatedXs, double[] interpolatedYs) = Interpolation.Cubic.InterpolateXY(xs, ys, 5);
            Assert.IsNotNull(interpolatedXs);
            Assert.IsNotNull(interpolatedYs);
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        [TestCase(10)]
        [TestCase(15)]
        [TestCase(23)]
        public void Test_OutputMultiples(int count)
        {
            (double[] xs, double[] ys) = RandomWalks(23);
            (double[] interpolatedXs, double[] interpolatedYs) = Interpolation.Cubic.InterpolateXY(xs, ys, count);
            Assert.IsNotNull(interpolatedXs);
            Assert.IsNotNull(interpolatedYs);
        }

        private string GetCodeToInstantiateArray(double[] values, string name, int columns = 10)
        {
            StringBuilder sb = new();
            sb.AppendLine($"{name} = new double[]");
            sb.AppendLine("{");
            for (int i = 0; i < values.Length; i++)
            {
                if (i % columns == 0 && i > 0)
                    sb.Append("\n");
                sb.Append($"{values[i]:0.00000}, ");
            }
            sb.AppendLine("\n};");
            return sb.ToString();
        }

        [Test]
        public void Test_KnownValues_Match()
        {
            (double[] xs, double[] ys) = RandomWalks(20);
            (double[] interpolatedXs, double[] interpolatedYs) = Interpolation.Cubic.InterpolateXY(xs, ys, 100);
            Assert.IsNotNull(interpolatedXs);
            Assert.IsNotNull(interpolatedYs);

            //Console.WriteLine(GetCodeToInstantiateArray(interpolatedXs, "xs"));
            //Console.WriteLine(GetCodeToInstantiateArray(interpolatedYs, "ys"));

            double[] expectedXs = new double[]
            {
                22.62433, 32.01963, 29.34372, 23.30072, 19.23011, 16.78732, 15.49192, 14.86344, 14.42144, 13.68547,
                12.17506, 9.40976, 4.91027, -1.43440, -8.84023, -16.38988, -23.16601, -28.25128, -30.72835, -29.67988,
                -24.45079, -15.74369, -4.69918, 7.54182, 19.77343, 30.61438, 38.65405, 42.48183, 40.68712, 32.59876,
                20.65301, 8.10222, -1.80121, -5.80493, -1.54204, 9.02192, 22.59849, 35.89887, 45.63428, 48.71060,
                44.83526, 35.81678, 23.51460, 9.78817, -3.50308, -14.49969, -21.34224, -22.31815, -19.09174, -14.30724,
                -7.94967, 0.10595, 8.51484, 14.00332, 14.61801, 11.57674, 6.51581, 1.07156, -3.11968, -4.42160,
                -1.23021, 6.61241, 17.26842, 28.75415, 39.08590, 46.28000, 48.39494, 44.86637, 36.78043, 25.32066,
                11.67061, -2.98618, -17.46616, -30.58580, -41.16154, -48.01244, -50.47028, -48.99257, -44.18636, -36.65871,
                -27.01667, -15.86729, -3.81761, 8.52530, 20.55440, 31.66264, 41.22499, 48.28050, 51.57036, 49.82545,
                42.35236, 31.21298, 19.29241, 9.13206, 1.16570, -4.97109, -9.64423, -13.21962, -16.06318, -18.54082,
            };

            double[] expectedYs = new double[]
            {
                31.69079, 35.33964, 45.56326, 52.24932, 50.57549, 42.24135, 29.10244, 13.01429, -4.16756, -20.58758,
                -34.39022, -43.71995, -46.72449, -42.60421, -33.11723, -20.40269, -6.59971, 6.15257, 15.71502, 19.94851,
                17.50325, 11.11576, 4.84084, 2.73131, 7.20255, 16.24676, 27.11669, 37.06509, 43.34470, 43.94288,
                39.93393, 33.20290, 25.63486, 19.11486, 15.11165, 13.05716, 11.76132, 10.03394, 6.68479, 0.61451,
                -7.96926, -17.87865, -27.90202, -36.82773, -43.44415, -46.53964, -44.90256, -37.42575, -25.47926, -13.24518,
                -5.36134, -6.33380, -14.66068, -20.94353, -18.83526, -9.83046, 3.54137, 18.75072, 33.26810, 44.56401,
                50.14852, 49.30128, 43.74413, 35.37742, 26.10146, 17.81661, 12.38678, 10.48722, 11.36853, 14.19724,
                18.13990, 22.36304, 26.03319, 28.31690, 28.38069, 25.39362, 19.01954, 10.00874, -0.74418, -12.34460,
                -23.89794, -34.50959, -43.28495, -49.32940, -51.74836, -49.64722, -42.25562, -31.12562, -19.86846, -12.16669,
                -10.66684, -13.05735, -15.54520, -14.80352, -10.36424, -2.84193, 7.14680, 18.98536, 32.05714, 45.74552,
            };

            Assert.AreEqual(expectedXs.Length, interpolatedXs.Length);
            Assert.AreEqual(expectedYs.Length, interpolatedYs.Length);

            for (int i = 0; i < expectedXs.Length; i++)
            {
                Assert.AreEqual(expectedXs[i], interpolatedXs[i], delta: 1e-5);
                Assert.AreEqual(expectedYs[i], interpolatedYs[i], delta: 1e-5);
            }
        }
    }
}