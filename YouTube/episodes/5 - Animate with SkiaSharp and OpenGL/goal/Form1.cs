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

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Random rand = new Random();
        Stopwatch stopwatch = Stopwatch.StartNew();
        int renderCount = 0;
        void Render()
        {
            // Create a Skia surface using the OpenGL control
            int width = glControl1.Width;
            int height = glControl1.Height;
            SKColorType colorType = SKColorType.Rgba8888;
            GRContext contextOpenGL = GRContext.Create(GRBackend.OpenGL, GRGlInterface.CreateNativeGlInterface());
            GL.GetInteger(GetPName.FramebufferBinding, out var framebuffer);
            GRGlFramebufferInfo glInfo = new GRGlFramebufferInfo((uint)framebuffer, colorType.ToGlSizedFormat());
            GL.GetInteger(GetPName.StencilBits, out var stencil);
            GRBackendRenderTarget renderTarget = new GRBackendRenderTarget(width, height, contextOpenGL.GetMaxSurfaceSampleCount(colorType), stencil, glInfo);
            SKSurface surface = SKSurface.Create(contextOpenGL, renderTarget, GRSurfaceOrigin.BottomLeft, colorType);
            SKCanvas canvas = surface.Canvas;

            // draw some lines
            canvas.Clear(SKColor.Parse("#003366"));
            var paint = new SKPaint
            {
                Color = new SKColor(255, 255, 255, 50),
                IsAntialias = true
            };
            for (int i = 0; i < 1_000; i++)
            {
                SKPoint ptA = new SKPoint(rand.Next(width), rand.Next(height));
                SKPoint ptB = new SKPoint(rand.Next(width), rand.Next(height));
                canvas.DrawLine(ptA, ptB, paint);
            }

            // Force a display
            surface.Canvas.Flush();
            glControl1.SwapBuffers();

            // dispose to prevent memory access violations while exiting
            renderTarget?.Dispose();
            contextOpenGL?.Dispose();
            canvas?.Dispose();
            surface?.Dispose();

            // update the FPS display
            renderCount += 1;
            double elapsedSeconds = (double)stopwatch.ElapsedMilliseconds / 1000;
            Text = string.Format("Rendered {0} frames in {1:0.00} seconds ({2:0.00} Hz)", renderCount, elapsedSeconds, renderCount / elapsedSeconds);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Render();
        }
    }
}
