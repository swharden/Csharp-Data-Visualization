// generate sample data using a random walk
Random rand = new(1268);
int pountCount = 20;
double[] xs1 = new double[pountCount];
double[] ys1 = new double[pountCount];
for (int i = 1; i < pountCount; i++)
{
    xs1[i] = xs1[i - 1] + rand.NextDouble() - .5;
    ys1[i] = ys1[i - 1] + rand.NextDouble() - .5;
}

// Use cubic interpolation to smooth the original data
(double[] xs2, double[] ys2) = Interpolation.Cubic.InterpolateXY(xs1, ys1, 200);

// Plot the original vs. interpolated data
var plt = new ScottPlot.Plot(600, 400);
plt.AddScatter(xs1, ys1, label: "original", markerSize: 7);
plt.AddScatter(xs2, ys2, label: "interpolated", markerSize: 3);
plt.Legend();
string filePath = Path.GetFullPath($"interpolation.png");
plt.SaveFig(filePath);
Console.WriteLine(filePath);