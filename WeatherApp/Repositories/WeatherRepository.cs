using WeatherApp.Database;
using WeatherApp.DTOs;

namespace WeatherApp.Repositories
{
    public class WeatherRepository : IWeatherRepository
    {
        private readonly AppDbContext _context;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _apiKey = "4d4289c27ffcad62f57690fde18d279c";

        public WeatherRepository(AppDbContext context, IHttpClientFactory httpClientFactory)
        {
            _context = context;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<WeatherDTO> GetWeather(string city)
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                var response = await client.GetAsync($"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={_apiKey}&units=metric");
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
