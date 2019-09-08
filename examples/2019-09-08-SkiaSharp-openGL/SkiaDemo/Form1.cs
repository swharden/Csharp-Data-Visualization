using OpenTK.Graphics.ES20;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SkiaDemo
{
    public partial class Form1 : Form
    {
        Random rand = new Random();
        SKColorType colorType = SKColorType.Rgba8888;

        public Form1()
        {
            InitializeComponent();

            glControl1.VSync = false; // why?
            glControl1.Paint += new PaintEventHandler(RenderWithOpenGL);
        }

        private void RenderWithOpenGL(object sender, PaintEventArgs e)
        {
            Control sctl = (Control)sender;
            int width = sctl.Width;
            int height = sctl.Height;

            // setup the Skia surface using OpenGL

            GRContext contextOpenGL = GRContext.Create(GRBackend.OpenGL, GRGlInterface.CreateNativeGlInterface());

            GL.GetInteger(GetPName.FramebufferBinding, out var framebuffer);
            GRGlFramebufferInfo glInfo = new GRGlFramebufferInfo((uint)framebuffer, colorType.ToGlSizedFormat());
            GL.GetInteger(GetPName.StencilBits, out var stencil);
            GRBackendRenderTarget renderTarget = new GRBackendRenderTarget(width, height, contextOpenGL.GetMaxSurfaceSampleCount(colorType), stencil, glInfo);

            SKSurface surface = SKSurface.Create(contextOpenGL, renderTarget, GRSurfaceOrigin.BottomLeft, colorType);

            // Draw some stuff on the canvas

            surface.Canvas.Clear(SKColor.Parse("#FFFFFF")); // adds about 3ms fullscreen

            byte alpha = 128;
            var paint = new SKPaint();
            for (int i = 0; i < 1_000; i++)
            {
                float x1 = (float)(rand.NextDouble() * width);
                float x2 = (float)(rand.NextDouble() * width);
                float y1 = (float)(rand.NextDouble() * height);
                float y2 = (float)(rand.NextDouble() * height);

                paint.Color = new SKColor(
                        red: (byte)(rand.NextDouble() * 255),
                        green: (byte)(rand.NextDouble() * 255),
                        blue: (byte)(rand.NextDouble() * 255),
                        alpha: alpha
                    );

                surface.Canvas.DrawLine(x1, y1, x2, y2, paint);
            }

            surface.Canvas.Flush();
            glControl1.SwapBuffers();

            // prevent memory access violations by disposing before exiting
            renderTarget?.Dispose();
            contextOpenGL?.Dispose();
            surface?.Dispose(); 
        }

        private void BtnBenchmark_Click(object sender, EventArgs e)
        {
            int count = 250;

            btnBenchmark.Enabled = false;
            System.Diagnostics.Stopwatch stopwatch = System.Diagnostics.Stopwatch.StartNew();

            progressBar1.Maximum = count;
            for (int i = 0; i < count; i++)
            {
                lblStatus.Text = $"on render {i + 1} of {count}...";
                progressBar1.Value = i + 1;
                progressBar1.Value = i;
                Application.DoEvents(); // adds about 1ms fullscreen
                glControl1.Refresh();
            }
            progressBar1.Value = 0;

            double meanSec = (double)stopwatch.ElapsedTicks / System.Diagnostics.Stopwatch.Frequency / count;
            lblStatus.Text = string.Format("mean render {0:0.00} ms ({1:0.00} Hz)", meanSec * 1000, 1.0 / meanSec);
            btnBenchmark.Enabled = true;
        }
    }
}
