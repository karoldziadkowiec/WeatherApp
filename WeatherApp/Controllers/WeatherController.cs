using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using WeatherApp.DTOs;
using WeatherApp.Repositories;

namespace WeatherApp.Controllers
{
    [Route("api/weather")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private readonly IWeatherRepository _weatherRepository;
        private readonly IHttpClientFactory _clientFactory;

        public WeatherController(IWeatherRepository weatherRepository, IHttpClientFactory clientFactory)
        {
            _weatherRepository = weatherRepository;
            _clientFactory = clientFactory;
        }

        [HttpGet("{city}")]
        public async Task<IActionResult> GetWeather(string city)
        {
            var client = _clientFactory.CreateClient();
            var apiKey = "4d4289c27ffcad62f57690fde18d279c";
            var apiURL = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={apiKey}&units=metric";

            var response = await client.GetAsync(apiURL);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadFromJsonAsync<WeatherDTO>();
                return Ok(json);
            }
            else
            {
                return BadRequest("Failed to fetch weather data.");
            }
        }
    }
}
