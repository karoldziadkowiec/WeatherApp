using Microsoft.Extensions.Configuration;
using WeatherApp.Database;
using WeatherApp.Models.DTOs;

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
                return await response.Content.ReadFromJsonAsync<WeatherDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Unable to retrieve weather data: {ex.Message}");
            }
        }
    }
}
