<a href="#"><img src="../media/620.png" width="128" height="128" align="right"/></a>
# **VK** Monitor Backlight - Windows App

![.NET Core](https://github.com/VK/MonitorBacklight/workflows/.NET%20Core/badge.svg)

A [.NET Core](https://dotnet.microsoft.com/) 3.1 windows application that continuously captures the screen. The data is sent via serial connection to a microcontroller:

![Screenshot](../media/screenshot.png)

## Build
Install .NET Core from [dotnet.microsoft.com/download](https://dotnet.microsoft.com/download)
```
# run to build
dotnet publish -r win-x64 -c Release /p:PublishSingleFile=true /p:PublishTrimmed=true
```

You can also use the latest github developement build: [MonitorBacklight.exe](https://github.com/VK/MonitorBacklight/releases/download/latest_app/MonitorBacklight.exe)