/// ScottPlot - highspeed interactive data plotting for Visual Studio
/// 
/// This is an early development version work in progress.
/// Ultimately this code may mature into a visual studio extension.
/// For now, let's compartmentalize everything into a single file.
/// 
/// AUTHOR:
///     Scott W Harden (http://www.SWHarden.com)
///     
/// LATEST CODE:
///     https://github.com/swharden/Csharp-Data-Visualization

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

public class ScottPlot {

    public double Version = 160625; // date code
    public int pxWidth = 800; // dimension of data figure in pixels
    public int pxHeight = 600; // dimension of data figure in pixels
    private System.Diagnostics.Stopwatch stopwatch = System.Diagnostics.Stopwatch.StartNew();
    public Bitmap bufferGraph;
    public System.Drawing.Graphics gfxGraph;
    private Random rand = new Random(); // seed random number generator
    public bool highQuality = true;

    // data
    List<double> Xs = new List<double>();
    List<double> Ys = new List<double>();
    public long nPlots; // number of times a plot was drawn

    // style
    public Pen penDefault = new Pen(Color.Black, 1); // default pen color
    public Color bg = Color.White; // default background color



    /// <summary>
    /// ScottPlot - highspeed interactive data plotting for Visual Studio [init level]
    /// </summary>
    /// <param name="pxWidth">width of the output data area (pixels)</param>
    /// <param name="pxHeight">height of the output data area (pixels)</param>
    public ScottPlot() {
        Console.WriteLine("creating a new ScottPlot class from scratch");
        this.InitBitmap();
    }



    /* #############################################################################
     * CONFIGURE THE PLOT AREA
     * ############################################################################# 
     */

    public void setSize(int pxWidth, int pxHeight) {
        this.pxWidth = pxWidth;
        this.pxHeight = pxHeight;
        this.InitBitmap();
    }

    /// <summary>
    /// set up (or reset) the bitmap buffer and graphics object. Required after resizing.
    /// </summary>
    public void InitBitmap() {
        this.bufferGraph = new Bitmap(this.pxWidth, this.pxHeight);
        this.gfxGraph = Graphics.FromImage(this.bufferGraph);
        if (this.highQuality) {
            this.gfxGraph.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
        } else {
            this.gfxGraph.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;
        }
    }

    /// <summary>
    /// clear the graphics area completely
    /// </summary>
    public void Clear() {
        this.gfxGraph.Clear(this.bg); //TODO: draw a square instead
    }
    /* #############################################################################
     * LOAD DATA OR CREATE XY DATA
     * ############################################################################# 
     */

    /// <summary>
    /// fill this.Ys with random data
    /// </summary>
    /// <param name="nPoints">number of random data points to generate</param>
    public void CreateDataRandom(int nPoints = 321) {
        this.Xs.Clear();
        this.Ys.Clear();
        for (int x = 0; x < nPoints; x++) {
            this.Xs.Add(x);
            this.Ys.Add(this.rand.NextDouble() * 100);
        }
        this.AxisFit();
    }

    public void CreateDataSine(int nPoints = 321, double freq=20) {
        this.Xs.Clear();
        this.Ys.Clear();
        double offset = DateTime.Now.Ticks / System.TimeSpan.TicksPerMillisecond / 20.0;
        for (int x = 0; x < nPoints; x++) {
            this.Xs.Add(x);
            this.Ys.Add(Math.Sin(freq*(x+offset)/nPoints));
        }
        this.AxisFit();
    }

    public void CreateDataCSV(string fname = "data.csv", int rate = 20000) {
        this.Xs.Clear();
        this.Ys.Clear();
        System.IO.StreamReader file = new System.IO.StreamReader(fname);
        string line;
        int counter = 0;
        while ((line = file.ReadLine()) != null) {
            this.Ys.Add(Double.Parse(line));
            this.Xs.Add((double)counter / rate);
            counter++;
        }
        file.Close();
        this.AxisFit();
    }

    /* #############################################################################
     * MANIPULATE XY DATA
     * ############################################################################# 
     */
    /// <summary>
    /// roll the Ys data horizontally
    /// </summary>
    /// <param name="rollBy">how many values to roll the data by</param>
    public void Roll(int rollBy = 1) {
        for (int i = 0; i < rollBy; i++) {
            Ys.Add(Ys[0]);
            Ys.RemoveAt(0);
        }
    }

    /* #############################################################################
     * AXIS
     * ############################################################################# 
     */

    public double axisX1;
    public double axisX2;
    public double axisY1;
    public double axisY2;
    public double axisUnitsPerPxX;
    public double axisUnitsPerPxY;
    public double axisPixelsPerUnitX;
    public double axisPixelsPerUnitY;

    // run this every time after you change the axis
    public void AxisToPixels() {
        this.axisUnitsPerPxX = (this.axisX2 - this.axisX1) / (this.pxWidth - 1);
        this.axisUnitsPerPxY = (this.axisY2 - this.axisY1) / (this.pxHeight - 1);
        this.axisPixelsPerUnitX = (this.pxWidth - 1) / (this.axisX2 - this.axisX1);
        this.axisPixelsPerUnitY = (this.pxHeight - 1) / (this.axisY2 - this.axisY1);
    }

    // fit axis to data
    public void AxisFit(bool x1 = true, bool x2 = true, bool y1 = true, bool y2 = true) {
        if (this.Xs.Count() > 0) {
            if (x1) this.axisX1 = this.Xs.Min();
            if (x2) this.axisX2 = this.Xs.Max();
        }
        if (this.Ys.Count() > 0) {
            if (y1) this.axisY1 = this.Ys.Min();
            if (y2) this.axisY2 = this.Ys.Max();
        }
        this.AxisToPixels(); // make sure this is up to date
    }
    
