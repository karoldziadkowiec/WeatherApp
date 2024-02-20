using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WeatherApp.Models.DTOs;
using WeatherApp.Models.Entities;
using WeatherApp.Repositories;

namespace WeatherApp.Controllers
{
    [Route("api/history")]
    [ApiController]
    public class HistoryController : ControllerBase
    {
        private readonly IHistoryRepository _historyRepository;

        public HistoryController(IHistoryRepository historyRepository)
        {
            _historyRepository = historyRepository;
        }

        // GET: /api/history
        [HttpGet]
        public async Task<IActionResult> GetAllHistoryAsync()
        {
            var history = await _historyRepository.GetAllHistory();
            return Ok(history);
        }

        // GET: /api/history/:id 
        [HttpGet("{historyId}")]
        public async Task<IActionResult> GetWeatherFromHistoryAsync(int historyId)
        {
            var weather = await _historyRepository.GetWeatherFromHistory(historyId);

            if (weather == null)
                return NotFound();

            return Ok(weather);
        }

        // GET: /api/history/csv
        [HttpGet("csv")]
        public async Task<IActionResult> GetHistoryCsvBytesAsync()
        {
            try
            {
                var historyCsvBytes = await _historyRepository.GetHistoryCsvBytes();
                return File(historyCsvBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "history.xlsx");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: /api/history/partial/:searchTerm
        [HttpGet("partial/{searchTerm}")]
        public async Task<IActionResult> SearchPartialWeatherAsync(string searchTerm)
        {
            var searchedWeather = await _historyRepository.SearchPartial(searchTerm);
            return Ok(searchedWeather);
        }
    }
}
