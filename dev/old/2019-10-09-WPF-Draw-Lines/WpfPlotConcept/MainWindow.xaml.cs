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

namespace WpfPlotConcept
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        readonly Random rand = new Random();
        DispatcherTimer timer = new DispatcherTimer();

        public MainWindow()
        {
            InitializeComponent();

            timer.Interval = TimeSpan.FromSeconds(.001);
            timer.Tick += Render;
            timer.Start();
        }

        private Point randomPoint { get { return new Point(randomX, randomY); } }
        private double randomX { get { return rand.NextDouble() * myCanvas.ActualWidth; } }
        private double randomY { get { return rand.NextDouble() * myCanvas.ActualHeight; } }

        private bool busyRendering = false;
        private void Render()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            busyRendering = true;

            //DrawUsingLineGeometry();
            //DrawUsingDrawingVisualBitmap();
            DrawUsingDrawingVisualRaw();

            Canvas.SetZIndex(btnRender, int.MaxValue); // raise the button

            double elapsedSec = (double)stopwatch.ElapsedTicks / Stopwatch.Frequency;
            Title = string.Format("Rendered in {0:0.00} ms ({1:0.00} Hz)", elapsedSec * 1000.0, 1 / elapsedSec);
            busyRendering = false;
        }

        private void Render(object sender, EventArgs e)
        {
            if (!busyRendering)
                Render();
        }

        private static readonly Action EmptyDelegate = delegate { };

        private void DrawUsingDrawingVisualRaw()
        {
            visThing.drawVis.Render(width: (int)visThing.ActualWidth, height: (int)visThing.ActualHeight);
        }

        private void DrawUsingDrawingVisualBitmap()
        {
            myCanvas.Children.Clear();

            DrawingVisual drawingVisual = new DrawingVisual();

            DrawingContext drawingContext = drawingVisual.RenderOpen();

            Pen pen = new Pen(new SolidColorBrush(Color.FromRgb(0, 0, 0)), 1);
            for (int i = 0; i < 1_000; i++)
            {
                drawingContext.DrawLine(pen, randomPoint, randomPoint);
            }

            drawingContext.Close();

            RenderTargetBitmap bmp = new RenderTargetBitmap((int)myCanvas.ActualWidth, (int)myCanvas.ActualHeight, 0, 0, PixelFormats.Pbgra32);
            bmp.Render(drawingVisual);

            Image image = new Image();
            image.Source = bmp;

            myCanvas.Children.Add(image);
        }

        private void DrawUsingLineGeometry()
        {
            myCanvas.Children.Clear();

            for (int i = 0; i < 1_000; i++)
            {
                LineGeometry myLineGeometry = new LineGeometry
                {
                    StartPoint = randomPoint,
                    EndPoint = randomPoint
                };

                Path myPath = new Path
                {
                    Stroke = Brushes.Black,
                    StrokeThickness = 1,
                    Data = myLineGeometry
                };

                myCanvas.Children.Add(myPath);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Render();
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Render();
        }

        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed)
                Render();
        }
    }
}
