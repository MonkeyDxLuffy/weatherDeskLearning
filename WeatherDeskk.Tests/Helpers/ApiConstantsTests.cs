using WeatherDeskk.Helpers;
using Xunit;

namespace WeatherDeskk.Tests.Helpers
{
    public class ApiConstantsTests
    {
        [Fact]
        public void BaseUrl_ShouldNotBeEmpty()
        {
            Assert.False(string.IsNullOrWhiteSpace(ApiConstants.BaseUrl));
        }

        [Fact]
        public void BaseUrl_ShouldContainOpenWeatherEndpoint()
        {
            Assert.Contains("openweathermap.org", ApiConstants.BaseUrl);
        }

        [Fact]
        public void ApiKey_ShouldNotBeEmpty()
        {
            Assert.False(string.IsNullOrWhiteSpace(ApiConstants.ApiKey));
        }
    }
}