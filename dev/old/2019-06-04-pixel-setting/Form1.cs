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
            InitializeBitmapAndGraphics();
        }

        private Bitmap bmp;
        private Graphics gfx;
        Random rand = new Random();

        private void InitializeBitmapAndGraphics()
        {
            bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            gfx = Graphics.FromImage(bmp);

            byte[] bmpBytes = BitmapToBytes(bmp);
            for (int i = 0; i < bmpBytes.Length; i++)
                bmpBytes[i] = (byte)rand.Next(256);

            Console.WriteLine($"Created new bitmap {bmp.Size} ({bmpBytes.Length} bytes)");
            pictureBox1.Image = BitmapFromBytes(bmpBytes, bmp.Size);

        }

        public byte[] BitmapToBytes(Bitmap bmp)
        {
            // return a bitmap (of any image format) as a byte array
            int bytesPerPixel = Image.GetPixelFormatSize(bmp.PixelFormat) / 8;
            byte[] bytes = new byte[bmp.Width * bmp.Height * bytesPerPixel];
            Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            BitmapData bmpData = bmp.LockBits(rect, ImageLockMode.ReadOnly, bmp.PixelFormat);
            Marshal.Copy(bmpData.Scan0, bytes, 0, bytes.Length);
            bmp.UnlockBits(bmpData);
            return bytes;
        }

        public Bitmap BitmapFromBytes(byte[] bytes, Size size, PixelFormat format = PixelFormat.Format32bppArgb)
        {
            // create a bitmap given a byte array of raw data
            Bitmap bmp = new Bitmap(size.Width, size.Height, format);
            Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            BitmapData bmpData = bmp.LockBits(rect, ImageLockMode.ReadWrite, bmp.PixelFormat);
            Marshal.Copy(bytes, 0, bmpData.Scan0, bytes.Length);
            bmp.UnlockBits(bmpData);
            return bmp;
        }

        private void PictureBox1_SizeChanged(object sender, EventArgs e)
        {
            InitializeBitmapAndGraphics();
        }

    }
}
