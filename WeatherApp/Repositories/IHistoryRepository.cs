using WeatherApp.Models.DTOs;
using WeatherApp.Models.Entities;

namespace WeatherApp.Repositories
{
    public interface IHistoryRepository
    {
        Task<IQueryable<WeatherData>> GetAllHistory();
        Task<WeatherData> GetWeatherFromHistory(int historyId);
        Task<byte[]> GetHistoryCsvBytes();
        Task<IEnumerable<WeatherData>> SearchPartial(string searchTerm);
    }
}
