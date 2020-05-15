using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Iot.Device.Graphics;
using Iot.Device.Ws28xx;
using System.Device.Spi;
using System.Device.Spi.Drivers;
using System.Drawing;

namespace Storage.API.Services
{
    public class LedService : ILedService
    {
       
        public async Task<bool> TurnOnLed(int id)
        {
            var settings = new SpiConnectionSettings(0, 0)
            {
                ClockFrequency = 2_400_000,
                Mode = SpiMode.Mode0,
                DataBitLength = 8
            };

            var spi = new Windows10SpiDevice(settings);
            var device = new Ws2812b(spi, 75);

            // Display basic colors for 5 sec
            BitmapImage img = device.Image;
            img.Clear();
            img.SetPixel(id-1, 0, Color.Blue);
            device.Update();
            System.Threading.Thread.Sleep(5000);
            return true;
        }

        public async Task<bool> TurnOff(int id)
        {
            var settings = new SpiConnectionSettings(0, 0)
            {
                ClockFrequency = 2_400_000,
                Mode = SpiMode.Mode0,
                DataBitLength = 8
            };

            var spi = new Windows10SpiDevice(settings);
            var device = new Ws2812b(spi, 75);

            // Display basic colors for 5 sec
            BitmapImage image = device.Image;
            image.Clear();
            image.SetPixel(0, 0, Color.Orange);
            image.SetPixel(1, 0, Color.Red);
            image.SetPixel(2, 0, Color.Green);
            image.SetPixel(3, 0, Color.Blue);
            image.SetPixel(4, 0, Color.Yellow);
            image.SetPixel(5, 0, Color.Cyan);
            image.SetPixel(6, 0, Color.Magenta);
            image.SetPixel(7, 0, Color.FromArgb(unchecked((int)0xffff8000)));
            device.Update();
            System.Threading.Thread.Sleep(5000);

            // Chase some blue leds
            for (int i = 0; i < 10; i++)
            {
                image.Clear();
                for (int j = 0; j < 10; j++)
                {
                    image.SetPixel(j, 0, Color.LightBlue);
                    device.Update();
                    System.Threading.Thread.Sleep(10);
                    image.SetPixel(j, 0, Color.Blue);
                    device.Update();
                    System.Threading.Thread.Sleep(25);
                }
            }

            // Color Fade
            int r = 255;
            int g = 0;
            int b = 0;

            while (true)
            {
                if (r > 0 && b == 0)
                {
                    r--;
                    g++;
                }
                if (g > 0 && r == 0)
                {
                    g--;
                    b++;
                }
                if (b > 0 && g == 0)
                {
                    r++;
                    b--;
                }

                image.Clear(Color.FromArgb(r, g, b));
                device.Update();
                System.Threading.Thread.Sleep(10);
            }


            image.Clear();
            device.Update();

            Console.WriteLine("Hello Pi!");

            return true;
        }
    }
}
