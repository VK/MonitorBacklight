<a href="#"><img src="media/620.png" width="128" height="128" align="right"/></a>

# **VK** Monitor Backlight

![.NET Core](https://github.com/VK/MonitorBacklight/workflows/.NET%20Core/badge.svg)

Add some ambient light to your computer monitor with this bad weather project.

## [Sample Videos](https://www.youtube.com/playlist?list=PLOqN181OssLONIdQaQpzb0PZYq7GFpEjc)


| [![Australien](https://img.youtube.com/vi/AJf77A_GO2I/0.jpg)](https://www.youtube.com/watch?v=AJf77A_GO2I "Monitor Backlight - Australien") | [![Ambilight Color Test](https://img.youtube.com/vi/YmT-IW0Tgiw/0.jpg)](https://www.youtube.com/watch?v=YmT-IW0Tgiw "Monitor Backlight - Ambilight Color Test") |
|:-:|:-:|
| [![Fluid Sim Hue Test](https://img.youtube.com/vi/0tqL5rYBxZ8/0.jpg)](https://www.youtube.com/watch?v=0tqL5rYBxZ8 "Monitor Backlight - Fluid Sim Hue Test") | [![Rocket League](https://img.youtube.com/vi/dtazzYlSmI4/0.jpg)](https://www.youtube.com/watch?v=dtazzYlSmI4 "Monitor Backlight - Rocket League") |


## Components
* [**App**](./app): a C# [.NET Core](https://dotnet.microsoft.com/) project to transform the picture on the main screen to some serial output 
* [**Controller**](./controller): a [PlatformIO](https://platformio.org/) project for [Arduinos](https://www.arduino.cc/) to listen for serial input and drive a strip of [WS2812](https://cdn-shop.adafruit.com/datasheets/WS2812.pdf) leds

| App | Controller | LEDs |
|:-:|:-:|:-:|
| [<img src="./media/screenshot.png" alt="App" height="170" />](./media/screenshot.png "App")| [<img src="./media/controller_tn.png" alt="Controller" height="170" />](./media/controller.png "Controller") | [<img src="./media/ledstrip_tn.png" alt="LEDs" height="170" />](./media/ledstrip.png "LEDs") |
| [MonitorBacklight.exe](https://github.com/VK/MonitorBacklight/releases/download/latest_app/MonitorBacklight.exe) | eg. Beetle Atmega32U4 |  |