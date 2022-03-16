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

namespace QuickstartInteractiveDataDisplay
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Random Rand = new Random(0);

        public MainWindow()
        {
            InitializeComponent();
            Scatter();
        }

        private void Scatter()
        {
            // generate some random X and Y data
            int pointCount = 500;
            double[] xs1 = RandomWalk(pointCount);
            double[] ys1 = RandomWalk(pointCount);
            double[] xs2 = RandomWalk(pointCount);
            double[] ys2 = RandomWalk(pointCount);
            double[] sizes = Consecutive(pointCount, 10, 0);

            // create the lines and describe their styling
            var line1 = new InteractiveDataDisplay.WPF.CircleMarkerGraph()
            {
                Color = new SolidColorBrush(Colors.Blue),
                Description = "Group A",
                StrokeThickness = 1
            };

            var line2 = new InteractiveDataDisplay.WPF.CircleMarkerGraph()
            {
                Color = new SolidColorBrush(Colors.Red),
                Description = "Group B",
                StrokeThickness = 1
            };

            // load data into the lines
            line1.PlotSize(xs1, ys1, sizes);
            line2.PlotSize(xs2, ys2, sizes);

            // add lines into the grid
            myGrid.Children.Clear();
            myGrid.Children.Add(line1);
            myGrid.Children.Add(line2);

            // customize styling
            myChart.Title = $"Line Plot ({pointCount:n0} points each)";
            myChart.BottomTitle = $"Horizontal Axis Label";
            myChart.LeftTitle = $"Vertical Axis Label";
            myChart.IsAutoFitEnabled = true;
            myChart.LegendVisibility = Visibility.Hidden;
        }

        private double[] RandomWalk(int points = 5, double start = 100, double mult = 50)
        {
            // return an array of difting random numbers
            double[] values = new double[points];
            values[0] = start;
            for (int i = 1; i < points; i++)
                values[i] = values[i - 1] + (Rand.NextDouble() - .5) * mult;
            return values;
        }

        private double[] Consecutive(int points, double offset = 0, double stepSize = 1)
        {
            // return an array of ascending numbers starting at 1
            double[] values = new double[points];
            for (int i = 0; i < points; i++)
                values[i] = i * stepSize + 1 + offset;
            return values;
        }
    }
}
