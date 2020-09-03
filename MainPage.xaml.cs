using System;
using Windows.Devices.Gpio;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace HalloweenPumpkin
{
    public sealed partial class MainPage : Page
    {
        private const int LED_PIN_WHITE = 5;
        private const int LED_PIN_AMBER = 12;
        private const int LED_PIN_RED_1 = 20;
        private const int LED_PIN_RED_2 = 21;
        private GpioPin pin_white;
        private GpioPin pin_amber;
        private GpioPin pin_red_1;
        private GpioPin pin_red_2;
        private GpioPinValue pinValue;
        private DispatcherTimer timer;

        public MainPage()
        {
            this.InitializeComponent();

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(2000);
            timer.Tick += Timer_Tick;

            InitGPIO();

            if (pin_white != null &&
                pin_amber != null &&
                pin_red_1 != null &&
                pin_red_2 != null)
            {
                timer.Start();
            }
        }

        private void InitGPIO()
        {
            var gpio = GpioController.GetDefault();

            if (gpio == null)
            {
                pin_white = null;
                pin_amber = null;
                pin_red_1 = null;
                pin_red_2 = null;
                return;
            }

            pin_white = gpio.OpenPin(LED_PIN_WHITE);
            pin_amber = gpio.OpenPin(LED_PIN_AMBER);
            pin_red_1 = gpio.OpenPin(LED_PIN_RED_1);
            pin_red_2 = gpio.OpenPin(LED_PIN_RED_2);
            pinValue = GpioPinValue.High;
            pin_white.Write(pinValue);
            pin_white.SetDriveMode(GpioPinDriveMode.Output);
            pin_amber.Write(pinValue);
            pin_amber.SetDriveMode(GpioPinDriveMode.Output);
            pin_red_1.Write(pinValue);
            pin_red_1.SetDriveMode(GpioPinDriveMode.Output);
            pin_red_2.Write(pinValue);
            pin_red_2.SetDriveMode(GpioPinDriveMode.Output);
        }

        private void SetAllLeds(GpioPinValue value, int delayMilliseconds)
        {
            pin_white.Write(value);
            pin_amber.Write(value);
            pin_red_1.Write(value);
            pin_red_2.Write(value);
            System.Threading.Tasks.Task.Delay(delayMilliseconds).Wait();
        }

        private void Timer_Tick(object sender, object e)
        {
            if (pinValue == GpioPinValue.High)
            {
                for (int i = 0; i < 7; i++)
                {
                    for (int y = 0; y < 10; y++)
                    {
                        pinValue = GpioPinValue.Low;
                        SetAllLeds(pinValue, 400);

                        pinValue = GpioPinValue.High;
                        SetAllLeds(pinValue, 400);
                    }

                    for (int x = 0; x < 5; x++)
                    {
                        pinValue = GpioPinValue.Low;
                        SetAllLeds(pinValue, 100);

                        pinValue = GpioPinValue.High;
                        SetAllLeds(pinValue, 150);
                    }
                }

                pinValue = GpioPinValue.Low;
                SetAllLeds(pinValue, 0);
            }
            else
            {
                for (int i = 0; i < 3; i++)
                {
                    var initialDelay = 975;
                    for (int y = 0; y < 10; y++)
                    {
                        var currentDelay = initialDelay - (y * 100);

                        pinValue = GpioPinValue.High;
                        SetAllLeds(pinValue, currentDelay);

                        pinValue = GpioPinValue.Low;
                        SetAllLeds(pinValue, currentDelay);


                        if (currentDelay > 100 && currentDelay < 300)
                        {
                            for (int j = 0; j < 15; j++)
                            {
                                pinValue = GpioPinValue.High;
                                SetAllLeds(pinValue, currentDelay);

                                pinValue = GpioPinValue.Low;
                                SetAllLeds(pinValue, currentDelay);
                            }
                        }
                        if (currentDelay < 100)
                        {
                            for (int j = 0; j < 30; j++)
                            {
                                pinValue = GpioPinValue.High;
                                SetAllLeds(pinValue, currentDelay);

                                pinValue = GpioPinValue.Low;
                                SetAllLeds(pinValue, currentDelay);
                            }
                        }
                    }
                }

                pinValue = GpioPinValue.High;
                SetAllLeds(pinValue, 0);
            }
        }
    }
}
