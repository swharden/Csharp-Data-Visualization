using GraphicsModel;
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

namespace ViewerWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Field field;
        DispatcherTimer renderTimer = new DispatcherTimer();

        public MainWindow()
        {
            InitializeComponent();
            Window_SizeChanged(null, null);

            renderTimer.Interval = TimeSpan.FromMilliseconds(1);
            renderTimer.Tick += timer_Tick;
            renderTimer.Start();
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            field?.Dispose();
            field = new Field((float)myCanvas.ActualWidth, (float)myCanvas.ActualHeight, 3);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            field.Dispose();
        }

        void timer_Tick(object sender, EventArgs e)
        {
            Bitmap bmp = new Bitmap((int)myCanvas.ActualWidth, (int)myCanvas.ActualHeight);
            RenderSystemDrawing.Renderer.Render(field, bmp);
            myImage.Source = BmpImageFromBmp(bmp);
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
    }
}
