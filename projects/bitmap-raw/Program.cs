RawBitmap bmp = new(500, 300);

Random rand = new();
for (int i = 0; i < 1000; i++)
{
    int x = rand.Next(bmp.Width);
    int y = rand.Next(bmp.Height);
    int width = rand.Next(50);
    int height = rand.Next(50);
    byte R = (byte)rand.Next(256);
    byte G = (byte)rand.Next(256);
    byte B = (byte)rand.Next(256);
    bmp.FillRectangle(x, y, width, height, R, G, B);
}

bmp.Save("test.bmp");