using Model;
using System;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace Viewer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Reset();
        }

        Field field;
        private void Reset()
        {
            field = new Field(
                width: skglControl1.Width,
                height: skglControl1.Height,
                boidCount: (int)BoidCountNud.Value,
                predatorCount: (int)PredatorCountNud.Value,
                random: RandomCheckbox.Checked);

            AttractTrackbar_Scroll(null, null);
            AvoidTrackbar_Scroll(null, null);
            AlignTrackbar_Scroll(null, null);
            VisionTrackbar_Scroll(null, null);
        }
        private void skglControl1_SizeChanged(object sender, EventArgs e) => Reset();
        private void ResetButton_Click(object sender, EventArgs e) => Reset();
        private void BoidCountNud_ValueChanged(object sender, EventArgs e) => Reset();

        private void PredatorCountNud_ValueChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < field.Boids.Count(); i++)
                field.Boids[i].IsPredator = (i < PredatorCountNud.Value);
        }

        private readonly Stopwatch stopwatch = new Stopwatch();
        private void timer1_Tick(object sender, EventArgs e)
        {
            stopwatch.Restart();
            field.Advance(SpeedTrackbar.Value * .2);
            skglControl1.Invalidate();
            double fps = (double)stopwatch.ElapsedTicks / Stopwatch.Frequency;
            FpsLabel.Text = $"{1 / fps:0.00} FPS";
        }

        private void skglControl1_PaintSurface(object sender, SkiaSharp.Views.Desktop.SKPaintGLSurfaceEventArgs e)
        {
            Renderer.Render(e.Surface.Canvas, field,
                tails: TailsCheckbox.Checked,
                ranges: RangeCheckbox.Checked,
                direction: DirectionCheckbox.Checked);
        }

        private void AttractTrackbar_Scroll(object sender, EventArgs e)
        {
            double frac = 2.0 * AttractTrackbar.Value / AttractTrackbar.Maximum;
            field.WeightFlock = Math.Pow(frac, 3);
        }

        private void AvoidTrackbar_Scroll(object sender, EventArgs e)
        {
            double frac = 2.0 * AvoidTrackbar.Value / AvoidTrackbar.Maximum;
            field.WeightAvoid = Math.Pow(frac, 4);
        }

        private void AlignTrackbar_Scroll(object sender, EventArgs e)
        {
            double frac = 2.0 * AlignTrackbar.Value / AlignTrackbar.Maximum;
            field.WeightAlign = Math.Pow(frac, 3);
        }

        private void VisionTrackbar_Scroll(object sender, EventArgs e)
        {
            field.Vision = 10 * VisionTrackbar.Value;
        }
    }
}
