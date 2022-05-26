---
Title: Maui.Graphics Maui Quickstart
Description: How to draw on a graphics view using Maui.Graphics in a MAUI application
Date: 2021-10-27
---

```xml
<ContentPage xmlns=http://schemas.microsoft.com/dotnet/2021/maui
             xmlns:x=http://schemas.microsoft.com/winfx/2009/xaml
             xmlns:drawable="clr-namespace:MyMauiApp"
             x:Class="MyMauiApp.MainPage">
    <ContentPage.Resources>
        <drawable:GraphicsDrawable x:Key="drawable" />
    </ContentPage.Resources>
    <StackLayout>
        <GraphicsView Drawable="{StaticResource drawable}"
                      HeightRequest="500"
                      WidthRequest="500" />
    </StackLayout>
</ContentPage>
```

```cs
using Microsoft.Maui.Graphics;

namespace MyMauiApp
{
    public class GraphicsDrawable : IDrawable
    {
        public void Draw(ICanvas canvas, RectangleF dirtyRect)
        {
            canvas.StrokeColor = Colors.Red;
            canvas.StrokeSize = 6;
            canvas.DrawLine(10, 50, 90, 100);
        }      
    }
}
```

### Resources

* [Microsoft.Maui.Graphics #111](https://github.com/dotnet/Microsoft.Maui.Graphics/discussions/111)
* [Use Microsoft.Maui.Graphics to Draw 2D Graphics in Any .NET Application](https://swharden.com/blog/2022-05-25-maui-graphics)
* [https://maui.graphics](https://maui.graphics)