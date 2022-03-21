namespace ArrayToImage;

public interface IGraphicsPlatform
{
    public string Name { get; }
    public void SaveImageRgb(string filePath, byte[,,] pixelArray);
    public byte[,,] LoadImageRgb(string filePath);
}
