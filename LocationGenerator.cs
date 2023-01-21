using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;

namespace Weather
{
    public class LocationGenerator
    {
        
        public static string? apiKey { get; set; }
        public LocationGenerator()
        {

        }

        public static async Task<List<ReturnLocation>> GetLocations(string query)
        {
            string uri = $"http://dataservice.accuweather.com/locations/v1/cities/autocomplete?apikey={apiKey}&q={query}";
            var json = await MakeApiCall(uri);
            List<ReturnLocation>? locations = JsonSerializer.Deserialize<List<ReturnLocation>>(json);
            return locations ?? new List<ReturnLocation>();
        }


        public static async Task<ReturnLocation> GetLocation(string locationKey)
        {
            string uri = $"http://dataservice.accuweather.com/locations/v1/{locationKey}?apikey={apiKey}";
            var json = await MakeApiCall(uri);
            ReturnLocation? location = new();
            try
            {
                location = JsonSerializer.Deserialize<ReturnLocation>(json);
            }
            catch(JsonException e)
            {
                location.LocalizedName = e.Message;
                
            }
            return location??new ReturnLocation();
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
