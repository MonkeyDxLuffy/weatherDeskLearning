using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WeatherDeskk.Models;
using WeatherDeskk.Services;
using WeatherDeskk.Tests.Helpers;
using Xunit;

namespace WeatherDeskk.Tests.Services
{
    public class WeatherServiceTests
    {
        private WeatherService CreateService(HttpStatusCode statusCode, string json = "")
        {
            var handler = new MockHttpMessageHandler(req =>
            {
                return new HttpResponseMessage
                {
                    StatusCode = statusCode,
                    Content = new StringContent(json, Encoding.UTF8, "application/json")
                };
            });

            var httpClient = new HttpClient(handler);

            return new WeatherService(httpClient);
        }

        [Fact]
        public async Task GetWeatherAsync_ReturnsWeatherInfo_WhenStatusCodeIs200()
        {
            string json = """
            {
              "name": "Delhi",
              "main": {
                "temp": 30,
                "humidity": 50
              },
              "wind": {
                "speed": 5
              },
              "weather": [
                { "description": "mist" }
              ]
            }
            """;

            var service = CreateService(HttpStatusCode.OK, json);

            WeatherInfo? result = await service.GetWeatherAsync("Delhi");

            Assert.NotNull(result);
            Assert.Equal("Delhi", result!.CityName);
            Assert.Equal(30, result.Temperature);
            Assert.Equal(50, result.Humidity);
            Assert.Equal(5, result.WindSpeed);
            Assert.Equal("mist", result.Description);
        }

        [Fact]
        public async Task GetWeatherAsync_ReturnsNull_WhenCityIsEmpty()
        {
            var service = CreateService(HttpStatusCode.OK);

            WeatherInfo? result = await service.GetWeatherAsync("");

            Assert.Null(result);
        }

        [Fact]
        public async Task GetWeatherAsync_ReturnsNull_WhenStatusCodeIs400()
        {
            var service = CreateService(HttpStatusCode.BadRequest);

            WeatherInfo? result = await service.GetWeatherAsync("Delhi");

            Assert.Null(result);
        }

        [Fact]
        public async Task GetWeatherAsync_ReturnsNull_WhenStatusCodeIs401()
        {
            var service = CreateService(HttpStatusCode.Unauthorized);

            WeatherInfo? result = await service.GetWeatherAsync("Delhi");

            Assert.Null(result);
        }

        [Fact]
        public async Task GetWeatherAsync_ReturnsNull_WhenStatusCodeIs404()
        {
            var service = CreateService(HttpStatusCode.NotFound);

            WeatherInfo? result = await service.GetWeatherAsync("WrongCity");

            Assert.Null(result);
        }

        [Fact]
        public async Task GetWeatherAsync_ReturnsNull_WhenStatusCodeIs500()
        {
            var service = CreateService(HttpStatusCode.InternalServerError);

            WeatherInfo? result = await service.GetWeatherAsync("Delhi");

            Assert.Null(result);
        }

        [Fact]
        public async Task GetWeatherAsync_ReturnsNull_WhenResponseBodyIsEmpty()
        {
            var service = CreateService(HttpStatusCode.OK, "");

            WeatherInfo? result = await service.GetWeatherAsync("Delhi");

            Assert.Null(result);
        }

        [Fact]
        public async Task GetWeatherAsync_ThrowsJsonException_WhenJsonIsInvalid()
        {
            var service = CreateService(HttpStatusCode.OK, "invalid json");

            await Assert.ThrowsAnyAsync<System.Text.Json.JsonException>(() =>
                service.GetWeatherAsync("Delhi"));
        }

        [Fact]
        public async Task GetWeatherAsync_ThrowsKeyNotFoundException_WhenRequiredJsonPropertyIsMissing()
        {
            string json = """
            {
              "name": "Delhi"
            }
            """;

            var service = CreateService(HttpStatusCode.OK, json);

            await Assert.ThrowsAsync<KeyNotFoundException>(() =>
                service.GetWeatherAsync("Delhi"));
        }
    }
}