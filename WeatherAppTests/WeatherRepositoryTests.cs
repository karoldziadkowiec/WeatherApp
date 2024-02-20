using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Database;
using WeatherApp.Models.DTOs;
using WeatherApp.Repositories;

namespace WeatherAppTests
{
    public class WeatherRepositoryTests
    {
        private DbContextOptions<AppDbContext> GetDbContextOptions(string dbName)
        {
            return new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;
        }

        [Fact]
        public async Task AddWeatherToHistory_AddsWeatherToDatabase()
        {
            // Arrange
            var dbName = Guid.NewGuid().ToString();
            var options = GetDbContextOptions(dbName);
            var weatherDTO = new WeatherDTO
            {
                Name = "London",
                Sys = new Sys { Country = "TestCountry", Sunrise = 1613777949, Sunset = 1613815610 },
                Weather = new List<Weather> { new Weather { Main = "TestWeather", Description = "TestDescription", Icon = "TestIcon" } },
                Main = new MainData { Temp = 30, Pressure = 1000, Humidity = 50 },
                Wind = new Wind { Speed = 4 },
                Dt = 1613777949
            };

            var configuration = new ConfigurationBuilder().Build();

            // Act
            using (var context = new AppDbContext(options))
            {
                var repository = new WeatherRepository(context, null, configuration);
                await repository.AddWeatherToHistory(weatherDTO, "London");
            }

            // Assert
            using (var context = new AppDbContext(options))
            {
                var savedWeather = await context.History.FirstOrDefaultAsync();
                Assert.NotNull(savedWeather);
                Assert.Equal("London", savedWeather.City);
            }
        }
    }
}
