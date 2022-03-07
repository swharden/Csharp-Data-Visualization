namespace ArrayToImage;

public static class Program
{
    public static void Main()
    {
        byte[,,] pixelArray = GetTestPattern();
        DemoSystemDrawing.SaveImageRgb("rgb-SystemDrawing.png", pixelArray);
    }

    private static byte[,,] GetTestPattern(int width = 653, int height = 487, int channels = 3)
    {
        byte[,,] pixelArray = new byte[height, width, channels];

        double xPeriod = 20;
        double yPeriod = 20;

        for (int y = 0; y < pixelArray.GetLength(0); y++)
        {
            for (int x = 0; x < pixelArray.GetLength(1); x++)
            {
                double xSin = (Math.Sin(x / xPeriod) + 1) / 2;
                double ySin = (Math.Sin(y / yPeriod) + 1) / 2;
                double vSin = (xSin + ySin) * 128;

                pixelArray[y, x, 2] = (byte)vSin; // blue
                pixelArray[y, x, 1] = (byte)x; // green
                pixelArray[y, x, 0] = (byte)y; // red
            }
        }

        return pixelArray;
    }
}