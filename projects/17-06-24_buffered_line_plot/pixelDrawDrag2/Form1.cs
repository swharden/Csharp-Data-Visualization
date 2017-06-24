using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic; // so we can use lists

namespace pixelDrawDrag2
{
    public partial class Form1 : Form
    {
        public List<int> listTimes = new List<int>();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void timesReset(object sender, EventArgs e)
        {
            listTimes.Clear();
        }

        /// <summary>
        /// draw a bunch of lines into pictureBox1 using a buffered bitmap
        /// </summary>
        /// <param name="nLines"></param>
        public int DrawSine()
        {
            // time how long this takes
            var watch = System.Diagnostics.Stopwatch.StartNew();

            // seed the time offset (to make sine wave a function of system time)
            int offsetMillisecond = DateTimeOffset.Now.Minute * 60000 + DateTimeOffset.Now.Second * 1000 + DateTimeOffset.Now.Millisecond;

            // make image buffer and graphics object
            int width = pictureBox1.Width, height = pictureBox1.Height;
             Bitmap buffer = new Bitmap(width, height);
            System.Drawing.Graphics gfx = Graphics.FromImage(buffer);

            // prep the pen and RNG
            Pen blackPen = new Pen(Color.Black, (int)numericUpDown1.Value); // black pen width 1
            Random rand = new Random();

            // create the point array (each point is an X,Y pair)
            Point[] points = Enumerable.Repeat<Point>(new Point(0, 1), width).ToArray();

            // fill the point away with data
            for (int x = 0; x < width; x++)
            {
                double t = ((double)offsetMillisecond/50)+ x; // seed with time (increase number to slow down translation)
                t /= (double)(width / 50); // divide it down (increase number to increase sine wave frequency)
                int y = (int)((Math.Sin(t)/2.0+.5)*height);
                points[x] = new Point(x, y);
            }


            // set anti-alias here if you wish
            if (checkBox1.Checked)
            {
                gfx.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            } else
            {
                gfx.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;
            }

            // draw stuff on the buffered graphic
            gfx.DrawLines(blackPen, points);

            // apply the buffer to the picturebox
            pictureBox1.Image = buffer;

            // stop the timer and show how long it took
            watch.Stop();
            //var elapsedMs = watch.ElapsedMilliseconds;
            //float sec = (float)DateTimeOffset.Now.Second;
            //sec += (float)DateTimeOffset.Now.Millisecond/1000;
            //textBox1.AppendText(String.Format("[{2:00.000}] drew {0} lines in {1} ms\n", width.ToString(), elapsedMs.ToString(), sec));
            return (int)watch.ElapsedMilliseconds;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            listTimes.Add(DrawSine());
            while (listTimes.Count > 100)
            {
                listTimes.RemoveAt(0);
            }
            double timeAverage = listTimes.Average();
            textBox1.Text = String.Format("Average time of drawing the last {0} frames is {1:00.000} ms", listTimes.Count, timeAverage);
            //Application.DoEvents();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            timesReset(sender, e);
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            timesReset(sender, e);
        }
    }
}
