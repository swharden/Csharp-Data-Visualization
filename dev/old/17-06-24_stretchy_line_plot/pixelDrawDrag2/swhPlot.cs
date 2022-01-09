using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic; // so we can use lists

using System.Drawing;
//using System.Windows.Forms;

namespace pixelDrawDrag2 {
    class ScottPlot {
        public double Version=160624; // this is an early development version of this class.
        public int pxWidth; // dimension of data figure in pixels
        public int pxHeight; // dimension of data figure in pixels
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
        public ScottPlot(int pxWidth=800, int pxHeight=600) {
            Console.WriteLine("creating a new ScottPlot class from scratch");
            this.pxWidth = pxWidth;
            this.pxHeight = pxHeight;
            this.InitBitmap();
            this.DevRandomData();
            this.Info();
            this.PlotXY();
        }

        /// <summary>
        /// roll the Ys data horizontally
        /// </summary>
        /// <param name="rollBy">how many values to roll the data by</param>
        public void Roll(int rollBy=1) {
            for(int i = 0; i < rollBy; i++) {
                Ys.Add(Ys[0]);
                Ys.RemoveAt(0);
            }
        }

        /// <summary>
        /// With Ys (and maybe Xs) loaded with values, plot it to the bitmap image
        /// </summary>
        public void PlotXY(bool clearFirst=true) {
            // clear the old graphics
            if (clearFirst) {
                this.Clear();
            }
            // prepare data points
            int X, Y;
            Point[] points = Enumerable.Repeat<Point>(new Point(0, 1), Ys.Count()).ToArray();
            for (int i = 0; i < this.Ys.Count(); i++) {
                X = i * pxWidth / this.Ys.Count();
                Y = (int)((this.Ys[i] * pxHeight / this.Ys.Max()));
                points[i] = new Point(X, Y);
            }
            // draw the data lines
            this.gfxGraph.DrawLines(penDefault, points);

            // incriment plot counter
            this.nPlots++;
        }

        /// <summary>
        /// fill this.Ys with random data
        /// </summary>
        /// <param name="nPoints">number of random data points to generate</param>
        public void DevRandomData(int nPoints=321) {
            System.Console.WriteLine("clearing data and creating random data");
            this.Xs.Clear();
            this.Ys.Clear();
            for (int x = 0; x < nPoints; x++) {
                this.Ys.Add(this.rand.NextDouble() * 100);
            }
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

            this.gfxGraph.Clear(this.bg);
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

        }
        
        public void TimerReset() {this.stopwatch.Reset();}
        public void TimerStart() {this.stopwatch.Start();}
        public void TimerStop() {this.stopwatch.Stop();}

        /// <summary>
        /// return a string showing details about the timer
        /// </summary>
        /// <returns></returns>
        public string TimerVal(){
            double ms = this.stopwatch.ElapsedTicks * 1000 / System.Diagnostics.Stopwatch.Frequency;
            return string.Format("plotted {4} points {0} times in {1} ms (average {2:00.00 ms}) [{3:00.00 Hz}]",
                this.nPlots, ms, (float)ms / this.nPlots, (float)1000 * this.nPlots / ms, this.Ys.Count());
        }
    }
}
