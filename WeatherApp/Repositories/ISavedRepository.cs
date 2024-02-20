using WeatherApp.Models.DTOs;
using WeatherApp.Models.Entities;

namespace WeatherApp.Repositories
{
    public interface ISavedRepository
    {
        Task<IQueryable<SavedWeather>> GetAllSaved();
        Task<SavedWeather> GetSavedWeather(int weatherId);
        Task SaveWeather(WeatherData weatherData);
        Task RemoveWeather(int weatherId);
        Task<byte[]> GetSavedCsvBytes();
    }
}
