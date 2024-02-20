using WeatherApp.Models.DTOs;

namespace WeatherApp.Repositories
{
    public interface IWeatherRepository
    {
        Task<WeatherDTO> GetWeather(string city);
        Task AddWeatherToHistory(WeatherDTO weatherDTO, string city);
    }
}
