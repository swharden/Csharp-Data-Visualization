using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PixelSetting
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InitializeBitmap();
        }

        private Bitmap bmp;
        Random rand = new Random();

        private void InitializeBitmap()
        {
            bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height, PixelFormat.Format8bppIndexed);

            byte[] bmpBytes = BitmapToBytes(bmp);

            // calculate bitmap width in memory (always a multiple of 4 pixels)
            int stride = bmp.Width;
            if ((bmp.Width % 4) > 0)
                stride += 4 - (bmp.Width % 4);

            // assign pixel value to correspond with column number
            for (int row = 0; row < bmp.Height; row++)
            {
                for (int col = 0; col < bmp.Width; col++)
                {
                    int bytePos = row * stride + col;
                    double value = Math.Sin((double)row / 20) + Math.Cos((double)col / 20);
                    value = (value + 2) / 4;
                    bmpBytes[bytePos] = (byte)(256 * value);
                }
            }

            Console.WriteLine($"Created new bitmap {bmp.Size} ({bmpBytes.Length} bytes) with stride {stride}");
            pictureBox1.Image = BitmapFromBytes(bmpBytes, bmp.Size);

        }

        public byte[] BitmapToBytes(Bitmap bmp)
        {
            // return a bitmap (of any image format) as a byte array
            int bytesPerPixel = Image.GetPixelFormatSize(bmp.PixelFormat) / 8;
            Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            BitmapData bmpData = bmp.LockBits(rect, ImageLockMode.ReadOnly, bmp.PixelFormat);
            byte[] bytes = new byte[bmpData.Stride * bmp.Height * bytesPerPixel];
            Marshal.Copy(bmpData.Scan0, bytes, 0, bytes.Length);
            bmp.UnlockBits(bmpData);
            return bytes;
        }

        public Bitmap BitmapFromBytes(byte[] bytes, Size size, PixelFormat format = PixelFormat.Format8bppIndexed)
        {
            // create a bitmap given a byte array of raw data
            Bitmap bmp = new Bitmap(size.Width, size.Height, format);
            Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            BitmapData bmpData = bmp.LockBits(rect, ImageLockMode.ReadWrite, bmp.PixelFormat);
            Marshal.Copy(bytes, 0, bmpData.Scan0, bytes.Length);
            bmp.UnlockBits(bmpData);

            // Create a grayscale palette (though any LUT could be used)
            System.Drawing.Imaging.ColorPalette pal = bmp.Palette;
            for (int i = 0; i < 256; i++)
                pal.Entries[i] = System.Drawing.Color.FromArgb(255, i, i, i);
            bmp.Palette = pal;

            return bmp;
        }

        private void PictureBox1_SizeChanged(object sender, EventArgs e)
        {
            InitializeBitmap();
        }

    }
}
