using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace Weather
{


    public class WeatherGenerator
    {


        public static string? apiKey {get;set; }
        public WeatherGenerator()
        {
          
        }

        public static async Task<List<WeatherForecast>> GetWeatherData(string location)
        {

            List<WeatherForecast>? currentWeather;
            var uri = $"http://dataservice.accuweather.com/forecasts/v1/hourly/12hour/{location}?apikey={apiKey}&metric=true";
            var json = await MakeApiCall(uri);
                
            try
                {
                    currentWeather = JsonSerializer.Deserialize<List<WeatherForecast>>(json);
                }
                
            catch
                {
                    currentWeather = new List<WeatherForecast>();
                }

            return currentWeather ?? new List<WeatherForecast>();

        }

        private static async Task<string> MakeApiCall(string uri)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    var response = await client.GetStringAsync(uri);
                    return response;
                }
                catch (HttpRequestException ex)
                {
                    Console.WriteLine(ex.Message);
                    return "";
                }

            }

        }

    }

}
