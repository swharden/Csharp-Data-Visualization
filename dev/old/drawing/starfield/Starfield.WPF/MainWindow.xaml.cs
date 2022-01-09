using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
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

namespace Starfield.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        readonly Field field = new Field(500);

        public MainWindow()
        {
            InitializeComponent();

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(1);
            timer.Tick += timer_Tick;
            timer.Start();
        }

        Stopwatch stopwatch = new Stopwatch();
        void timer_Tick(object sender, EventArgs e)
        {
            stopwatch.Restart();
            field.Advance();
            Bitmap bmp = new Bitmap((int)myCanvas.ActualWidth, (int)myCanvas.ActualHeight);
            byte alpha = (byte)(mySlider.Value * 255 / 100);
            var starColor = System.Drawing.Color.FromArgb(alpha, 255, 255, 255);
            field.Render(bmp, starColor);
            myImage.Source = BmpImageFromBmp(bmp);
            double elapsedSec = (double)stopwatch.ElapsedTicks / Stopwatch.Frequency;
            Title = $"Starfield in WPF - {elapsedSec * 1000:0.00} ms ({1 / elapsedSec:0.00} FPS)";
        }

        private BitmapImage BmpImageFromBmp(Bitmap bmp)
        {
            using (var memory = new System.IO.MemoryStream())
            {
                bmp.Save(memory, System.Drawing.Imaging.ImageFormat.Png);
                memory.Position = 0;

                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
                bitmapImage.Freeze();

                return bitmapImage;
            }
        }

        private void Set500Stars(object sender, RoutedEventArgs e)
        {
            field.Reset(500);
        }

        private void Set100kStars(object sender, RoutedEventArgs e)
        {
            field.Reset(100_000);
        }
    }
}
