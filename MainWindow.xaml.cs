using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Weather
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public  partial class MainWindow : Window
    {
        private List<WeatherForecast> forecasts = new();
        public MainWindow()
        {
            InitializeComponent();
            ShowWeatherLocation("328266");
            
            //var client = new HttpClient();
            //Console.WriteLine("Here");
            //TheText.Text = client.GetStringAsync("http://google.com").Result;
            
            //this.LocationInput.KeyDown += new KeyEventHandler(this.ComboBox_KeyPress);
        }
        private async void ShowWeatherLocation(string locationID)
        {
           
            SetLocationTitle(locationID);
            forecasts = await WeatherGenerator.GetWeatherData(locationID);

            PopulateTimes(forecasts);
            if (forecasts.Count > 0)
            {
                SetTimeOffset(forecasts.First());
            }
        }

        private async void SetLocationTitle(string locationKey)
        {
            ReturnLocation location = await LocationGenerator.GetLocation(locationKey);
            LocationTitle.Content = $"The weather in {location} is:";

        }

        private void PopulateTimes(List<WeatherForecast> forecasts)
        {
            TimesListBox.Items.Clear();
            foreach(var forecast in forecasts)
            {
                TimesListBox.Items.Add(forecast);
            }
        }

        private void SetTimeOffset(WeatherForecast currentWeather)
        {
            TheWeatherText.Text = $"{currentWeather.IconPhrase}, with a temperature of {currentWeather.Temperature.Value}{currentWeather.Temperature.Unit}.";
            string imageNumber;
            if (currentWeather.WeatherIcon.ToString().Length == 1)
            {
                imageNumber = '0' + currentWeather.WeatherIcon.ToString();
            }
            else
            {
                imageNumber = currentWeather.WeatherIcon.ToString();
            }
            var imageUri = new Uri($"Images/{imageNumber}-s.png", UriKind.Relative);

            WeatherIconSpace.Source = new BitmapImage(imageUri);
        }
       /* private void ComboBox_KeyPress(ComboBox sender, EventArgs e)
        {
            
        }*/

        private async void ComboBox_KeyPress(object sender, KeyEventArgs e)
        {

            string userinput = LocationInput.Text;
            if (LocationInput.Text.Length > 2)

            {
                List<ReturnLocation> locationList = await LocationGenerator.GetLocations(LocationInput.Text);
                LocationInput.ItemsSource = locationList;
                LocationInput.IsDropDownOpen = true;
                //LocationInput.Text = userinput;
            }
        }

        private void LocationInput_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var location =LocationInput.SelectedItem;
            if (location is ReturnLocation)
            {
                ReturnLocation checkedLocation =(ReturnLocation)location;
                ShowWeatherLocation(checkedLocation.Key);
            }
        }

        private void TimesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SetTimeOffset((WeatherForecast)TimesListBox.SelectedValue);
        }
    }
}
