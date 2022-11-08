public struct RawColor
{
    public readonly byte R, G, B;

    public RawColor(byte r, byte g, byte b)
    {
        R = r;
        G = g;
        B = b;
    }

    public static RawColor Random(Random rand)
    {
        byte r = (byte)rand.Next(256);
        byte g = (byte)rand.Next(256);
        byte b = (byte)rand.Next(256);
        return new RawColor(r, g, b);
    }

    public static RawColor Gray(byte value)
    {
        return new RawColor(value, value, value);
    }
}
