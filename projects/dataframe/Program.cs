using Microsoft.Data.Analysis;

public static class Program
{
    public static void Main()
    {
        DataFrame df = SampleDataFrame();

    }

    private static void ColStats()
    {
        DataFrame df = SampleDataFrame();

        foreach (DataFrameColumn col in df.Columns.Skip(1))
        {
            // warning: additional care must be taken for datasets which contain null
            double[] values = Enumerable.Range(0, (int)col.Length).Select(x => Convert.ToDouble(col[x])).ToArray();
            (double mean, double std) = MeanAndStd(values);
            Console.WriteLine($"{col.Name} = {mean} +/- {std:N3} (n={values.Length})");
        }
    }

    private static (double mean, double std) MeanAndStd(double[] values)
    {
        if (values is null)
            throw new ArgumentNullException(nameof(values));

        if (values.Length == 0)
            throw new ArgumentException($"{nameof(values)} must not be empty");

        double sum = 0;
        for (int i = 0; i < values.Length; i++)
            sum += values[i];

        double mean = sum / values.Length;

        double sumVariancesSquared = 0;
        for (int i = 0; i < values.Length; i++)
        {
            double pointVariance = Math.Abs(mean - values[i]);
            double pointVarianceSquared = Math.Pow(pointVariance, 2);
            sumVariancesSquared += pointVarianceSquared;
        }

        double meanVarianceSquared = sumVariancesSquared / values.Length;
        double std = Math.Sqrt(meanVarianceSquared);

        return (mean, std);
    }

    public static DataFrame SampleDataFrame()
    {
        string[] names = { "Oliver", "Charlotte", "Henry", "Amelia", "Owen" };
        int[] ages = { 23, 19, 42, 64, 35 };
        double[] heights = { 1.91, 1.62, 1.72, 1.57, 1.85 };

        DataFrameColumn[] columns = {
            new StringDataFrameColumn("Name", names),
            new PrimitiveDataFrameColumn<int>("Age", ages),
            new PrimitiveDataFrameColumn<double>("Height", heights),
        };

        DataFrame df = new(columns);

        return df;
    }

    public static void Quickstart()
    {
        DataFrame df = SampleDataFrame();

        Console.WriteLine(df);

        df.PrettyPrint();
        Console.WriteLine(df.ToMarkdown());
    }

    public static void Everything()
    {

        // Generate sample data
        string[] x = { "Scott", "Ray", "Steven", "Dennis", "Greg", "Alan" };

        Random rand = new(0);
        double[] y1 = Enumerable.Range(0, x.Length).Select(x => rand.Next(100) / 100.0).ToArray();
        int[] y2 = Enumerable.Range(0, x.Length).Select(x => rand.Next(100)).ToArray();
        double[] y3 = Enumerable.Range(0, x.Length).Select(x => rand.Next(100) / 100.0).ToArray();

        // Create columns using initial data
        StringDataFrameColumn colX = new("Name", x);
        PrimitiveDataFrameColumn<double> colY1 = new("A", y1);
        PrimitiveDataFrameColumn<int> colY2 = new("B", y2);
        PrimitiveDataFrameColumn<double> colY3 = new("C", y3);

        // Build a DataFrame from a collection of columns
        DataFrameColumn[] columns = { colX, colY1, colY2, colY3 };
        DataFrame df = new(columns);
        Console.WriteLine(df.PrettyText());

        // modify cell values
        df[3, 2] = 12345;
        df[4, 3] = 12.3;
        Console.WriteLine(df.PrettyText());

        // modify values in place
        df["B"].Subtract(100, inPlace: true);
        Console.WriteLine(df.PrettyText());

        // modify values in place using assignment
        df["B"] = df["B"] + 200;
        Console.WriteLine(df.PrettyText());

        // values are nullable
        df[3, 2] = null;
        Console.WriteLine(df.PrettyText());

        // rows can be accessed
        DataFrameRow row = df.Rows[2];
        row.Pretty();

        // rows can be added using key/value pairs
        List<KeyValuePair<string, object>> rowValues = new()
        {
            new KeyValuePair<string, object>("Name", "Clark"),
            new KeyValuePair<string, object>("B", 123),
            new KeyValuePair<string, object>("C", 32.1)
        };
        df.Append(rowValues, true);
        Console.WriteLine(df.PrettyText());

        // sort
        DataFrame dfSorted = df.OrderBy("Name");
        Console.WriteLine(dfSorted.PrettyText());

        // access column
        //DataFrameColumn col = df["C"];
        //Console.WriteLine(col.Pretty());
    }

    public static void PlotDF()
    {
        // create a DataFrame containing random data
        Random rand = new(0);
        DoubleDataFrameColumn colTemp = new("Temperature", ScottPlot.DataGen.Random(rand, 100, 100));
        DoubleDataFrameColumn colHumid = new("Humidity", ScottPlot.DataGen.Random(rand, 100));
        DataFrameColumn[] columns = { colTemp, colHumid };
        DataFrame df = new(columns);
        Console.WriteLine(df);

        // extract data into double arrays
        double[] xs = Enumerable.Range(0, (int)df.Rows.Count).Select(row => Convert.ToDouble(df["Temperature"][row])).ToArray();
        double[] ys = Enumerable.Range(0, (int)df.Rows.Count).Select(row => Convert.ToDouble(df["Humidity"][row])).ToArray();

        // plot data
        ScottPlot.Plot plt = new(400, 300);
        plt.AddScatterPoints(xs, ys);
        plt.XAxis.Label("Temperature");
        plt.YAxis.Label("Humidity");
        plt.SaveFig("DataFrame.png");
    }
}