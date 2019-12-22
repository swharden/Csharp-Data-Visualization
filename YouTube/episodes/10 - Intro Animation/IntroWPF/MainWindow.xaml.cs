using System;
using System.Collections.Generic;
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

namespace IntroWPF
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

            timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(.005) };
            timer.Tick += Render;
            timer.Start();
        }

        private void myCanvas_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            field = new IntroAnimation.Field((int)myCanvas.ActualWidth, (int)myCanvas.ActualHeight, 100);
        }

        IntroAnimation.Field field;
        private bool busyRendering = false;
        private void Render(object sender, EventArgs e)
        {
            if (field == null || busyRendering == true)
                return;
            else
                busyRendering = true;

            // step the field forward in time
            field.StepForward(3);

            myCanvas.Children.Clear();
            var bgColor = field.GetBackgroundColor();
            myCanvas.Background = new SolidColorBrush(Color.FromRgb(bgColor.R, bgColor.G, bgColor.B));

            // draw circles at every corner
            float radius = 2;
            for (int cornerIndex = 0; cornerIndex < field.corners.Length; cornerIndex++)
            {
                Point pt = new Point((float)field.corners[cornerIndex].X, (float)field.corners[cornerIndex].Y);
                EllipseGeometry circle = new EllipseGeometry(pt, radius, radius);
                Path circlePath = new Path() { Fill = Brushes.White, Data = circle };
                myCanvas.Children.Add(circlePath);
            }

            // draw lines between every corner and every other corner
            int maxVisibleDistance = 200;
            for (int i = 0; i < field.corners.Length; i++)
            {
                for (int j = 0; j < field.corners.Length; j++)
                {
                    double distance = field.GetDistance(i, j);
                    if (distance < maxVisibleDistance && distance != 0)
                    {
                        Point pt1 = new Point((float)field.corners[i].X, (float)field.corners[i].Y);
                        Point pt2 = new Point((float)field.corners[j].X, (float)field.corners[j].Y);
                        LineGeometry line = new LineGeometry(pt1, pt2);

                        double distanceFraction = distance / maxVisibleDistance;
                        byte alpha = (byte)(255 - distanceFraction * 256);
                        Brush lineBrush = new SolidColorBrush(Color.FromArgb(alpha, 255, 255, 255));
                        Path linePath = new Path { Stroke = lineBrush, StrokeThickness = 1, Data = line };
                        myCanvas.Children.Add(linePath);
                    }
                }
            }

            Title = field.GetBenchmarkMessage();
            busyRendering = false;
        }
    }
}
