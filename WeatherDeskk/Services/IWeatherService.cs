using System.Threading.Tasks;
using WeatherDeskk.Models;

namespace WeatherDeskk.Services
{
    public interface IWeatherService
    {
        Task<WeatherInfo?> GetWeatherAsync(string city);
    }
}