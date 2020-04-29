using OpenTK.Graphics.ES20;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SkiaSharpOpenGLBenchmark
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Random rand = new Random(0);
        List<double> renderTimesMsec = new List<double>();
        private void Render(int lineCount)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            // set up the Skia surface using OpenGL
            SKColorType colorType = SKColorType.Rgba8888;
            GRContext contextOpenGL = GRContext.Create(GRBackend.OpenGL, GRGlInterface.CreateNativeGlInterface());
            GL.GetInteger(GetPName.FramebufferBinding, out var framebuffer);
            GRGlFramebufferInfo glInfo = new GRGlFramebufferInfo((uint)framebuffer, colorType.ToGlSizedFormat());
            GL.GetInteger(GetPName.StencilBits, out var stencilBits);
            GRBackendRenderTarget renderTarget = new GRBackendRenderTarget(
                width: skglControl1.Width,
                height: skglControl1.Height,
                sampleCount: contextOpenGL.GetMaxSurfaceSampleCount(colorType),
                stencilBits: stencilBits,
                glInfo: glInfo);
            SKSurface surface = SKSurface.Create(
                context: contextOpenGL, 
                renderTarget: renderTarget, 
                origin: GRSurfaceOrigin.BottomLeft, 
                colorType: colorType);

            // perform the drawing
            surface.Canvas.Clear(SKColor.Parse("#003366"));
            for (int i=0; i<lineCount; i++)
            {
                var paint = new SKPaint
                {
                    Color = new SKColor(
                        red: (byte)rand.Next(255),
                        green: (byte)rand.Next(255),
                        blue: (byte)rand.Next(255),
                        alpha: (byte)rand.Next(255)),
                    StrokeWidth = rand.Next(1, 10),
                    IsAntialias = true
                };
                surface.Canvas.DrawLine(
                    x0: rand.Next(skglControl1.Width),
                    y0: rand.Next(skglControl1.Height),
                    x1: rand.Next(skglControl1.Width),
                    y1: rand.Next(skglControl1.Height),
                    paint: paint);
            }

            // Force a display
            surface.Canvas.Flush();
            skglControl1.SwapBuffers();

            // prevent memory access violations by disposing before exiting
            renderTarget?.Dispose();
            contextOpenGL?.Dispose();
            surface?.Dispose();

            stopwatch.Stop();
            renderTimesMsec.Add(1000.0 * stopwatch.ElapsedTicks / Stopwatch.Frequency);
            double mean = renderTimesMsec.Sum() / renderTimesMsec.Count();
            Debug.WriteLine($"Render {renderTimesMsec.Count:00} " +
                $"took {renderTimesMsec.Last():0.000} ms " +
                $"(running mean: {mean:0.000} ms)");

            Application.DoEvents();
        }

        private void Benchmark(int lineCount, int times = 10)
        {
            rand = new Random(0);
            renderTimesMsec.Clear();
            for (int i = 0; i < times; i++)
                Render(lineCount);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Benchmark(10);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Benchmark(1_000);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Benchmark(10_000);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Benchmark(100_000);
        }
    }
}
