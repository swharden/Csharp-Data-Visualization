using Boids.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Boids.Viewer
{
    public partial class Form1 : Form
    {
        Model.Field field;

        public Form1()
        {
            InitializeComponent();
            Reset();
        }

        private void pictureBox1_SizeChanged(object sender, EventArgs e)
        {
            Reset();
        }

        private void Reset(int boidCount = 20)
        {
            field = new Field(pictureBox1.Width, pictureBox1.Height, boidCount);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            RenderField();
        }

        private void RenderBoid(Graphics gfx, Boid boid)
        {
            var boidOutline = new Point[]
            {
                new Point(0, 0),
                new Point(-5, -1),
                new Point(0, 10),
                new Point(5, -1),
                new Point(0, 0),
            };

            using (var brush = new SolidBrush(Color.LightGreen))
            {
                gfx.TranslateTransform((float)boid.X, (float)boid.Y);
                gfx.RotateTransform((float)boid.GetAngle());
                gfx.FillClosedCurve(brush, boidOutline);
                gfx.ResetTransform();
            }
        }

        private void RenderField()
        {
            field.Advance();

            using (var bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height))
            using (var gfx = Graphics.FromImage(bmp))
            {
                gfx.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                gfx.Clear(ColorTranslator.FromHtml("#2f3539"));

                foreach (var boid in field.boids)
                    RenderBoid(gfx, boid);

                pictureBox1.Image?.Dispose();
                pictureBox1.Image = (Bitmap)bmp.Clone();
            }
        }
    }
}
