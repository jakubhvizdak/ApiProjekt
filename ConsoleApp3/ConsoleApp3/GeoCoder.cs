using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;
using System;

namespace WeatherApp
{
    public class GeoCoder
    {
        private readonly string apiKey;
        private readonly HttpClient httpClient;

        public GeoCoder(string apiKey)
        {
            this.apiKey = apiKey;
            this.httpClient = new HttpClient();
        }

        public async Task<Coordinates> GetCoordinatesAsync(string city)
        {
            var url = $"http://api.openweathermap.org/geo/1.0/direct?q={city}&limit=1&appid={apiKey}";

            var response = await httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var geoData = JsonConvert.DeserializeObject<GeoData[]>(jsonResponse);

            if (geoData.Length == 0)
            {
                throw new Exception($"Unable to find coordinates for city {city}");
            }

            return new Coordinates { Latitude = geoData[0].Lat, Longitude = geoData[0].Lon };
        }
    }

    public class GeoData
    {
        [JsonProperty("lat")]
        public double Lat { get; set; }

        [JsonProperty("lon")]
        public double Lon { get; set; }
    }

    public class Coordinates
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}