using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QuickstartWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Random rand = new Random();
        Bitmap bmp;
        Graphics gfx;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ResizeBitmap();
            Render();
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ResizeBitmap();
            Render();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Render();
        }

        private static BitmapImage BmpImageFromBmp(System.Drawing.Bitmap bmp)
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

        void ResizeBitmap()
        {
            gfx?.Dispose();
            bmp?.Dispose();

            bmp = new Bitmap((int)myCanvas.ActualWidth, (int)myCanvas.ActualHeight);
            gfx = Graphics.FromImage(bmp);
        }

        private void Render()
        {
            using (var brush = new SolidBrush(Color.White))
            using (var pen = new Pen(brush))
            {
                gfx.SmoothingMode = SmoothingMode.AntiAlias;

                gfx.Clear(Color.Navy);
                for (int i = 0; i < 1000; i++)
                {
                    gfx.DrawLine(pen: pen,
                        x1: rand.Next(bmp.Width),
                        y1: rand.Next(bmp.Height),
                        x2: rand.Next(bmp.Width),
                        y2: rand.Next(bmp.Height));
                }
            }

            myImage.Source = BmpImageFromBmp(bmp);
        }
    }
}
