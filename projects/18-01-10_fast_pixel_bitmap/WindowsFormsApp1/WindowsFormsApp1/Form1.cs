using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// this method puts pixel values into memory and is slower (~99ms)
        /// </summary>
        private void btn_putPixel_Click(object sender, EventArgs e)
        {
            Random rand = new Random();
            var watch = System.Diagnostics.Stopwatch.StartNew();

            int width = pictureBox1.Width;
            int height = pictureBox1.Height;
            Bitmap buffer = new Bitmap(width, height);

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Color NewPixel = Color.FromArgb(255, rand.Next(255), rand.Next(255), rand.Next(255));
                    buffer.SetPixel(x, y, NewPixel);
                }
            }

            pictureBox1.Image = buffer;

            label4.Text = string.Format("putPixel method took {0} ms", watch.ElapsedMilliseconds);
        }

        /// <summary>
        /// this method puts pixel values into memory and is faster (~6ms)
        /// </summary>
        private void btn_fast_Click(object sender, EventArgs e)
        {
            Random rand = new Random();
            var watch = System.Diagnostics.Stopwatch.StartNew();

            int width = pictureBox1.Width;
            int height = pictureBox1.Height;
            Bitmap buffer = new Bitmap(width, height);

            BitmapData bitmapData = buffer.LockBits(new Rectangle(0, 0, buffer.Width, buffer.Height), ImageLockMode.ReadWrite, buffer.PixelFormat);
            int bytesPerPixel = Bitmap.GetPixelFormatSize(buffer.PixelFormat) / 8;
            int byteCount = bitmapData.Stride * buffer.Height;
            byte[] pixels = new byte[byteCount];
            IntPtr ptrFirstPixel = bitmapData.Scan0;
            Marshal.Copy(ptrFirstPixel, pixels, 0, pixels.Length);
            int heightInPixels = bitmapData.Height;
            int widthInBytes = bitmapData.Width * bytesPerPixel;

            for (int y = 0; y < heightInPixels; y++)
            {
                int currentLine = y * bitmapData.Stride;
                for (int x = 0; x < widthInBytes; x = x + bytesPerPixel)
                {
                    pixels[currentLine + x] = (byte)rand.Next(255);
                    pixels[currentLine + x + 1] = (byte)rand.Next(255);
                    pixels[currentLine + x + 2] = (byte)rand.Next(255);
                    pixels[currentLine + x + 3] = 255; // alpha
                }
            }
            Marshal.Copy(pixels, 0, ptrFirstPixel, pixels.Length);
            buffer.UnlockBits(bitmapData);
            pictureBox1.Image = buffer;

            label4.Text = string.Format("faster method took {0} ms", watch.ElapsedMilliseconds);
        }
    }
}