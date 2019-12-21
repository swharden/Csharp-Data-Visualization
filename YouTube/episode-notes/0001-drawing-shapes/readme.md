# Drawing Shapes with System.Drawing

### `System.Drawing` Considerations
* Simple and easy to learn
* Supported everywhere
  * .NET Framework out of the box 
  * .NET Core with the [System.Drawing.Common NuGet package](https://www.nuget.org/packages/System.Drawing.Common/)
* Moderate performance
* Not thread safe - must render in your GUI thread for WinForms

### Main Points
* Draw on a `PictureBox` to prevent flicker (it's double-buffered)
* Create a `Bitmap` the size of your Picturebox
* Use `Graphics` to draw on the `Bitmap`


### Code Highlights
```cs

```

## Next video:
* use a static class to handle rendering
* dependency injection 