using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using WeatherDeskk.Helpers;
using WeatherDeskk.Models;

namespace WeatherDeskk.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly HttpClient httpClient =
            new HttpClient();

        public async Task<WeatherInfo?> GetWeatherAsync(string city)
        {
            if (string.IsNullOrWhiteSpace(city))
                return null;

            string url =
                $"{ApiConstants.BaseUrl}" +
                $"?q={city}" +
                $"&appid={ApiConstants.ApiKey}" +
                $"&units=metric";

            HttpResponseMessage response =
                await httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
                return null;

            string json =
                await response.Content.ReadAsStringAsync();

            using JsonDocument document =
                JsonDocument.Parse(json);

            JsonElement root =
                document.RootElement;

            string cityName =
                root.GetProperty("name").GetString() ?? "";

            double temperature =
                root.GetProperty("main")
                    .GetProperty("temp")
                    .GetDouble();

            int humidity =
                root.GetProperty("main")
                    .GetProperty("humidity")
                    .GetInt32();

            double windSpeed =
                root.GetProperty("wind")
                    .GetProperty("speed")
                    .GetDouble();

            string description =
                root.GetProperty("weather")[0]
                    .GetProperty("description")
                    .GetString() ?? "";

            return new WeatherInfo
            {
                CityName = cityName,
                Temperature = temperature,
                Description = description,
                Humidity = humidity,
                WindSpeed = windSpeed
            };
        }
    }
}