using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace UWPWeather
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            ApplicationView.PreferredLaunchViewSize = new Size(432, 725);
            ApplicationView.GetForCurrentView().SetPreferredMinSize(new Size(432, 725));
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;
        }

        [Obsolete]
        private async void Button_Click(object sender, RoutedEventArgs e)
        {

            var position = await LocationManager.GetPosition();

            Root myWeather = await OpenWeatherMapProxy.GetWeather(position.Coordinate.Latitude, position.Coordinate.Longitude);
            //Root myWeather = await OpenWeatherMapProxy.GetWeather(43.7315, -79.7624);

            CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
            TextInfo textInfo = cultureInfo.TextInfo;

            locationText.Text = myWeather.name + ", " + myWeather.sys.country;
            temperatureText.Text = "Temperature ";
            degrees.Text = (Math.Round(myWeather.main.temp)).ToString() + " C";
            skyStatus.Text = textInfo.ToTitleCase(myWeather.weather[0].description);
            //string iconUrl = String.Format("http://openweathermap.org/img/wn/{0}@2x.png", myWeather.weather[0].icon);

            string iconUrl = String.Format("ms-appx:///Assets/Weather/{0}.png", myWeather.weather[0].icon);


            ResultImage.Source = new BitmapImage(new Uri(iconUrl, UriKind.Absolute));
        }

        private void FormName_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if ((this.Width != 432) & (this.Height != 725))
            {
                this.Width = 432;
                this.Height = 725;
            }
        }
    }
}
