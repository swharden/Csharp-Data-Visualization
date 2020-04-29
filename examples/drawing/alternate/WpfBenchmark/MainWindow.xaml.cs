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

namespace WpfBenchmark
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        Random rand = new Random(0);
        List<double> renderTimesMsec = new List<double>();

        private void Render(int lineCount)
        {
            // render by directly drawing WPF primitives

            Title = "WPF Primitive Drawing Benchmark";

            Stopwatch stopwatch = Stopwatch.StartNew();

            myCanvas.Children.Clear();
            var bgColor = (Color)ColorConverter.ConvertFromString("#003366");
            myCanvas.Background = new SolidColorBrush(bgColor);

            for (int i = 0; i < lineCount; i++)
            {
                var p1 = new Point(rand.Next((int)myCanvas.ActualWidth), rand.Next((int)myCanvas.ActualHeight));
                var p2 = new Point(rand.Next((int)myCanvas.ActualWidth), rand.Next((int)myCanvas.ActualHeight));
                LineGeometry lineGeom = new LineGeometry { StartPoint = p1, EndPoint = p2 };

                var lineColor = Color.FromArgb(
                    a: (byte)rand.Next(255),
                    r: (byte)rand.Next(255),
                    g: (byte)rand.Next(255),
                    b: (byte)rand.Next(255));
                Path linePath = new Path
                {
                    Stroke = new SolidColorBrush(lineColor),
                    StrokeThickness = rand.Next(1, 10),
                    Data = lineGeom
                };

                myCanvas.Children.Add(linePath);
            }

            // force a render
            Application.Current.Dispatcher.Invoke(
                System.Windows.Threading.DispatcherPriority.Render, 
                new Action(delegate { }));

            stopwatch.Stop();
            renderTimesMsec.Add(1000.0 * stopwatch.ElapsedTicks / Stopwatch.Frequency);
            double mean = renderTimesMsec.Sum() / renderTimesMsec.Count();
            Debug.WriteLine($"Render {renderTimesMsec.Count:00} " +
                $"took {renderTimesMsec.Last():0.000} ms " +
                $"(running mean: {mean:0.000} ms)");
        }

        private void Render2(int lineCount)
        {
            // render using the DrawingVisual method

            Title = "WPF DrawingVisual Benchmark";

            Stopwatch stopwatch = Stopwatch.StartNew();

            myCanvas.Children.Clear();
            var bgColor = (Color)ColorConverter.ConvertFromString("#003366");
            myCanvas.Background = new SolidColorBrush(bgColor);

            // create a visual
            DrawingVisual drawingVisual = new DrawingVisual();
            DrawingContext drawingContext = drawingVisual.RenderOpen();
            for (int i = 0; i < lineCount; i++)
            {
                var p1 = new Point(rand.Next((int)myCanvas.ActualWidth), rand.Next((int)myCanvas.ActualHeight));
                var p2 = new Point(rand.Next((int)myCanvas.ActualWidth), rand.Next((int)myCanvas.ActualHeight));

                var lineColor = Color.FromArgb(
                    a: (byte)rand.Next(255),
                    r: (byte)rand.Next(255),
                    g: (byte)rand.Next(255),
                    b: (byte)rand.Next(255));
                var pen = new Pen(new SolidColorBrush(lineColor), rand.Next(1, 10));

                drawingContext.DrawLine(pen, p1, p2);
            }
            drawingContext.Close();

            // render the visual on a bitmap
            var bmp = new RenderTargetBitmap(
                pixelWidth: (int)myCanvas.ActualWidth, 
                pixelHeight: (int)myCanvas.ActualHeight, 
                dpiX: 0, dpiY: 0, pixelFormat: PixelFormats.Pbgra32);
            bmp.Render(drawingVisual);

            // create a new Image and display the bitmap in it then add it to the canvas
            Image image = new Image();
            image.Source = bmp;
            myCanvas.Children.Add(image);

            // force a render
            Application.Current.Dispatcher.Invoke(
                System.Windows.Threading.DispatcherPriority.Render,
                new Action(delegate { }));

            stopwatch.Stop();
            renderTimesMsec.Add(1000.0 * stopwatch.ElapsedTicks / Stopwatch.Frequency);
            double mean = renderTimesMsec.Sum() / renderTimesMsec.Count();
            Debug.WriteLine($"Render {renderTimesMsec.Count:00} " +
                $"took {renderTimesMsec.Last():0.000} ms " +
                $"(running mean: {mean:0.000} ms)");
        }

        private void Benchmark(int lineCount, int times = 10)
        {
            rand = new Random(0);
            renderTimesMsec.Clear();
            for (int i = 0; i < times; i++)
                Render2(lineCount);
        }

        private void Button10_Click(object sender, RoutedEventArgs e)
        {
            Benchmark(10);
        }

        private void Button1k_Click(object sender, RoutedEventArgs e)
        {
            Benchmark(1_000);
        }

        private void Button10k_Click(object sender, RoutedEventArgs e)
        {
            Benchmark(10_000);
        }

        private void Button100k_Click(object sender, RoutedEventArgs e)
        {
            Benchmark(100_000);
        }
    }
}
