using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WeatherApp.Models.DTOs;
using WeatherApp.Models.Entities;
using WeatherApp.Repositories;

namespace WeatherApp.Controllers
{
    [Route("api/saved")]
    [ApiController]
    public class SavedController : ControllerBase
    {
        private readonly ISavedRepository _savedRepository;

        public SavedController(ISavedRepository savedRepository)
        {
            _savedRepository = savedRepository;
        }

        // GET: /api/saved
        [HttpGet]
        public async Task<IActionResult> GetAllSavedAsync()
        {
            var saved = await _savedRepository.GetAllSaved();
            return Ok(saved);
        }

        // GET: /api/saved/:id 
        [HttpGet("{historyId}")]
        public async Task<IActionResult> GetSavedWeatherAsync(int historyId)
        {
            var weather = await _savedRepository.GetSavedWeather(historyId);

            if (weather == null)
                return NotFound();

            return Ok(weather);
        }

        // POST: /api/saved
        [HttpPost]
        public async Task<IActionResult> SaveWeatherAsync([FromBody] WeatherData weatherData)
        {
            try
            {
                if (weatherData == null)
                {
                    return BadRequest("Invalid weather data.");
                }

                await _savedRepository.SaveWeather(weatherData);
                return Ok("Weather saved successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: /api/saved/:id
        [HttpDelete("{weatherId}")]
        public async Task<IActionResult> RemoveWeatherAsync(int weatherId)
        {
            try
            {
                var weather = await _savedRepository.GetSavedWeather(weatherId);

                if (weather == null)
                    return NotFound();

                await _savedRepository.RemoveWeather(weatherId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: /api/saved/csv
        [HttpGet("csv")]
        public async Task<IActionResult> GetSavedCsvBytesAsync()
        {
            try
            {
                var savedCsvBytes = await _savedRepository.GetSavedCsvBytes();
                return File(savedCsvBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "saved.xlsx");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