    /// <summary>
    /// shift the axis (by a number of pixels) without changing zoom
    /// </summary>
    /// <param name="Xpx"></param>
    /// <param name="Ypx"></param>
    public void AxisPanPx(int Xpx = 0, int Ypx = 0) {
        this.axisX1 += this.axisUnitsPerPxX * Xpx;
        this.axisX2 += this.axisUnitsPerPxX * Xpx;
        this.axisY1 += this.axisUnitsPerPxY * Ypx;
        this.axisY2 += this.axisUnitsPerPxY * Ypx;
        this.AxisToPixels(); // make sure this is up to date
    }

    /// <summary>
    /// zoom the axis (by a number of pixels) without changing the center point
    /// </summary>
    /// <param name="Xpx"></param>
    /// <param name="Ypx"></param>
    public void AxisZoomPx(int Xpx = 0, int Ypx = 0) {
        // if the mouse goes nuts, we should shift past 0 and "flip" our data. Let's prevent that.
        double maxX = (axisX2 - axisX1) * this.axisPixelsPerUnitX / 2; // the most it could be in pixels
        double maxY = (axisY2 - axisY1) * this.axisPixelsPerUnitX / 2; // the most it could be in pixels
        double shiftX = Math.Min(Xpx * this.axisUnitsPerPxX, maxX); // choose the less dramatic shift
        double shiftY = Math.Min(Ypx * this.axisUnitsPerPxY, maxY); // choose the less dramatic shift
        this.axisX1 -= shiftX;
        this.axisX2 += shiftX;
        this.axisY1 -= shiftY;
        this.axisY2 += shiftY;
        this.AxisToPixels();
    }
    

    /* #############################################################################
     * PLOTTING AND GRAPHIC MODIFICATION
     * ############################################################################# 
     */

    /// <summary>
    /// With Ys (and maybe Xs) loaded with values, plot it to the bitmap image
    /// </summary>
    public void PlotXY(bool clearFirst = true) {

        // make sure we have data to plot
        if (this.Ys.Count() == 0) {
            System.Console.WriteLine("no Y data. will not plot.");
            return;
        }

        // clear the old graphics
        if (clearFirst) this.Clear();

        this.AxisToPixels(); // make sure this is up to date

        double x, y;
        List<Point> xyPoints = new List<Point>();
        for (int i = 0; i < this.Ys.Count(); i++) {

            // get initial values (in units)
            x = this.Xs[i];
            y = this.Ys[i];

            // set offset (by units)
            x -= this.axisX1;
            y -= this.axisY1;

            // units -> pixels
            x *= axisPixelsPerUnitX;
            y *= axisPixelsPerUnitY;

            // flip y (since pixels are numbered low to high from top to bottom)
            y = this.pxHeight - y - 1; // not sure why the +1 helps but it does

            // add to the array
            xyPoints.Add(new Point((int)x, (int)y));
        }

        // draw data lines
        this.gfxGraph.DrawLines(penDefault, xyPoints.ToArray());

        /*
        // draw data markers (circles)
        int markerSize = 5;
        int markerOffset = markerSize / 2;
        System.Drawing.SolidBrush markerBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Blue);
        for (int i=0; i<xyPoints.Count(); i++) {
            //this.gfxGraph.DrawEllipse(markerPen, xyPoints[i].X-markerOffset, xyPoints[i].Y-markerOffset, markerSize, markerSize);
            this.gfxGraph.FillEllipse(markerBrush, xyPoints[i].X - markerOffset, xyPoints[i].Y - markerOffset, markerSize, markerSize);
        }
        */
        

        // incriment plot counter
        this.nPlots++;

        // optionally show a report
        this.Info();
    }


    /* #############################################################################
     * PROGRESS ASSESSMENT
     * ############################################################################# 
     */

    public void TimerReset() { this.stopwatch.Reset(); this.nPlots = 0; }
    public void TimerStart() { this.stopwatch.Start(); }
    public void TimerStop() { this.stopwatch.Stop(); }

    /// <summary>
    /// return a string showing details about the timer
    /// </summary>
    /// <returns></returns>
    public string TimerVal() {
        double ms = this.stopwatch.ElapsedTicks * 1000 / System.Diagnostics.Stopwatch.Frequency;
        return string.Format("plotted {4} points {0} times in {1} ms (average {2:00.00 ms}) [{3:00.00 Hz}]",
            this.nPlots, ms, (float)ms / this.nPlots, (float)1000 * this.nPlots / ms, this.Ys.Count());
    }

    /// <summary>
    /// show info about the current state of the image
    /// </summary>
    public void Info() {
        // show image dimensions
        Console.WriteLine("pxWidth={0}, pxHeight={1}", pxWidth, pxHeight);

        // show timer state
        Console.WriteLine(this.TimerVal());

        // show X/Y data
        Console.WriteLine("Ys: ");
        for (int i = 0; i < this.Ys.Count(); i++) {
            Console.Write(" {0:0.00}", this.Ys[i]);
            if (i > 10) {
                Console.WriteLine("...");
                break;
            }
        }
        Console.WriteLine("Xs: ");
        for (int i = 0; i < this.Xs.Count(); i++) {
            Console.Write(" {0:0.00}", this.Xs[i]);
            if (i > 10) {
                Console.WriteLine("...");
                break;
            }
        }

        // show axis info
        Console.WriteLine("pixels per unit: ({0}, {1})", this.axisPixelsPerUnitX, this.axisPixelsPerUnitY);
        Console.WriteLine("units per pixel: ({0}, {1})", this.axisUnitsPerPxX, this.axisUnitsPerPxY);

    }

    /* #############################################################################
     * DEVELOPMENT CODE BELOW THIS POINT
     * ############################################################################# 
     */

}