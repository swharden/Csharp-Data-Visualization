using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        DispatcherTimer timer;

        public MainWindow()
        {
            InitializeComponent();

            timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(.020) };
            timer.Tick += Render;
            timer.Start();
        }

        Random rand = new Random();
        Stopwatch stopwatch = Stopwatch.StartNew();
        int renderCount = 0;
        private bool busyRendering = false;
        private void Render(object sender, EventArgs e)
        {
            if (busyRendering == false)
            {
                busyRendering = true;
                //Render_DrawingVisual();
                Render_LineGeometry();
                renderCount += 1;
                double elapsedSeconds = (double)stopwatch.ElapsedMilliseconds / 1000;
                Title = string.Format("Rendered {0} frames in {1:0.00} seconds ({2:0.00} Hz)", renderCount, elapsedSeconds, renderCount / elapsedSeconds);
            }

            busyRendering = false;
        }

        private void Render_LineGeometry()
        {
            myCanvas.Children.Clear();
            Brush brush = new SolidColorBrush(Color.FromArgb(50, 255, 255, 255));
            for (int i = 0; i < 1_000; i++)
            {
                LineGeometry myLineGeometry = new LineGeometry()
                {
                    StartPoint = new Point(rand.Next((int)myCanvas.ActualWidth), rand.Next((int)myCanvas.ActualHeight)),
                    EndPoint = new Point(rand.Next((int)myCanvas.ActualWidth), rand.Next((int)myCanvas.ActualHeight))
                };
                
                Path myPath = new Path
                {
                    Stroke = brush,
                    StrokeThickness = 1,
                    Data = myLineGeometry
                };

                myCanvas.Children.Add(myPath);
            }
        }

        private void Render_DrawingVisual()
        {
            myCanvas.Children.Clear();

            DrawingVisual drawingVisual = new DrawingVisual();
            DrawingContext drawingContext = drawingVisual.RenderOpen();

            Brush brush = new SolidColorBrush(Color.FromArgb(50, 255, 255, 255));
            Pen pen = new Pen(brush, 1);
            for (int i = 0; i < 1_000; i++)
            {
                Point ptA = new Point(rand.Next((int)myCanvas.ActualWidth), rand.Next((int)myCanvas.ActualHeight));
                Point ptB = new Point(rand.Next((int)myCanvas.ActualWidth), rand.Next((int)myCanvas.ActualHeight));
                drawingContext.DrawLine(pen, ptA, ptB);
            }

            drawingContext.Close();

            RenderTargetBitmap bmp = new RenderTargetBitmap((int)myCanvas.ActualWidth, (int)myCanvas.ActualHeight, 0, 0, PixelFormats.Pbgra32);
            bmp.Render(drawingVisual);

            Image image = new Image();
            image.Source = bmp;

            myCanvas.Children.Add(image);
        }

        private void myCanvas_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            stopwatch.Restart();
            renderCount = 0;
        }
    }
}
