# Fast Pixel Drawing in C#
The purpose of this example is to determine/demonstrate the fastest/simplest methods to take an array of data and plot it into a bitmap. It compares the SetPixel method to the locked bits method. This example takes a 400x600 bitmap and fills it with random RGB values. The bitmap is then applied to a picturebox.

<img src="screenshot.png" width="300">

**These are needed for the examples:**
```c
using System.Drawing.Imaging; // for ImageLockMode
using System.Runtime.InteropServices; // for Marshal
```

### Example using `Bitmap.SetPixel` (~100ms)
```c
Bitmap buffer = new Bitmap(width, height);
Color NewPixel = Color.FromArgb(255, 123, 213, 059);
buffer.SetPixel(x, y, NewPixel); // modify the pixel (RGBA) with a Color
pictureBox1.Image = buffer;
```

### Example using `Bitmap.Lockbits` (~5ms)
This example containes extra code to show how to _read_ and also _write_ to a buffered bitmap by modifying a byte array.
```c
Bitmap buffer = new Bitmap(width, height);
BitmapData bitmapData = buffer.LockBits(new Rectangle(0, 0, buffer.Width, buffer.Height), 
                                        ImageLockMode.ReadWrite, buffer.PixelFormat);
int bytesPerPixel = Bitmap.GetPixelFormatSize(buffer.PixelFormat) / 8;
int byteCount = bitmapData.Stride * buffer.Height;
byte[] pixels = new byte[byteCount];
IntPtr ptrFirstPixel = bitmapData.Scan0;
Marshal.Copy(ptrFirstPixel, pixels, 0, pixels.Length);
pixels[someBytePosition] = 123; // set a single R, G, B, or A value as a byte
Marshal.Copy(pixels, 0, ptrFirstPixel, pixels.Length);
buffer.UnlockBits(bitmapData);
pictureBox1.Image = buffer;
```

### Example turning raw values into a bitmap (simple/fast)
This example shows how to make an 8-bit bitmap color-mapped to an indexed color pallette. By looking at this example you can figure out how to change the color pallette...

```c
// create a bitmap we will work with
Bitmap bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height, PixelFormat.Format8bppIndexed);

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

// fill pixels with random data
for (int i=0; i < pixels.Length; i++)
    pixels[i] = (byte) rand.Next(255);

// turn the byte array back into a bitmap
Marshal.Copy(pixels, 0, bitmapData.Scan0, pixels.Length);
bitmap.UnlockBits(bitmapData);            

// apply the bitmap to the picturebox
pictureBox1.Image = bitmap;
```

### Applying a 2d list to a roll-able bitmap
This example has been added to the project, but important code is here:

##### Creating data
```c
private static List<List<double>> data; // a list of column pixel values

private List<double> Generate_list_random(int itemCount) {
    List<double> list = new List<double>(itemCount);
    for (int i=0; i< itemCount; i++)
    {
        list.Add(rand.NextDouble());
    }
    return list;
}

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

```

##### Rolling the data
```c
private void Bitmap_roll()
{
    data.Insert(data.Count, data[0]);
    data.RemoveAt(0);
}
```

##### Applying the 2d list data to bitmap
```c
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
```
