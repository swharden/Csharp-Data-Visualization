using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArrayToImage;

public interface IImageMaker
{
    public string Name { get; }
    public void SaveImageRgb(string filePath, byte[,,] pixelArray);
}
