---
Title: Cross-Platform Support for System.Drawing.Common
Description: About the end of cross-platform support for System.Drawing.Common and what to do about it
Date: 2022-03-11 7:00:00
weight: 999
---

## History

* `System.Drawing` started as a Windows-Only namespace
* In 2018 the [`System.Drawing.Common` package](https://www.nuget.org/packages/System.Drawing.Common/) brought `System.Drawing` to cross-platform .NET Core projects
* In 2021 the dotnet design team [decided to only support Windows](https://github.com/dotnet/designs/blob/main/accepted/2021/system-drawing-win-only/system-drawing-win-only.md) as a [breaking change](https://docs.microsoft.com/en-us/dotnet/core/compatibility/core-libraries/6.0/system-drawing-common-windows-only)
* In 2021 .NET 6 projects began warning `CA1416: This call site is reachable on all platforms`
* In 2022 .NET 7 projects using `System.Drawing.Common` began throwing `PlatformNotSupportedException`

## Why was cross-platform support dropped?

Summarizing Microsoft's [Reason for change](https://docs.microsoft.com/en-us/dotnet/core/compatibility/core-libraries/6.0/system-drawing-common-windows-only),

* Cross-platform support for `System.Drawing.Common` depends on `libgdiplus`

* `libgdiplus` is a mess and Microsoft does not want to support it. It a large (30k line) C code base without tests and has numerous external dependencies (`cairo`, `pango`, etc.)

* _"It's not viable to get `libgdiplus` to the point where its feature set and quality is on par with the rest of the .NET stack."_

* _"From analysis of NuGet packages ... we haven't noticed heavy graphics usage ... [these projects are] typically well supported with SkiaSharp and ImageSharp."_

* `System.Drawing.Common` will now only be developed for Windows Forms

## What to do about it?

### Target Windows

This problem is resolved if you update your csproj file to only target Windows:

```xml
<PropertyGroup>
  <TargetFramework>net6.0-windows</TargetFramework>
</PropertyGroup>

<ItemGroup>
  <PackageReference Include="System.Drawing.Common" />
</ItemGroup>
```

### Use an Old Package and Silence the Warning

I think if you continue installing the .NET 5 version of the [`System.Drawing.Common` package](https://www.nuget.org/packages/System.Drawing.Common/) and edit your csproj file to silence the warning, you may get a little more life out of your existing project.

```xml
<PropertyGroup>
  <TargetFramework>net5.0</TargetFramework>
  <NoWarn>1416</NoWarn>
</PropertyGroup>

<ItemGroup>
  <PackageReference Include="System.Drawing.Common" Version="5.*" />
</ItemGroup>
```

### Use Another Graphics Technology

Quickstart guides are available on this website for:
* `SkiaSharp`
* `Microsoft.Maui.Graphics`
* `ImageSharp`

Pro: These newer projects are more performant and thread-safe.

Con: It's a lot of work to migrate to these platforms and it may require big changes to your project's API.

### Use `System.Drawing.Primitives`

To use a different rendering technology without breaking existing APIs it may be possible to use `Color`, `Point`, `Rect`, etc. from the [`System.Drawing.Primitives` package](https://www.nuget.org/packages/System.Drawing.Primitives) which will continue cross-platform support but not contain any rendering capabilities.

## References

* NuGet: [`System.Drawing.Common` package](https://www.nuget.org/packages/System.Drawing.Common/)

* NuGet: [`System.Drawing.Primitives` package](https://www.nuget.org/packages/System.Drawing.Primitives/)

* Official: [System.Drawing.Common only supported on Windows](https://docs.microsoft.com/en-us/dotnet/core/compatibility/core-libraries/6.0/system-drawing-common-windows-only)

* Dotnet design proposal: [Make System.Drawing.Common only supported on Windows](https://github.com/dotnet/designs/blob/main/accepted/2021/system-drawing-win-only/system-drawing-win-only.md)

* Scott Hanselman's [_How do you use System.Drawing in .NET Core?_](https://www.hanselman.com/blog/how-do-you-use-systemdrawing-in-net-core) is an old article written when .NET Core was new but before `System.Drawing.Common` was released. It recommends SkiaSharp and ImageSharp as alternatives for .NET Core projects.

* How to use [floating versions in your csproj file](https://docs.microsoft.com/en-us/nuget/concepts/dependency-resolution#floating-versions)