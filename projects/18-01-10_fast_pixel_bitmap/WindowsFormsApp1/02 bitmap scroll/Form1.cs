using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Drawing.Imaging; // for ImageLockMode
using System.Runtime.InteropServices; // for Marshal

namespace _02_bitmap_scroll
{
    public partial class Form1 : Form
    {
        private static int update_count = 0;
        private static Random rand = new Random();

        private static List<List<double>> data; // a list of column pixel values

        public Form1()
        {
            InitializeComponent();
            Data_init();
        }

        /// <summary>
        /// return a list of random doubles from 0.0 to 1.0
        /// </summary>
        private List<double> Generate_list_random(int itemCount) {
            List<double> list = new List<double>(itemCount);
            for (int i=0; i< itemCount; i++)
            {
                list.Add(rand.NextDouble());
            }
            return list;
        }
        

        /// <summary>
        /// fill "data" with 2d random data
        /// </summary>
        private void Data_init()
        {
            int data_samples = pictureBox1.Width;
            int data_size = pictureBox1.Height;

            data = new List<List<double>>();
            for (int i=0; i<data_samples; i++)
            {
                data.Add(Generate_list_random(data_size));
            }

            System.Console.WriteLine("generated random data");

        }

        private void Bitmap_from_data()
        {
            // create a bitmap we will work with
            Bitmap bitmap = new Bitmap(data.Count, data[0].Count, PixelFormat.Format8bppIndexed);

            // modify the indexed palette to make it grayscale
            ColorPalette pal = bitmap.Palette;
            for (int i = 0; i < 256; i++)
                pal.Entries[i] = Color.FromArgb(255, i, i, i);
            bitmap.Palette = pal;

            // prepare to access data via the bitmapdata object
            BitmapData bitmapData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                                                    ImageLockMode.ReadOnly, bitmap.PixelFormat);

            // create a byte array to reflect each pixel in the image
            byte[] pixels = new byte[bitmapData.Stride * bitmap.Height];

            // fill pixel array with data
            for (int col=0; col<data.Count; col++)
            {
                for (int row=0; row<data[col].Count; row++)
                {
                    int bytePosition = row * bitmapData.Stride + col;
                    pixels[bytePosition] = (byte)(255 * data[col][row]);
                }
            }

            // turn the byte array back into a bitmap
            Marshal.Copy(pixels, 0, bitmapData.Scan0, pixels.Length);
            bitmap.UnlockBits(bitmapData);

            // apply the bitmap to the picturebox
            pictureBox1.Image = bitmap;

        }

        private void Bitmap_roll()
        {
            data.Insert(data.Count, data[0]);
            data.RemoveAt(0);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            for (int i=0; i<5; i++)
            {
                Bitmap_roll();
            }
            
            Bitmap_from_data();
            update_count += 1;
            label4.Text = string.Format("Update count: {0}", update_count);
        }
    }
}
