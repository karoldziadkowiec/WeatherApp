using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using WeatherApp.Models.DTOs;
using WeatherApp.Repositories;

namespace WeatherApp.Controllers
{
    [Route("api")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private readonly IWeatherRepository _weatherRepository;

        public WeatherController(IWeatherRepository weatherRepository)
        {
            _weatherRepository = weatherRepository;
        }

        // GET: /api/weather/:city
        [HttpGet("/weather/{city}")]
        public async Task<ActionResult<WeatherDTO>> GetWeather(string city)
        {
            try
            {
                var weather = await _weatherRepository.GetWeather(city);
                return Ok(weather);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // POST: /api/history
        [HttpPost("history")]
        public async Task<ActionResult> AddWeatherToHistory([FromBody] WeatherDTO weatherDTO, [FromQuery] string city)
        {
            try
            {
                if (weatherDTO == null)
                {
                    return BadRequest("Invalid weather data.");
                }

                await _weatherRepository.AddWeatherToHistory(weatherDTO, city);
                return Ok("Weather data added to history.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Unable to add weather data to history: {ex.Message}");
            }
        }
    }
}
