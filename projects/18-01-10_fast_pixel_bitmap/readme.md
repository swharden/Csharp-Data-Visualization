# Fast Pixel Drawing in C#
The purpose of this example is to determine/demonstrate the fastest/simplest methods to take an array of data and plot it into a bitmap. It compares the SetPixel method to the locked bits method. This example takes a 400x600 bitmap and fills it with random RGB values. The bitmap is then applied to a picturebox.

<img src="screenshot.png" width="300">

### Example using `Bitmap.SetPixel` (~100ms)
```
Bitmap buffer = new Bitmap(width, height);
Color NewPixel = Color.FromArgb(255, 123, 213, 059);
buffer.SetPixel(x, y, NewPixel); // modify the pixel (RGBA) with a Color
pictureBox1.Image = buffer;
```

### Example using `Bitmap.Lockbits` (~5ms)
```
Bitmap buffer = new Bitmap(width, height);
BitmapData bitmapData = buffer.LockBits(new Rectangle(0, 0, buffer.Width, buffer.Height), ImageLockMode.ReadWrite, buffer.PixelFormat);
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
```c
Bitmap bitmap = new Bitmap(600, 400, PixelFormat.Format8bppIndexed);
BitmapData bitmapData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, bitmap.PixelFormat);

// create a byte array to reflect each pixel in the image
byte[] pixels = new byte[bitmap.Size.Height * bitmap.Size.Width];

// fill pixels with random data
for (int i=0; i < pixels.Length; i++) pixels[i] = (byte) rand.Next(255);

// turn the byte array back into a bitmap
Marshal.Copy(pixels, 0, bitmapData.Scan0, pixels.Length);
bitmap.UnlockBits(bitmapData);

// apply the bitmap to the picturebox
pictureBox1.Image = bitmap;
```

### Useful Links
* [High performance System.Drawing.Bitmap pixel manipulation in C#](http://erison.blogspot.com/2016/02/techdercom-high-performance.html)
