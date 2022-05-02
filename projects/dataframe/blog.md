---
title: Using DataFrames in C#
description: How to use the DataFrame class from the Microsoft.Data.Analysis package to interact with tabular data
date: 2022-05-01 23:00:00
tags: csharp
---

# Using DataFrames in C# 

**The DataFrame is a data structure designed to facilitate manipulation and analysis of tabular data, and it is the cornerstone data type in data science using modern programming languages.** One of the most famous implementations of the DataFrame is provided by the Pandas package for the Python programming language. An equivalent data structure is available for C# using Microsoft's data analysis package. Although data frames are commonly used in notebooks, then can be used in standard .NET applications as well. This article surveys Microsoft's Data Analysis package and introduces how to with with data frames using C# and the .NET platform.

### TLDR
* A DataFrame is a 2D matrix that stores data values in named columns.
* Each column may have a distinct data type.
* Rows typically represent observations.

## DataFrame Quickstart

Add the [`Microsoft.Data.Analysis` package](https://www.nuget.org/packages/Microsoft.Data.Analysis/) to your project, then you can create a DataFrame like this:

```cs
using Microsoft.Data.Analysis;

string[] names = { "Oliver", "Charlotte", "Henry", "Amelia", "Owen" };
int[] ages = { 23, 19, 42, 64, 35 };
double[] heights = { 1.91, 1.62, 1.72, 1.57, 1.85 };

DataFrameColumn[] columns = {
    new StringDataFrameColumn("Name", names),
    new PrimitiveDataFrameColumn<int>("Age", ages),
    new PrimitiveDataFrameColumn<double>("Height", heights),
};

DataFrame df = new(columns);
```

Contents of a DataFrame can be previewed using `Console.WriteLine(df)` but the formatting isn't pretty.

```text
Name  Age   Height
Oliver23    1.91
Charlotte19    1.62
Henry 42    1.72
Amelia64    1.57
Owen  35    1.85
```

## Pretty DataFrame Formatting

**A custom `PrettyPrint()` extension method can improve DataFrame readability.** Implementing this as an extension method allows me to call `df.PrettyPrint()` anywhere in my code.

```cs
Name       Age  Height
Oliver     23   1.91
Charlotte  19   1.62
Henry      42   1.72
Amelia     64   1.57
Owen       35   1.85
```

I can create similar methods to format a DataFrame as Markdown or HTML.

```text
Name      | Age | Height
----------|-----|--------
Oliver    | 23  | 1.91
Charlotte | 19  | 1.62
Henry     | 42  | 1.72
Amelia    | 64  | 1.57
Owen      | 35  | 1.85
```


Name      | Age | Height
----------|-----|--------
Oliver    | 23  | 1.91
Charlotte | 19  | 1.62
Henry     | 42  | 1.72
Amelia    | 64  | 1.57
Owen      | 35  | 1.85

## HTML Formatting for Interactive Notebooks

⚠️⚠️⚠️⚠️ TODO

## Append a Row

Build a new row using key/value pair then append it to the DataFrame

```cs
List<KeyValuePair<string, object>> newRowData = new()
{
    new KeyValuePair<string, object>("Name", "Scott"),
    new KeyValuePair<string, object>("Age", 36),
    new KeyValuePair<string, object>("Height", 1.65),
};

df.Append(newRowData, inPlace: true);
```

## Add a Column

Build a new column, populate it with data, and add it to the DataFrame

```cs
int[] weights = { 123, 321, 111, 121, 131 };
PrimitiveDataFrameColumn<int> weightCol = new("Weight", weights);
df.Columns.Add(weightCol);
```

## Sort and Filter

**The DataFrame class has numerous operations available** to sort, filter, and analyze data in many different ways. A popular pattern when working with DataFrames is to use _method chaining_ to combine numerous operations together into a single statement. See the [DataFrame Class API](https://docs.microsoft.com/en-us/dotnet/api/microsoft.data.analysis.dataframe) for a full list of available operations.

```cs
df.OrderBy("Name")
    .Filter(df["Age"].ElementwiseGreaterThan(30))
    .PrettyPrint();
```

```text
Name    Age  Height
Henry   42   1.72
Oliver  23   1.91
Owen    35   1.85
```

## Mathematical Operations

It's easy to perform math on columns or across multiple DataFrames. In this example we will perform math using two columns and create a new column to hold the output.

```cs
DataFrameColumn iqCol = df["Age"] * df["Height"] * 1.5;

double[] iqs = Enumerable.Range(0, (int)iqCol.Length)
    .Select(x => (double)iqCol[x])
    .ToArray();

df.Columns.Add(new PrimitiveDataFrameColumn<double>("IQ", iqs));
df.PrettyPrint();
```

```text
Name       Age  Height  IQ
Oliver     23   1.91    65.9
Charlotte  19   1.62    46.17
Henry      42   1.72    108.36
Amelia     64   1.57    150.72
Owen       35   1.85    97.12
```

## Statistical Operations

You can iterate across every row of a column to calculate population statistics

```cs
foreach (DataFrameColumn col in df.Columns.Skip(1))
{
    // warning: additional care must be taken for datasets which contain null
    double[] values = Enumerable.Range(0, (int)col.Length).Select(x => Convert.ToDouble(col[x])).ToArray();
    (double mean, double std) = MeanAndStd(values);
    Console.WriteLine($"{col.Name} = {mean} +/- {std:N3} (n={values.Length})");
}
```

```text
Age = 36.6 +/- 15.982 (n=5)
Height = 1.734 +/- 0.130 (n=5)
```

## Plot Values from a DataFrame

⚠️⚠️⚠️⚠️ TODO

## Conclusions

Although I typically reach for Python to perform exploratory data science, it's good to know that C# has a DataFrame available and that it can be used to inspect and manipulate tabular data. I look forward to watching Microsoft's Data Analysis namespace continue to evolve as part of their machine learning / ML.NET platform.

### Why not just use LINQ?

I see this question asked frequently, often with an aggressive and condescending tone. LINQ (Language-Integrated Query) is fantastic for performing logical operations on simple collections of data. When you have large 2D datasets of _labeled_ data, advantages of DataFrames over flat LINQ statements start to become apparent. It is also easy to perform logical operations across multiple DataFrames, allowing users to write simpler and more readable code than could be achieved with LINQ statements. DataSets also make it much easier to visualize complex data too. In the data science world where complex labeled datasets are routinely compared, manipulated, merged, and visualized, often in an interactive context, the DataFrames are much easier to work with than raw LINQ statements.

### Data may contain `null`

I didn't demonstrate it in the code examples above, but note that all column data types are nullable. While null-containing data requires extra considerations when writing mathematical routes, it's a convenient way to model missing data which is a common occurrence in the real world. 

### The C# DataFrame is Still Evolving

It was recently moved to the Machine Learning / ML.NET team, and reviewing the code while editing this article I found a few surprising instances where a `NotImplementedException` is thrown. See [DataFrameColumn.cs](https://github.com/dotnet/machinelearning/blob/main/src/Microsoft.Data.Analysis/DataFrameColumn.cs) for examples.

## Resources
* [Official `Microsoft.Data.Analysis.DataFrame` Class Documentation](https://docs.microsoft.com/en-us/dotnet/api/microsoft.data.analysis.dataframe)
* [An Introduction to DataFrame](https://devblogs.microsoft.com/dotnet/an-introduction-to-dataframe/) (.NET Blog) - official introduction, notebook-dependent, not particularly easy to follow in my opinion
* [ExtremeOptimization DataFrame Quickstart](https://www.extremeoptimization.com/QuickStart/CSharp/DataFrames.aspx)
* [`Microsoft.Data.Analysis` on NuGet](https://www.nuget.org/packages/Microsoft.Data.Analysis/)
* [10 minutes to pandas](https://pandas.pydata.org/docs/user_guide/10min.html) - summary of things you can do with DataFrames using the Pandas library for Python