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

namespace WindowsFormsApp39
{
    public partial class Form1 : Form
    {
        Starfield.Field field;
        public Form1()
        {
            InitializeComponent();
        }

        Stopwatch stopwatch = Stopwatch.StartNew();
        int renderCount = 0;

        void RenderWithSkia()
        {
            int width = glControl1.Width;
            int height = glControl1.Height;

            if (field==null || field.width != width || field.height != height)
                field = new Starfield.Field(100_000, width, height);
            field.StepForward();

            // Create a Skia surface using the OpenGL control
            SKColorType colorType = SKColorType.Rgba8888;
            GRContext contextOpenGL = GRContext.Create(GRBackend.OpenGL, GRGlInterface.CreateNativeGlInterface());
            GL.GetInteger(GetPName.FramebufferBinding, out var framebuffer);
            GRGlFramebufferInfo glInfo = new GRGlFramebufferInfo((uint)framebuffer, colorType.ToGlSizedFormat());
            GL.GetInteger(GetPName.StencilBits, out var stencil);
            GRBackendRenderTarget renderTarget = new GRBackendRenderTarget(width, height, contextOpenGL.GetMaxSurfaceSampleCount(colorType), stencil, glInfo);
            SKSurface surface = SKSurface.Create(contextOpenGL, renderTarget, GRSurfaceOrigin.BottomLeft, colorType);
            SKCanvas canvas = surface.Canvas;

            // draw the starfield
            var paint = new SKPaint
            {
                Color = SKColors.White,
                IsAntialias = true
            };

            canvas.Clear(SKColors.Black);
            foreach (Starfield.Star star in field.stars)
                //canvas.DrawRect(star.X, star.Y, star.Size, star.Size, paint);
                canvas.DrawCircle(new SKPoint(star.X, star.Y), star.Size/2, paint);

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
            RenderWithSkia();
        }
    }
}
