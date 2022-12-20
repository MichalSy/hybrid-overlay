# hybrid-overlay

With this overlay app, any web application or website can be easily anchored in the taskbar. This allows quick access, but the automatic hiding is also very handy. This makes it easy to find and use applications that are needed frequently for a short time. For example, I use the overlay for work to manage my projects / tasks and I regularly need the application quickly but only for a short time.

The application is optimised for Windows 11 and has a similar behaviour to the normal start menu. The overlay appears when you click on it and disappears automatically when you focus on another window.

Developed with:
- c# (dotnet 7.x)
  - Target Framework -> net7.0-windows
  - Nullable
  - Implicit Usings / Global Usings
- Dependency Injection
- WPF with MVVM
- External DllImport (user32.dll)

<br />

## Multimonitor Support
Based on the mouse position when clicking on the icon, the app determines which monitor is currently active. The overlay is centered and animated on this monitor.

<br />

## Configurable via Settings file
With the configuration file, the application can be configured multiple times in different folders. Multiple instances can be started with different icons and settings.

In a folder there must always be an AppIcon.png file and an AppConfig.json file.

|Property|Description|
|-|-|
|Title|Title when mouse hover over icon|
|Widht|Window width|
|Height|Window height|
|CanClose|If the application can be closed via the taskbar|
|SourceUrl|Url to the web application or website (local also works)|

```json
{
  "Title": "WebSearch",
  "Width": 800,
  "Height": 500,
  "CanClose": true,
  "SourceUrl": "https://google.com"
}
```


<br />

## Demo
![](./doc/HybridOverlay.gif)