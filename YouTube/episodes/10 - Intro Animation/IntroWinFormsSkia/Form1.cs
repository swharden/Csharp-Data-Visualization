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

namespace IntroSkia
{
    public partial class Form1 : Form
    {
        OpenTK.GLControl glControl1;
        public Form1()
        {
            InitializeComponent();

            // create the OpenGL control formatted to match Skia
            var colorFormat = new OpenTK.Graphics.ColorFormat(32);
            var graphicsMode = new OpenTK.Graphics.GraphicsMode(colorFormat, 24, 8, 4);
            glControl1 = new OpenTK.GLControl(graphicsMode)
            {
                BackColor = Color.Black,
                Dock = DockStyle.Fill,
                VSync = true
            };

            panel1.Controls.Add(glControl1);
        }

        IntroAnimation.Field field;
        private void Render(int cornerCount = 100, int maxVisibleDistance = 200)
        {
            // create the field if needed or if the size changed
            if (field == null || field.width != glControl1.Width || field.height != glControl1.Height)
                field = new IntroAnimation.Field(glControl1.Width, glControl1.Height, cornerCount);

            // step the field forward in time
            field.StepForward(3);

            // Create a Skia surface using the OpenGL control
            SKColorType colorType = SKColorType.Rgba8888;
            GRContext contextOpenGL = GRContext.Create(GRBackend.OpenGL, GRGlInterface.CreateNativeGlInterface());
            GL.GetInteger(GetPName.FramebufferBinding, out var framebuffer);
            GRGlFramebufferInfo glInfo = new GRGlFramebufferInfo((uint)framebuffer, colorType.ToGlSizedFormat());
            GL.GetInteger(GetPName.StencilBits, out var stencil);
            GRBackendRenderTarget renderTarget = new GRBackendRenderTarget(glControl1.Width, glControl1.Height, contextOpenGL.GetMaxSurfaceSampleCount(colorType), stencil, glInfo);
            SKSurface surface = SKSurface.Create(contextOpenGL, renderTarget, GRSurfaceOrigin.BottomLeft, colorType);
            SKCanvas canvas = surface.Canvas;

            // draw the stuff
            var bgColor = field.GetBackgroundColor();
            canvas.Clear(new SKColor(bgColor.R, bgColor.G, bgColor.B));

            // draw circles at every corner
            var paint = new SKPaint { Color = new SKColor(255, 255, 255), IsAntialias = true };
            float radius = 2;
            for (int cornerIndex = 0; cornerIndex < field.corners.Length; cornerIndex++)
                canvas.DrawCircle((float)field.corners[cornerIndex].X, (float)field.corners[cornerIndex].Y, radius, paint);

            // draw lines between every corner and every other corner
            for (int i = 0; i < field.corners.Length; i++)
            {
                for (int j = 0; j < field.corners.Length; j++)
                {
                    double distance = field.GetDistance(i, j);
                    if (distance < maxVisibleDistance && distance != 0)
                    {
                        SKPoint pt1 = new SKPoint((float)field.corners[i].X, (float)field.corners[i].Y);
                        SKPoint pt2 = new SKPoint((float)field.corners[j].X, (float)field.corners[j].Y);
                        double distanceFraction = distance / maxVisibleDistance;
                        byte alpha = (byte)(255 - distanceFraction * 256);
                        var linePaint = new SKPaint { Color = new SKColor(255, 255, 255, alpha), IsAntialias = true };
                        canvas.DrawLine(pt1, pt2, linePaint);
                    }
                }
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
            Text = field.GetBenchmarkMessage();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Render();
        }
    }
}
