public class RawBitmap
{
    public readonly int Width;
    public readonly int Height;
    public readonly byte[] ImageBytes;

    public RawBitmap(int width, int height)
    {
        Width = width;
        Height = height;
        ImageBytes = new byte[width * height * 4];
    }

    private void SetPixel(int x, int y, byte R, byte G, byte B)
    {
        if (x < 0 || x >= Width)
            return;

        if (y < 0 || y >= Height)
            return;

        int offset = ((Height - y - 1) * Width + x) * 4;
        ImageBytes[offset + 0] = B;
        ImageBytes[offset + 1] = G;
        ImageBytes[offset + 2] = R;
    }

    public void FillRectangle(int x, int y, int width, int height, byte R, byte G, byte B)
    {
        for (int i = x; i < x + width; i++)
            for (int j = y; j < y + height; j++)
                SetPixel(i, j, R, G, B);
    }

    public byte[] GetBitmapBytes()
    {
        const int imageHeaderSize = 54;
        const int dbHeaderSize = 40;

        byte[] bmpBytes = new byte[ImageBytes.Length + imageHeaderSize];
        bmpBytes[0] = (byte)'B';
        bmpBytes[1] = (byte)'M';
        bmpBytes[14] = dbHeaderSize;

        Array.Copy(BitConverter.GetBytes(bmpBytes.Length), 0, bmpBytes, 2, 4);
        Array.Copy(BitConverter.GetBytes(imageHeaderSize), 0, bmpBytes, 10, 4);
        Array.Copy(BitConverter.GetBytes(Width), 0, bmpBytes, 18, 4);
        Array.Copy(BitConverter.GetBytes(Height), 0, bmpBytes, 22, 4);
        Array.Copy(BitConverter.GetBytes(32), 0, bmpBytes, 28, 2);
        Array.Copy(BitConverter.GetBytes(ImageBytes.Length), 0, bmpBytes, 34, 4);
        Array.Copy(ImageBytes, 0, bmpBytes, imageHeaderSize, ImageBytes.Length);

        return bmpBytes;
    }

    public void Save(string filename)
    {
        byte[] bytes = GetBitmapBytes();
        File.WriteAllBytes(filename, bytes);
    }
}