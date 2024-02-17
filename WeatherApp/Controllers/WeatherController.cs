using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using WeatherApp.Models.DTOs;
using WeatherApp.Repositories;

namespace WeatherApp.Controllers
{
    [Route("api/weather")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private readonly IWeatherRepository _weatherRepository;

        public WeatherController(IWeatherRepository weatherRepository)
        {
            _weatherRepository = weatherRepository;
        }

        // GET: /api/weather/:city
        [HttpGet("{city}")]
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
    }
}
