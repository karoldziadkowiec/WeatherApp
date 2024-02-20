using WeatherApp.Models.DTOs;
using WeatherApp.Models.Entities;

namespace WeatherApp.Repositories
{
    public interface IWeatherRepository
    {
        Task<WeatherDTO> GetWeather(string city);
        Task AddWeatherToHistory(WeatherDTO weatherDTO, string city);
    }
}
