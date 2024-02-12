using WeatherApp.DTOs;

namespace WeatherApp.Repositories
{
    public interface IWeatherRepository
    {
        Task<WeatherDTO> GetWeather(string city);

    }
}
