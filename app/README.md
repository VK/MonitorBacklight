# **VK** Monitor Backlight - Windows App


A .NET Core 3.1 windows application that continuously captures the screen. The data is sent via serial connection to a microcontroller.

## Build
Install .NET Core from [dotnet.microsoft.com/download](https://dotnet.microsoft.com/download)
```
# run to build
dotnet publish -r win-x64 -c Release /p:PublishSingleFile=true /p:PublishTrimmed=true
```
