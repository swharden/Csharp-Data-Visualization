using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Linq;

namespace AwokeKnowing.GnuplotCSharp
{
    class GnuPlot
    {
        public static string PathToGnuplot = @"C:\Program Files (x86)\gnuplot\bin";
        private static Process ExtPro;
        private static StreamWriter GnupStWr;
        private static List<StoredPlot> PlotBuffer;
        private static List<StoredPlot> SPlotBuffer;
        private static bool ReplotWithSplot;

        public static bool Hold { get; private set; }

        static GnuPlot()
        {
            if (PathToGnuplot[PathToGnuplot.Length - 1].ToString() != @"\")
                PathToGnuplot += @"\";
            ExtPro = new Process();
            ExtPro.StartInfo.FileName = PathToGnuplot + "gnuplot.exe";
            ExtPro.StartInfo.UseShellExecute = false;
            ExtPro.StartInfo.RedirectStandardInput = true;
            ExtPro.Start();
            GnupStWr = ExtPro.StandardInput;
            PlotBuffer = new List<StoredPlot>();
            SPlotBuffer = new List<StoredPlot>();
            Hold = false;
        }

        public static void WriteLine(string gnuplotcommands)
        {

            GnupStWr.WriteLine(gnuplotcommands);
            GnupStWr.Flush();
        }

        public static void Write(string gnuplotcommands)
        {
            GnupStWr.Write(gnuplotcommands);
            GnupStWr.Flush();
        }

        public static void Set(params string[] options)
        {
            for (int i = 0; i < options.Length; i++)
                GnupStWr.WriteLine("set " + options[i]);

        }

        public static void Unset(params string[] options)
        {
            for (int i = 0; i < options.Length; i++)
                GnupStWr.WriteLine("unset " + options[i]);
        }

        public static bool SaveData(double[] Y, string filename)
        {
            StreamWriter dataStream = new StreamWriter(filename, false);
            WriteData(Y, dataStream);
            dataStream.Close();

            return true;
        }

        public static bool SaveData(double[] X, double[] Y, string filename)
        {
            StreamWriter dataStream = new StreamWriter(filename, false);
            WriteData(X, Y, dataStream);
            dataStream.Close();

            return true;
        }

        public static bool SaveData(double[] X, double[] Y, double[] Z, string filename)
        {
            StreamWriter dataStream = new StreamWriter(filename, false);
            WriteData(X, Y, Z, dataStream);
            dataStream.Close();

            return true;
        }

        public static bool SaveData(int sizeY, double[] Z, string filename)
        {
            StreamWriter dataStream = new StreamWriter(filename, false);
            WriteData(sizeY, Z, dataStream);
            dataStream.Close();

            return true;
        }

        public static bool SaveData(double[,] Z, string filename)
        {
            StreamWriter dataStream = new StreamWriter(filename, false);
            WriteData(Z, dataStream);
            dataStream.Close();

            return true;
        }

        public static void Replot()
        {
            if (ReplotWithSplot)
                SPlot(SPlotBuffer);
            else
                Plot(PlotBuffer);
        }

        public static void Plot(string filenameOrFunction, string options = "")
        {
            if (!Hold) PlotBuffer.Clear();
            PlotBuffer.Add(new StoredPlot(filenameOrFunction, options));
            Plot(PlotBuffer);
        }
        public static void Plot(double[] y, string options = "")
        {
            if (!Hold) PlotBuffer.Clear();
            PlotBuffer.Add(new StoredPlot(y, options));
            Plot(PlotBuffer);
        }
        public static void Plot(double[] x, double[] y, string options = "")
        {
            if (!Hold) PlotBuffer.Clear();
            PlotBuffer.Add(new StoredPlot(x, y, options));
            Plot(PlotBuffer);
        }

        public static void Contour(string filenameOrFunction, string options = "", bool labelContours = true)
        {
            if (!Hold) PlotBuffer.Clear();
            var p = new StoredPlot(filenameOrFunction, options, PlotTypes.ContourFileOrFunction);
            p.LabelContours = labelContours;
            PlotBuffer.Add(p);
            Plot(PlotBuffer);
        }
        public static void Contour(int sizeY, double[] z, string options = "", bool labelContours = true)
        {
            if (!Hold) PlotBuffer.Clear();
            var p = new StoredPlot(sizeY, z, options, PlotTypes.ContourZ);
            p.LabelContours = labelContours;
            PlotBuffer.Add(p);
            Plot(PlotBuffer);
        }
        public static void Contour(double[] x, double[] y, double[] z, string options = "", bool labelContours = true)
        {
            if (!Hold) PlotBuffer.Clear();
            var p = new StoredPlot(x, y, z, options, PlotTypes.ContourXYZ);
            p.LabelContours = labelContours;
            PlotBuffer.Add(p);
            Plot(PlotBuffer);
        }
        public static void Contour(double[,] zz, string options = "", bool labelContours = true)
        {
            if (!Hold) PlotBuffer.Clear();
            var p = new StoredPlot(zz, options, PlotTypes.ContourZZ);
            p.LabelContours = labelContours;
            PlotBuffer.Add(p);
            Plot(PlotBuffer);
        }

        public static void HeatMap(string filenameOrFunction, string options = "")
        {
            if (!Hold) PlotBuffer.Clear();
            PlotBuffer.Add(new StoredPlot(filenameOrFunction, options, PlotTypes.ColorMapFileOrFunction));
            Plot(PlotBuffer);
        }
        public static void HeatMap(int sizeY, double[] intensity, string options = "")
        {
            if (!Hold) PlotBuffer.Clear();
            PlotBuffer.Add(new StoredPlot(sizeY, intensity, options, PlotTypes.ColorMapZ));
            Plot(PlotBuffer);
        }
        public static void HeatMap(double[] x, double[] y, double[] intensity, string options = "")
        {
            if (!Hold) PlotBuffer.Clear();
            PlotBuffer.Add(new StoredPlot(x, y, intensity, options, PlotTypes.ColorMapXYZ));
            Plot(PlotBuffer);
        }
        public static void HeatMap(double[,] intensityGrid, string options = "")
        {
            if (!Hold) PlotBuffer.Clear();
            PlotBuffer.Add(new StoredPlot(intensityGrid, options, PlotTypes.ColorMapZZ));
            Plot(PlotBuffer);
        }

        public static void SPlot(string filenameOrFunction, string options = "")
        {
            if (!Hold) SPlotBuffer.Clear();
            SPlotBuffer.Add(new StoredPlot(filenameOrFunction, options,PlotTypes.SplotFileOrFunction));
            SPlot(SPlotBuffer);
        }
        public static void SPlot(int sizeY, double[] z, string options = "")
        {
            if (!Hold) SPlotBuffer.Clear();
            SPlotBuffer.Add(new StoredPlot(sizeY, z, options));
            SPlot(SPlotBuffer);
        }

        public static void SPlot(double[] x, double[] y, double[] z, string options = "")
        {
            if (!Hold) SPlotBuffer.Clear();
            SPlotBuffer.Add(new StoredPlot(x, y, z, options));
            SPlot(SPlotBuffer);
        }

        public static void SPlot(double[,] zz, string options = "")
        {
            if (!Hold) SPlotBuffer.Clear();
            SPlotBuffer.Add(new StoredPlot(zz, options));
            SPlot(SPlotBuffer);
        }


        public static void Plot(List<StoredPlot> storedPlots)
        {
            ReplotWithSplot = false;
            string plot = "plot ";
            string plotstring = "";
            string contfile;
            string defcntopts;
            removeContourLabels();
            for (int i = 0; i < storedPlots.Count; i++)
            {
                var p = storedPlots[i];
                defcntopts = (p.Options.Length > 0 && (p.Options.Contains(" w") || p.Options[0] == 'w')) ? " " : " with lines ";
                switch (p.PlotType)
                {
                    case PlotTypes.PlotFileOrFunction:
                        if (p.File != null)
                            plotstring += (plot + plotPath(p.File) + " " + p.Options);
                        else
                            plotstring += (plot + p.Function + " " + p.Options);
                        break;
                    case PlotTypes.PlotXY:
                    case PlotTypes.PlotY:
                        plotstring += (plot + @"""-"" " + p.Options);
                        break;
                    case PlotTypes.ContourFileOrFunction:
                        contfile = Path.GetTempPath() + "_cntrtempdata" + i + ".dat";
                        makeContourFile((p.File != null ? plotPath(p.File) : p.Function), contfile);
                        if (p.LabelContours) setContourLabels(contfile);
                        plotstring += (plot + plotPath(contfile) + defcntopts + p.Options);
                        break;
                    case PlotTypes.ContourXYZ:
                        contfile = Path.GetTempPath() + "_cntrtempdata" + i + ".dat";
                        makeContourFile(p.X, p.Y, p.Z, contfile);
                        if (p.LabelContours) setContourLabels(contfile);
                        plotstring += (plot + plotPath(contfile) + defcntopts + p.Options);
                        break;
                    case PlotTypes.ContourZZ:
                        contfile = Path.GetTempPath() + "_cntrtempdata" + i + ".dat";
                        makeContourFile(p.ZZ, contfile);
                        if (p.LabelContours) setContourLabels(contfile);
                        plotstring += (plot + plotPath(contfile) + defcntopts + p.Options);
                        break;
                    case PlotTypes.ContourZ:
                        contfile = Path.GetTempPath() + "_cntrtempdata" + i + ".dat";
                        makeContourFile(p.YSize, p.Z, contfile);
                        if (p.LabelContours) setContourLabels(contfile);
                        plotstring += (plot + plotPath(contfile) + defcntopts + p.Options);
                        break;


                    case PlotTypes.ColorMapFileOrFunction:
                        if (p.File != null)
                            plotstring += (plot + plotPath(p.File) + " with image " + p.Options);
                        else
                            plotstring += (plot + p.Function + " with image " + p.Options);
                        break;
                    case PlotTypes.ColorMapXYZ:
                    case PlotTypes.ColorMapZ:
                        plotstring += (plot + @"""-"" " + " with image " + p.Options);
                        break;
                    case PlotTypes.ColorMapZZ:
                        plotstring += (plot + @"""-"" " + "matrix with image " + p.Options);
                        break;
                }
                if (i == 0) plot = ", ";
            }
            GnupStWr.WriteLine(plotstring);

            for (int i = 0; i < storedPlots.Count; i++)
            {
                var p = storedPlots[i];
                switch (p.PlotType)
                {
                    case PlotTypes.PlotXY:
                        WriteData(p.X, p.Y, GnupStWr, false);
                        GnupStWr.WriteLine("e");
                        break;
                    case PlotTypes.PlotY:
                        WriteData(p.Y, GnupStWr, false);
                        GnupStWr.WriteLine("e");
                        break;
                    case PlotTypes.ColorMapXYZ:
                        WriteData(p.X, p.Y, p.Z, GnupStWr, false);
                        GnupStWr.WriteLine("e");
                        break;
                    case PlotTypes.ColorMapZ:
                        WriteData(p.YSize, p.Z, GnupStWr, false);
                        GnupStWr.WriteLine("e");
                        break;
                    case PlotTypes.ColorMapZZ:
                        WriteData(p.ZZ, GnupStWr, false);
                        GnupStWr.WriteLine("e");
                        GnupStWr.WriteLine("e");
                        break;
                    
                }
            }
            GnupStWr.Flush();
        }

        public static void SPlot(List<StoredPlot> storedPlots)
        {
            ReplotWithSplot = true;
            var splot = "splot ";
            string plotstring = "";
            string defopts = "";
            removeContourLabels();
            for (int i = 0; i < storedPlots.Count; i++)
            {
                var p = storedPlots[i];
                defopts = (p.Options.Length > 0 && (p.Options.Contains(" w") || p.Options[0] == 'w')) ? " " : " with lines ";
                switch (p.PlotType)
                {
                    case PlotTypes.SplotFileOrFunction:
                        if (p.File != null)
                            plotstring += (splot + plotPath(p.File) + defopts + p.Options);
                        else
                            plotstring += (splot + p.Function + defopts + p.Options);
                        break;
                    case PlotTypes.SplotXYZ:
                    case PlotTypes.SplotZ:
                        plotstring += (splot + @"""-"" " + defopts + p.Options);
                        break;
                    case PlotTypes.SplotZZ:
                        plotstring += (splot + @"""-"" matrix " + defopts + p.Options);
                        break;
                }
                if (i == 0) splot = ", ";
            }
            GnupStWr.WriteLine(plotstring);

            for (int i = 0; i < storedPlots.Count; i++)
            {
                var p = storedPlots[i];
                switch (p.PlotType)
                {
                    case PlotTypes.SplotXYZ:
                        WriteData(p.X, p.Y, p.Z, GnupStWr, false);
                        GnupStWr.WriteLine("e");
                        break;
                    case PlotTypes.SplotZZ:
                        WriteData(p.ZZ, GnupStWr, false);
                        GnupStWr.WriteLine("e");
                        GnupStWr.WriteLine("e");
                        break;
                    case PlotTypes.SplotZ:
                        WriteData(p.YSize, p.Z, GnupStWr, false);
                        GnupStWr.WriteLine("e");
                        break;
                }
            }
            GnupStWr.Flush();
        }

        public static void WriteData(double[] y, StreamWriter stream, bool flush = true)
        {
            for (int i = 0; i < y.Length; i++)
                stream.WriteLine(y[i].ToString());

            if (flush) stream.Flush();
        }

        public static void WriteData(double[] x, double[] y, StreamWriter stream, bool flush = true)
        {
            for (int i = 0; i < y.Length; i++)
                stream.WriteLine(x[i].ToString() + " " + y[i].ToString());

            if (flush) stream.Flush();
        }

        public static void WriteData(int ySize, double[] z, StreamWriter stream, bool flush = true)
        {
            for (int i = 0; i < z.Length; i++)
            {
                if (i > 0 && i % ySize == 0)
                    stream.WriteLine();
                stream.WriteLine(z[i].ToString());
            }

            if (flush) stream.Flush();
        }

        public static void WriteData(double[,] zz, StreamWriter stream, bool flush = true)
        {
            int m = zz.GetLength(0);
            int n = zz.GetLength(1);
            string line;
            for (int i = 0; i < m; i++)
            {
                line = "";
                for (int j = 0; j < n; j++)
                    line += zz[i, j].ToString() + " ";
                stream.WriteLine(line.TrimEnd());
            }

            if (flush) stream.Flush();
        }

        public static void WriteData(double[] x, double[] y, double[] z, StreamWriter stream, bool flush = true)
        {
            int m = Math.Min(x.Length, y.Length);
            m = Math.Min(m, z.Length);
            for (int i = 0; i < m; i++)
            {
                if (i > 0 && x[i] != x[i - 1])
                    stream.WriteLine("");
                stream.WriteLine(x[i] + " " + y[i] + " " + z[i]);
            }

            if (flush) stream.Flush();
        }

        static string plotPath(string path)
        {
            return "\"" + path.Replace(@"\", @"\\") + "\"";
        }

        public static void SaveSetState(string filename = null)
        {
            if (filename == null)
                filename = Path.GetTempPath() + "setstate.tmp";
            GnupStWr.WriteLine("save set " + plotPath(filename));
            GnupStWr.Flush();
            waitForFile(filename);
        }
        public static void LoadSetState(string filename = null)
        {
            if (filename == null)
                filename = Path.GetTempPath() + "setstate.tmp";
            GnupStWr.WriteLine("load " + plotPath(filename));
            GnupStWr.Flush();
        }

        //these makecontourFile functions should probably be merged into one function and use a StoredPlot parameter
        static void makeContourFile(string fileOrFunction, string outputFile)//if it's a file, fileOrFunction needs quotes and escaped backslashes
        {
            SaveSetState();
            Set("table " + plotPath(outputFile));
            Set("contour base");
            Unset("surface");
            GnupStWr.WriteLine(@"splot " + fileOrFunction);
            Unset("table");
            GnupStWr.Flush();
            LoadSetState();
            waitForFile(outputFile);
        }

        static void makeContourFile(double[] x, double[] y, double[] z, string outputFile)
        {
            SaveSetState();
            Set("table " + plotPath(outputFile));
            Set("contour base");
            Unset("surface");
            GnupStWr.WriteLine(@"splot ""-""");
            WriteData(x, y, z, GnupStWr);
            GnupStWr.WriteLine("e");
            Unset("table");
            GnupStWr.Flush();
            LoadSetState();
            waitForFile(outputFile);
        }

        static void makeContourFile(double[,] zz, string outputFile)
        {
            SaveSetState();
            Set("table " + plotPath(outputFile));
            Set("contour base");
            Unset("surface");
            GnupStWr.WriteLine(@"splot ""-"" matrix");
            WriteData(zz, GnupStWr);
            GnupStWr.WriteLine("e");
            GnupStWr.WriteLine("e");
            Unset("table");
            GnupStWr.Flush();
            LoadSetState();
            waitForFile(outputFile);
        }

        static void makeContourFile(int sizeY, double[] z, string outputFile)
        {
            SaveSetState();
            Set("table " + plotPath(outputFile));
            Set("contour base");
            Unset("surface");
            GnupStWr.WriteLine(@"splot ""-""");
            WriteData(sizeY, z, GnupStWr);
            GnupStWr.WriteLine("e");
            Unset("table");
            GnupStWr.Flush();
            LoadSetState();
            waitForFile(outputFile);
        }

        static int contourLabelCount = 50000;
        static void setContourLabels(string contourFile)
        {
            var file = new System.IO.StreamReader(contourFile);
            string line;
            while ((line = file.ReadLine()) != null)
            {
                if (line.Contains("label:"))
                {
                    string[] c = file.ReadLine().Trim().Replace("   ", " ").Replace("  ", " ").Split(' ');
                    GnupStWr.WriteLine("set object " + ++contourLabelCount + " rectangle center " + c[0] + "," + c[1] + " size char " + (c[2].ToString().Length + 1) + ",char 1 fs transparent solid .7 noborder fc rgb \"white\"  front");
                    GnupStWr.WriteLine("set label " + contourLabelCount + " \"" + c[2] + "\" at " + c[0] + "," + c[1] + " front center");
                }
            }
            file.Close();
        }
        static void removeContourLabels()
        {
            while (contourLabelCount > 50000)
                GnupStWr.WriteLine("unset object " + contourLabelCount + ";unset label " + contourLabelCount--);
        }

        static bool waitForFile(string filename, int timeout = 10000)
        {
            Thread.Sleep(20);
            int attempts = timeout / 100;
            System.IO.StreamReader file = null;
            while (file == null)
            {
                try { file = new System.IO.StreamReader(filename); }
                catch
                {
                    if (attempts-- > 0)
                        Thread.Sleep(100);
                    else
                        return false;
                }
            }
            file.Close();
            return true;
        }

        public static void HoldOn()
        {
            Hold = true;
            PlotBuffer.Clear();
            SPlotBuffer.Clear();
        }

        public static void HoldOff()
        {
            Hold = false;
            PlotBuffer.Clear();
            SPlotBuffer.Clear();
        }

        public static void Close()
        {
            ExtPro.CloseMainWindow();
        }

    }

    enum PointStyles
    {
        Dot = 0,
        Plus = 1,
        X = 2,
        Star = 3,
        DotSquare = 4,
        SolidSquare = 5,
        DotCircle = 6,
        SolidCircle = 7,
        DotTriangleUp = 8,
        SolidTriangleUp = 9,
        DotTriangleDown = 10,
        SolidTriangleDown = 11,
        DotDiamond = 12,
        SolidDiamond = 13
    }

    enum PlotTypes
    {
        PlotFileOrFunction,
        PlotY,
        PlotXY,
        ContourFileOrFunction,
        ContourXYZ,
        ContourZZ,
        ContourZ,
        ColorMapFileOrFunction,
        ColorMapXYZ,
        ColorMapZZ,
        ColorMapZ,
        SplotFileOrFunction,
        SplotXYZ,
        SplotZZ,
        SplotZ
    }

    class StoredPlot
    {
        public string File = null;
        public string Function = null;
        public double[] X;
        public double[] Y;
        public double[] Z;
        public double[,] ZZ;
        public int YSize;
        public string Options;
        public PlotTypes PlotType;
        public bool LabelContours;

        public StoredPlot()
        {
        }
        public StoredPlot(string functionOrfilename, string options = "", PlotTypes plotType = PlotTypes.PlotFileOrFunction)
        {
            if (IsFile(functionOrfilename))
                File = functionOrfilename;
            else
                Function = functionOrfilename;
            Options = options;
            PlotType = plotType;
        }

        public StoredPlot(double[] y, string options = "")
        {
            Y = y;
            Options = options;
            PlotType = PlotTypes.PlotY;
        }

        public StoredPlot(double[] x, double[] y, string options = "")
        {
            X = x;
            Y = y;
            Options = options;
            PlotType = PlotTypes.PlotXY;
        }

        //3D data
        public StoredPlot(int sizeY, double[] z, string options = "", PlotTypes plotType = PlotTypes.SplotZ)
        {
            YSize = sizeY;
            Z = z;
            Options = options;
            PlotType = plotType;
        }

        public StoredPlot(double[] x, double[] y, double[] z, string options = "", PlotTypes plotType = PlotTypes.SplotXYZ)
        {
            if (x.Length < 2)
                YSize = 1;
            else
                for (YSize = 1; YSize < x.Length; YSize++)
                    if (x[YSize] != x[YSize - 1])
                        break;
            Z = z;
            Y = y;
            X = x;
            Options = options;
            PlotType = plotType;
        }

        public StoredPlot(double[,] zz, string options = "", PlotTypes plotType = PlotTypes.SplotZZ)
        {
            ZZ = zz;
            Options = options;
            PlotType = plotType;
        }

        private bool IsFile(string functionOrFilename)
        {
            int dot = functionOrFilename.LastIndexOf(".");
            if (dot < 1) return false;
            if (char.IsLetter(functionOrFilename[dot - 1]) || char.IsLetter(functionOrFilename[dot + 1]))
                return true;
            return false;
        }

    }


}
