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

namespace WpfPlotConcept
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        readonly Random rand = new Random();

        public MainWindow()
        {
            InitializeComponent();
        }

        private Point randomPoint { get { return new Point(randomX, randomY); } }
        private double randomX { get { return rand.NextDouble() * myCanvas.ActualWidth; } }
        private double randomY { get { return rand.NextDouble() * myCanvas.ActualHeight; } }

        private void Render()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            myCanvas.Children.Clear();

            for (int i=0; i<1_000; i++)
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

            Canvas.SetZIndex(btnRender, int.MaxValue); // raise the button

            double elapsedSec = (double)stopwatch.ElapsedTicks / Stopwatch.Frequency;
            Title = string.Format("Rendered in {0:0.00} ms ({1:0.00} Hz)", elapsedSec * 1000.0, 1 / elapsedSec);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Render();
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Render();
        }
    }
}
