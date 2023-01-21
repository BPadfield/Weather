using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weather
{
    public class WeatherForecast
    {
        public DateTime DateTime { get; set; }// = DateTime.Now;
        public Temperature Temperature { get; set; } = new Temperature();
        public string? IconPhrase { get; set; }
        public int WeatherIcon { get; set; }

        public bool HasPrecipitation { get; set; }
        public bool IsDaylight { get; set; }

        public string? Message { get; set; }
        public override string ToString()
        {
            return DateTime.ToString("HH:mm");
        }

    }

    public class Temperature
    {
        public double Value { get; set; }
        public string? Unit { get; set; }
    }
}
