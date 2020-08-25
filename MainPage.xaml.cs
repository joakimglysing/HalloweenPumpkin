using System;
using Windows.Devices.Gpio;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace HalloweenPumpkin
{
    public sealed partial class MainPage : Page
    {
        private const int LED_PIN = 5;
        private GpioPin pin;
        private GpioPinValue pinValue;
        private DispatcherTimer timer;

        public MainPage()
        {
            this.InitializeComponent();

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(500);
            timer.Tick += Timer_Tick;

            InitGPIO();

            if (pin != null)
            {
                timer.Start();
            }
        }

        private void InitGPIO()
        {
            var gpio = GpioController.GetDefault();

            if (gpio == null)
            {
                pin = null;
                return;
            }

            pin = gpio.OpenPin(LED_PIN);
            pinValue = GpioPinValue.High;
            pin.Write(pinValue);
            pin.SetDriveMode(GpioPinDriveMode.Output);
        }

        private void Timer_Tick(object sender, object e)
        {
            if (pinValue == GpioPinValue.High)
            {
                for (int i = 0; i < 6; i++)
                {
                    for (int y = 0; y < 7; y++)
                    {
                        pinValue = GpioPinValue.Low;
                        pin.Write(pinValue);
                        System.Threading.Tasks.Task.Delay(350).Wait();

                        pinValue = GpioPinValue.High;
                        pin.Write(pinValue);
                        System.Threading.Tasks.Task.Delay(350).Wait();
                    }

                    for (int x = 0; x < 3; x++)
                    {
                        pinValue = GpioPinValue.Low;
                        pin.Write(pinValue);
                        System.Threading.Tasks.Task.Delay(100).Wait();

                        pinValue = GpioPinValue.High;
                        pin.Write(pinValue);
                        System.Threading.Tasks.Task.Delay(150).Wait();
                    }
                }

                pinValue = GpioPinValue.Low;
                pin.Write(pinValue);
            }
            else
            {
                var initialDelay = 975;
                for (int i = 0; i < 10; i++)
                {
                    var currentDelay = initialDelay - (i * 100);

                    pinValue = GpioPinValue.High;
                    pin.Write(pinValue);
                    System.Threading.Tasks.Task.Delay(currentDelay).Wait();

                    pinValue = GpioPinValue.Low;
                    pin.Write(pinValue);
                    System.Threading.Tasks.Task.Delay(currentDelay).Wait();


                    if (currentDelay > 100 && currentDelay < 300)
                    {
                        for (int j = 0; j < 15; j++)
                        {
                            pinValue = GpioPinValue.High;
                            pin.Write(pinValue);
                            System.Threading.Tasks.Task.Delay(currentDelay).Wait();

                            pinValue = GpioPinValue.Low;
                            pin.Write(pinValue);
                            System.Threading.Tasks.Task.Delay(currentDelay).Wait();
                        }
                    }
                    if (currentDelay < 100)
                    {
                        for (int j = 0; j < 30; j++)
                        {
                            pinValue = GpioPinValue.High;
                            pin.Write(pinValue);
                            System.Threading.Tasks.Task.Delay(currentDelay).Wait();

                            pinValue = GpioPinValue.Low;
                            pin.Write(pinValue);
                            System.Threading.Tasks.Task.Delay(currentDelay).Wait();
                        }
                    }
                }

                pinValue = GpioPinValue.High;
                pin.Write(pinValue);
            }
        }
    }
}
