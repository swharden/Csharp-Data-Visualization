using Microsoft.Maui.Graphics;

namespace GraphicsModelLibrary
{
    public interface IGraphicsModel : IDrawable
    {
        void Reset(float width, float height);
        void Resize(float width, float height);
        void Draw(ICanvas canvas, float width, float height);
        void Advance(float time = 1);
    }
}