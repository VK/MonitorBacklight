#include <Arduino.h>
#include <Adafruit_NeoPixel.h>

int pin = 11;
int numPixels = 60;
float target[180];
float current[180];
int pixelFormat = NEO_GRB + NEO_KHZ800;
#define DELAYVAL 5
Adafruit_NeoPixel *pixels;

uint32_t *colors = new uint32_t[1];
int stepcount = 0;

void setup()
{
  Serial.begin(115200);
  Serial.setTimeout(DELAYVAL);

  pixels = new Adafruit_NeoPixel(numPixels, pin, pixelFormat);
  pixels->begin();

  colors[0] = pixels->Color(48, 32, 32);

  for (int i = 0; i < numPixels; i++)
  {
    target[i * 3] = 48;
    target[i * 3 + 1] = 32;
    target[i * 3 + 2] = 32;

    current[i * 3] = 48;
    current[i * 3 + 1] = 32;
    current[i * 3 + 2] = 32;
  }

  pixels->fill(colors[0], 0, numPixels);
  pixels->show();
}

void loop()
{
  char buf[200];
  int len = Serial.readBytes(buf, 200);

  if (len > 0)
  {
    stepcount = 0;
    for (int i = 0; i < numPixels * 3 && i < len; i++)
    {
      target[i] = buf[i];
    }

    Serial.flush();
  }

  for (int i = 0; i < numPixels * 3; i++)
  {
    current[i] += (target[i] > current[i])*0.75;
    current[i] -= (target[i] < current[i])*0.75;
  }

  for (int i = 0; i < numPixels; i++)
  {
    pixels->setPixelColor(i, current[3 * i], current[3 * i + 1], current[3 * i + 2]);
  }

  pixels->show();

  stepcount++;

  if (stepcount % 50 == 0)
  {
    byte t2;
    for (int i = 0; i < numPixels * 3; i++)
    {
      t2 = 32 + (i % 3 == 0) * 16;
      target[i] += (t2 > target[i]);
      target[i] -= (t2 < target[i]);
    }
  }
}