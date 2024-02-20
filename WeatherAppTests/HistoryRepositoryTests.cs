using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Database;
using WeatherApp.Models.Entities;
using WeatherApp.Repositories;

namespace WeatherAppTests
{
    public class HistoryRepositoryTests
    {

        private DbContextOptions<AppDbContext> GetDbContextOptions(string dbName)
        {
            return new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;
        }

        [Fact]
        public async Task GetAllHistory_ReturnsAllWeatherHistory()
        {
            // Arrange
            var options = GetDbContextOptions("GetAllHistory_ReturnsAllWeatherHistory");
            using (var context = new AppDbContext(options))
            {
                context.History.Add(new WeatherData { Id = 1, City = "Los Angeles", Country = "USA", Title = "Sunny", Description = "sunny", Icon = "Icon1", Temperature = 25.5, Pressure = 1013, Humidity = 60, WindSpeed = 5.0, Sunrise = DateTime.Now, Sunset = DateTime.Now.AddHours(6), Created = DateTime.Now });
                context.History.Add(new WeatherData { Id = 2, City = "London", Country = "Great Britain", Title = "Cloudy", Description = "cloudy", Icon = "Icon2", Temperature = 26.5, Pressure = 1014, Humidity = 65, WindSpeed = 6.0, Sunrise = DateTime.Now, Sunset = DateTime.Now.AddHours(7), Created = DateTime.Now });
                await context.SaveChangesAsync();
            }

            using (var context = new AppDbContext(options))
            {
                var repository = new HistoryRepository(context);

                // Act
                var result = await repository.GetAllHistory();

                // Assert
                Assert.Equal(2, result.Count());
            }
        }

        [Fact]
        public async Task GetWeatherFromHistory_ReturnsCorrectWeatherData()
        {
            // Arrange
            var options = GetDbContextOptions("GetWeatherFromHistory_ReturnsCorrectWeatherData");
            using (var context = new AppDbContext(options))
            {
                context.History.Add(new WeatherData { Id = 1, City = "Los Angeles", Country = "USA", Title = "Sunny", Description = "sunny", Icon = "Icon1", Temperature = 25.5, Pressure = 1013, Humidity = 60, WindSpeed = 5.0, Sunrise = DateTime.Now, Sunset = DateTime.Now.AddHours(6), Created = DateTime.Now });
                context.History.Add(new WeatherData { Id = 2, City = "London", Country = "Great Britain", Title = "Cloudy", Description = "cloudy", Icon = "Icon2", Temperature = 26.5, Pressure = 1014, Humidity = 65, WindSpeed = 6.0, Sunrise = DateTime.Now, Sunset = DateTime.Now.AddHours(7), Created = DateTime.Now });
                await context.SaveChangesAsync();
            }

            using (var context = new AppDbContext(options))
            {
                var repository = new HistoryRepository(context);

                // Act
                var result = await repository.GetWeatherFromHistory(2);

                // Assert
                Assert.NotNull(result);
                Assert.Equal(2, result.Id);
            }
        }

        [Fact]
        public async Task GetHistoryCsvBytes_ReturnsCorrectByteArray()
        {
            // Arrange
            var options = GetDbContextOptions("GetHistoryCsvBytes_ReturnsCorrectByteArray");
            using (var context = new AppDbContext(options))
            {
                context.History.Add(new WeatherData { Id = 1, City = "Los Angeles", Country = "USA", Title = "Sunny", Description = "sunny", Icon = "Icon1", Temperature = 25.5, Pressure = 1013, Humidity = 60, WindSpeed = 5.0, Sunrise = DateTime.Now, Sunset = DateTime.Now.AddHours(6), Created = DateTime.Now });
                await context.SaveChangesAsync();
            }

            using (var context = new AppDbContext(options))
            {
                var repository = new HistoryRepository(context);

                // Act
                var result = await repository.GetHistoryCsvBytes();

                // Assert
                Assert.NotNull(result);
                Assert.True(result.Length > 0);
            }
        }

        [Fact]
        public async Task SearchPartial_ReturnsMatchingWeatherData()
        {
            // Arrange
            var options = GetDbContextOptions("SearchPartial_ReturnsMatchingWeatherData");
            using (var context = new AppDbContext(options))
            {
                context.History.Add(new WeatherData { Id = 1, City = "Los Angeles", Country = "USA", Title = "Sunny", Description = "sunny", Icon = "Icon1", Temperature = 25.5, Pressure = 1013, Humidity = 60, WindSpeed = 5.0, Sunrise = DateTime.Now, Sunset = DateTime.Now.AddHours(6), Created = DateTime.Now });
                context.History.Add(new WeatherData { Id = 2, City = "London", Country = "Great Britain", Title = "Cloudy", Description = "cloudy", Icon = "Icon2", Temperature = 26.5, Pressure = 1014, Humidity = 65, WindSpeed = 6.0, Sunrise = DateTime.Now, Sunset = DateTime.Now.AddHours(7), Created = DateTime.Now });
                await context.SaveChangesAsync();
            }

            using (var context = new AppDbContext(options))
            {
                var repository = new HistoryRepository(context);

                // Act
                var result = await repository.SearchPartial("Lo");

                // Assert
                Assert.NotNull(result);
                Assert.Equal(2, result.Count());
            }
        }
    }
}
