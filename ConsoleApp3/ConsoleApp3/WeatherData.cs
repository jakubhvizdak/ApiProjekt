using Newtonsoft.Json;
using System.Collections.Generic;

namespace WeatherApp
{
    public class WeatherData
    {
        [JsonProperty("name")]
        public string CityName { get; set; }

        [JsonProperty("main")]
        public MainData Main { get; set; }

        [JsonProperty("wind")]
        public WindData Wind { get; set; }

        [JsonProperty("forecast")]
        public List<WeatherData> Forecast { get; set; }
    }
}