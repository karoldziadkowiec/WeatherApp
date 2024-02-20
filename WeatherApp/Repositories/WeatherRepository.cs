using Microsoft.Extensions.Configuration;
using WeatherApp.Database;
using WeatherApp.Models.DTOs;
using WeatherApp.Models.Entities;

namespace WeatherApp.Repositories
{
    public class WeatherRepository : IWeatherRepository
    {
        private readonly AppDbContext _context;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string apiKey;


        public WeatherRepository(AppDbContext context, IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _context = context;
            _httpClientFactory = httpClientFactory;
            apiKey = configuration.GetValue<string>("AppSettings:ApiKey");
        }

        public async Task<WeatherDTO> GetWeather(string city)
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                var response = await client.GetAsync($"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={apiKey}&units=metric");
                response.EnsureSuccessStatusCode();
                var weatherDTO = await response.Content.ReadFromJsonAsync<WeatherDTO>();

                if (weatherDTO != null)
                {
                    await AddWeatherToHistory(weatherDTO, city);
                }

                return weatherDTO;
            }
            catch (Exception ex)
            {
                throw new Exception($"Unable to retrieve weather data: {ex.Message}");
            }
        }

        public async Task AddWeatherToHistory(WeatherDTO weatherDTO, string city)
        {
            try
            {
                var weather = new WeatherData
                {
                    City = weatherDTO.Name,
                    Country = weatherDTO.Sys.Country,
                    Title = weatherDTO.Weather[0].Main,
                    Description = weatherDTO.Weather[0].Description,
                    Icon = weatherDTO.Weather[0].Icon,
                    Temperature = weatherDTO.Main.Temp,
                    Pressure = weatherDTO.Main.Pressure,
                    Humidity = weatherDTO.Main.Humidity,
                    WindSpeed = weatherDTO.Wind.Speed,
                    Sunrise = DateTimeOffset.FromUnixTimeSeconds(weatherDTO.Sys.Sunrise).UtcDateTime,
                    Sunset = DateTimeOffset.FromUnixTimeSeconds(weatherDTO.Sys.Sunset).UtcDateTime,
                    Created = DateTime.UtcNow
                };

                _context.History.Add(weather);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while adding weather: {ex.Message}");
            }
        }
    }
}
