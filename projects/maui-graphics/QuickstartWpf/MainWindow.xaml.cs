using Microsoft.Maui.Graphics;
using Microsoft.Maui.Graphics.Skia;
using System;
using System.Windows;
using System.Windows.Threading;

namespace QuickstartWpf
{
    public partial class MainWindow : Window
    {
        readonly DispatcherTimer Timer1 = new();

        public MainWindow()
        {
            InitializeComponent();
            Timer1.Interval = System.TimeSpan.FromMilliseconds(10);
            Timer1.Tick += Timer1_Tick;
        }

        private void SKElement_PaintSurface(object sender, SkiaSharp.Views.Desktop.SKPaintSurfaceEventArgs e)
        {
            ICanvas canvas = new SkiaCanvas() { Canvas = e.Surface.Canvas };

            canvas.FillColor = Colors.Navy;
            canvas.FillRectangle(0, 0, (float)SkElement1.ActualWidth, (float)SkElement1.ActualHeight);

            canvas.StrokeColor = Colors.White.WithAlpha(.5f);
            canvas.StrokeSize = 2;
            for (int i = 0; i < 100; i++)
            {
                float x = Random.Shared.Next((int)SkElement1.ActualWidth);
                float y = Random.Shared.Next((int)SkElement1.ActualHeight);
                float r = Random.Shared.Next(5, 50);
                canvas.DrawCircle(x, y, r);
            }
        }

        private void SKElement_SizeChanged(object sender, SizeChangedEventArgs e) => SkElement1.InvalidateVisual();
        private void Button_Click(object sender, RoutedEventArgs e) => SkElement1.InvalidateVisual();
        private void Timer1_Tick(object? sender, System.EventArgs e) => SkElement1.InvalidateVisual();
        private void Checkbox1_Checked(object sender, RoutedEventArgs e) => Timer1.Start();
        private void Checkbox1_Unchecked(object sender, RoutedEventArgs e) => Timer1.Stop();
    }
}