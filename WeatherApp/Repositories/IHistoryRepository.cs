using WeatherApp.Models.DTOs;
using WeatherApp.Models.Entities;

namespace WeatherApp.Repositories
{
    public interface IHistoryRepository
    {
        Task<IQueryable<History>> GetAllHistory();
        Task<History> GetWeatherFromHistory(int historyId);
        Task<byte[]> GetHistoryCsvBytes();
        Task<IEnumerable<History>> SearchPartial(string searchTerm);
    }
}
