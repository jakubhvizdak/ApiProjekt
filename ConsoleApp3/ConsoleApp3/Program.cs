using Newtonsoft.Json;
using OpenWeatherMap;
using System;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;

namespace WeatherApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Enter a city:");
                string city = Console.ReadLine();

                Console.WriteLine("Choose API url (aktualne pocasie alebo predpoved):");
                string apiUrlChoice = Console.ReadLine();

                string apiKey = "806a96b8e05f1120fa228411aa01a0e7";
                string apiUrl = "";

                if (apiUrlChoice == "aktualne")
                {
                    apiUrl = $"http://api.openweathermap.org/data/2.5/weather?q={city}&appid={apiKey}&units=metric";
                }
                else if (apiUrlChoice == "predpoved")
                {
                    var geoCoder = new GeoCoder(apiKey);
                    var coordinates = await geoCoder.GetCoordinatesAsync(city);
                    apiUrl = $"http://api.openweathermap.org/data/2.5/forecast?id=524901&appid={apiKey}";
                }
                else
                {
                    Console.WriteLine("Invalid API url choice.");
                    continue;
                }

                using var httpClient = new HttpClient();

                if (apiUrlChoice == "aktualne")
                {
                    var response = await httpClient.GetAsync(apiUrl);
                    response.EnsureSuccessStatusCode();
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    var weatherData = JsonConvert.DeserializeObject<WeatherData>(jsonResponse);
                    Console.WriteLine($"Weather in {weatherData.CityName}:");
                    Console.WriteLine($"Temperature: {weatherData.Main.Temperature}°C");
                    Console.WriteLine($"Humidity: {weatherData.Main.Humidity}%");
                    Console.WriteLine($"Pressure: {weatherData.Main.Pressure} hPa");
                    Console.WriteLine($"Wind direction: {weatherData.Wind.Degree}°");
                }
                else if (apiUrlChoice == "predpoved")
                {
                    var response = await httpClient.GetAsync(apiUrl);
                    response.EnsureSuccessStatusCode();
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    var forecast = JsonConvert.DeserializeObject<Forecast>(jsonResponse);
                    Console.WriteLine($"Weather forecast for {forecast.City.Name}:");

                 foreach (var item in forecast.List)
{
                    DateTime date = item.DateTime;

                    Console.WriteLine($"Date: {date.ToString("dd.MM.yyyy HH:mm")}");
                    Console.WriteLine($"Temperature: {item.Main.Temperature}°C");
                    Console.WriteLine($"Humidity: {item.Main.Humidity}%");
                    Console.WriteLine($"Pressure: {item.Main.Pressure} hPa");
                    Console.WriteLine();
}
                }

                Console.WriteLine("Do you want to continue? (y/n)");
                string input = Console.ReadLine();
                if (input.ToLower() != "y")
                {
                    break;
                }
            }
        }
    }
}