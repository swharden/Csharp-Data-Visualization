using Microsoft.Data.Analysis;
using System.Text;

internal static class PrettyPrinters
{
    public static void PrettyPrint(this DataFrame df) => Console.WriteLine(PrettyText(df));
    public static string PrettyText(this DataFrame df) => ToStringArray2D(df).ToFormattedText();

    public static string ToMarkdown(this DataFrame df) => ToStringArray2D(df).ToMarkdown();

    public static void PrettyPrint(this DataFrameRow row) => Console.WriteLine(Pretty(row));
    public static string Pretty(this DataFrameRow row) => row.Select(x => x?.ToString() ?? string.Empty).StringJoin();
    private static string StringJoin(this IEnumerable<string> strings) => string.Join(" ", strings.Select(x => x.ToString()));

    private static string[,] ToStringArray2D(DataFrame df)
    {
        string[,] strings = new string[df.Rows.Count + 1, df.Columns.Count];

        for (int i = 0; i < df.Columns.Count; i++)
            strings[0, i] = df.Columns[i].Name;

        for (int i = 0; i < df.Rows.Count; i++)
            for (int j = 0; j < df.Columns.Count; j++)
                strings[i + 1, j] = df[i, j]?.ToString() ?? string.Empty;

        return strings;
    }

    private static int[] GetMaxLengthsByColumn(this string[,] strings)
    {
        int[] maxLengthsByColumn = new int[strings.GetLength(1)];

        for (int y = 0; y < strings.GetLength(0); y++)
            for (int x = 0; x < strings.GetLength(1); x++)
                maxLengthsByColumn[x] = Math.Max(maxLengthsByColumn[x], strings[y, x].Length);

        return maxLengthsByColumn;
    }

    private static string ToFormattedText(this string[,] strings)
    {
        StringBuilder sb = new();
        int[] maxLengthsByColumn = GetMaxLengthsByColumn(strings);

        for (int y = 0; y < strings.GetLength(0); y++)
        {
            for (int x = 0; x < strings.GetLength(1); x++)
            {
                sb.Append(strings[y, x].PadRight(maxLengthsByColumn[x] + 2));
            }
            sb.AppendLine();
        }

        return sb.ToString();
    }


    private static string ToMarkdown(this string[,] strings)
    {
        StringBuilder sb = new();
        int[] maxLengthsByColumn = GetMaxLengthsByColumn(strings);

        for (int y = 0; y < strings.GetLength(0); y++)
        {
            for (int x = 0; x < strings.GetLength(1); x++)
            {
                sb.Append(strings[y, x].PadRight(maxLengthsByColumn[x]));
                if (x < strings.GetLength(1) - 1)
                    sb.Append(" | ");
            }
            sb.AppendLine();

            if (y == 0)
            {
                for (int i = 0; i < strings.GetLength(1); i++)
                {
                    int bars = maxLengthsByColumn[i] + 2;
                    if (i == 0)
                        bars -= 1;
                    sb.Append(new String('-', bars));

                    if (i < strings.GetLength(1) - 1)
                        sb.Append("|");
                }
                sb.AppendLine();
            }
        }

        return sb.ToString();
    }
}
