using Iot.Device.Graphics;
using Iot.Device.Ws28xx;
using System;
using System.Device.Spi;
using System.Device.Spi.Drivers;
using System.Drawing;

namespace LED.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            int a = 0;
            int b = 2;
            int c = 4;
            int d = 8;

            var settings = new SpiConnectionSettings(0, 0)
            {
                ClockFrequency = 2_400_000,
                Mode = SpiMode.Mode0,
                DataBitLength = 8
            };

            var spi = new Windows10SpiDevice(settings);
            var device = new Ws2812b(spi, 10);

            // Display basic colors for 5 sec
            BitmapImage image = device.Image;
            image.Clear();
            image.SetPixel(d, 0, Color.Green);
            device.Update();
            System.Threading.Thread.Sleep(5000);
            Console.WriteLine("Hello Pi!");
        }
    }
}
