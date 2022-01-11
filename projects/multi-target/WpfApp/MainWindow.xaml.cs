using Microsoft.Maui.Graphics;
using Microsoft.Maui.Graphics.Skia;
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

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        readonly Random Rand = new();
        readonly DispatcherTimer RenderTimer = new();

        public MainWindow()
        {
            InitializeComponent();
            RenderTimer.Interval = TimeSpan.FromMilliseconds(10);
            RenderTimer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            SKElement1.InvalidateVisual();
        }

        private void SKElement1_PaintSurface(object sender, SkiaSharp.Views.Desktop.SKPaintSurfaceEventArgs e)
        {
            ICanvas canvas = new SkiaCanvas() { Canvas = e.Surface.Canvas };
            StandardGraphics.Render.TestImage(Rand, canvas, (float)SKElement1.ActualWidth, (float)SKElement1.ActualHeight);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SKElement1.InvalidateVisual();
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            RenderTimer.Start();
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            RenderTimer.Stop();
        }
    }
}
