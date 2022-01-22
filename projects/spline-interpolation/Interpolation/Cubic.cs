namespace Interpolation
{
    public static class Cubic
    {
        public static (double[] xs, double[] ys) InterpolateXY(double[] xsIn, double[] ysIn, int multiple)
        {
            int inputPointCount = xsIn.Length;
            int outputPointCount = xsIn.Length * multiple;

            double[] distIn = new double[inputPointCount];
            for (int i = 1; i < inputPointCount; i++)
            {
                double dx = xsIn[i] - xsIn[i - 1];
                double dy = ysIn[i] - ysIn[i - 1];
                double distance = Math.Sqrt(dx * dx + dy * dy);
                distIn[i] = distIn[i - 1] + (float)distance; // TODO: REMOVE THIS CAST
            }

            // TOOD: replace with LINQ 
            //double[] times = Enumerable.Range(0, outputPointCount).Select(x => x * meanDistanceBetweenPoints).ToArray();
            double meanDistanceBetweenPoints = distIn.Last() / (outputPointCount - 1);
            double[] distOut = new double[outputPointCount];
            for (int i = 1; i < outputPointCount; i++)
                distOut[i] = distOut[i - 1] + meanDistanceBetweenPoints;

            double[] xsOut = InterpolateCubicSpline(distIn, xsIn, distOut);
            double[] ysOut = InterpolateCubicSpline(distIn, ysIn, distOut);
            return (xsOut, ysOut);
        }

        public static double[] InterpolateCubicSpline(double[] actualDistances, double[] actualValues, double[] interpolationDistances)
        {
            (double[] a, double[] b) = FitMatrix(actualDistances, actualValues);

            double[] interpolatedValues = new double[interpolationDistances.Length];
            for (int i = 0; i < interpolatedValues.Length; i++)
            {
                int nextIndex = Enumerable.Range(0, actualDistances.Length - 2)
                    .FirstOrDefault(index => interpolationDistances[i] <= actualDistances[index + 1], actualDistances.Length - 2);

                double dx = actualDistances[nextIndex + 1] - actualDistances[nextIndex];
                double t = (interpolationDistances[i] - actualDistances[nextIndex]) / dx;
                double y = (1 - t) * actualValues[nextIndex] + t * actualValues[nextIndex + 1] + t * (1 - t) * (a[nextIndex] * (1 - t) + b[nextIndex] * t);
                interpolatedValues[i] = y;
            }

            return interpolatedValues;
        }

        public static (double[] a, double[] b) FitMatrix(double[] x, double[] y)
        {
            int n = x.Length;
            double[] a = new double[n - 1];
            double[] b = new double[n - 1];
            double[] r = new double[n];
            double[] A = new double[n];
            double[] B = new double[n];
            double[] C = new double[n];

            double dx1, dx2, dy1, dy2;

            dx1 = x[1] - x[0];
            C[0] = 1.0f / dx1;
            B[0] = 2.0f * C[0];
            r[0] = 3 * (y[1] - y[0]) / (dx1 * dx1);

            for (int i = 1; i < n - 1; i++)
            {
                dx1 = x[i] - x[i - 1];
                dx2 = x[i + 1] - x[i];
                A[i] = 1.0f / dx1;
                C[i] = 1.0f / dx2;
                B[i] = 2.0f * (A[i] + C[i]);
                dy1 = y[i] - y[i - 1];
                dy2 = y[i + 1] - y[i];
                r[i] = 3 * (dy1 / (dx1 * dx1) + dy2 / (dx2 * dx2));
            }

            dx1 = x[n - 1] - x[n - 2];
            dy1 = y[n - 1] - y[n - 2];
            A[n - 1] = 1.0f / dx1;
            B[n - 1] = 2.0f * A[n - 1];
            r[n - 1] = 3 * (dy1 / (dx1 * dx1));

            double[] k = SolveTriDiagonalMatrix(r, A, B, C);

            for (int i = 1; i < n; i++)
            {
                dx1 = x[i] - x[i - 1];
                dy1 = y[i] - y[i - 1];
                a[i - 1] = k[i - 1] * dx1 - dy1;
                b[i - 1] = -k[i] * dx1 + dy1;
            }

            return (a, b);
        }

        private static double[] SolveTriDiagonalMatrix(double[] d, double[] A, double[] B, double[] C)
        {
            int n = d.Length;

            double[] cPrime = new double[n];
            cPrime[0] = C[0] / B[0];
            for (int i = 1; i < n; i++)
                cPrime[i] = C[i] / (B[i] - cPrime[i - 1] * A[i]);

            double[] dPrime = new double[n];
            dPrime[0] = d[0] / B[0];
            for (int i = 1; i < n; i++)
                dPrime[i] = (d[i] - dPrime[i - 1] * A[i]) / (B[i] - cPrime[i - 1] * A[i]);

            double[] x = new double[n];
            x[n - 1] = dPrime[n - 1];
            for (int i = n - 2; i >= 0; i--)
                x[i] = dPrime[i] - cPrime[i] * x[i + 1];

            return x;
        }
    }
}