using System.Collections.Generic;
using ConsoleApp3;

namespace WeatherApp
{
    public class Forecast
    {
        public City City { get; set; }
        public List<ForecastItem> List { get; set; }
    }

    public class ForecastItem
    {
        public long DateTimeUnix { get; set; }
        public MainData Main { get; set; }

        public DateTime DateTime
        {
            get
            {
                return DateTimeOffset.FromUnixTimeSeconds(DateTimeUnix).LocalDateTime;
            }
        }
    }
}