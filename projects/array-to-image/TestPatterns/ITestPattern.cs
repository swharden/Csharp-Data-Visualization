using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArrayToImage;

public interface ITestPattern
{
    public string Name { get; }
    public byte[,,] Generate(int width, int height);
}
