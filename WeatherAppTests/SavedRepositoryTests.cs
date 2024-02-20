using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Xunit;
using WeatherApp.Database;
using WeatherApp.Models.Entities;
using WeatherApp.Repositories;

namespace WeatherAppTests
{
    public class SavedRepositoryTests
    {
        private DbContextOptions<AppDbContext> GetDbContextOptions(string dbName)
        {
            return new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;
        }

        [Fact]
        public async Task GetAllSaved_ReturnsAllSavedWeather()
        {
            // Arrange
            var options = GetDbContextOptions("GetAllSaved_ReturnsAllSavedWeather");
            using (var context = new AppDbContext(options))
            {
                context.Saved.Add(new SavedWeather { Id = 1, WeatherId = 1 });
                context.Saved.Add(new SavedWeather { Id = 2, WeatherId = 2 });
                await context.SaveChangesAsync();
            }

            using (var context = new AppDbContext(options))
            {
                var repository = new SavedRepository(context);

                // Act
                var result = await repository.GetAllSaved();

                // Assert
                Assert.Equal(2, result.Count());
            }
        }

        [Fact]
        public async Task GetSavedWeather_ReturnsCorrectSavedWeather()
        {
            // Arrange
            var options = GetDbContextOptions("GetSavedWeather_ReturnsCorrectSavedWeather");
            using (var context = new AppDbContext(options))
            {
                context.Saved.Add(new SavedWeather { Id = 1, WeatherId = 1 });
                context.Saved.Add(new SavedWeather { Id = 2, WeatherId = 2 });
                await context.SaveChangesAsync();
            }

            using (var context = new AppDbContext(options))
            {
                var repository = new SavedRepository(context);

                // Act
                var result = await repository.GetSavedWeather(2);

                // Assert
                Assert.NotNull(result);
                Assert.Equal(2, result.Id);
            }
        }

        [Fact]
        public async Task SaveWeather_AddsWeatherToDatabase()
        {
            // Arrange
            var options = GetDbContextOptions("SaveWeather");

            using (var context = new AppDbContext(options))
            {
                var repository = new SavedRepository(context);
                var weatherData = new WeatherData { Id = 1, City = "Los Angeles", Country = "USA", Title = "Sunny", Description = "sunny", Icon = "Icon1", Temperature = 25.5, Pressure = 1013, Humidity = 60, WindSpeed = 5.0, Sunrise = DateTime.Now, Sunset = DateTime.Now.AddHours(6), Created = DateTime.Now };

                // Act
                await repository.SaveWeather(weatherData);

                // Assert
                var savedWeather = await context.Saved.FirstOrDefaultAsync();
                Assert.NotNull(savedWeather);
                Assert.Equal(weatherData.Id, savedWeather.WeatherId);
            }
        }

        [Fact]
        public async Task RemoveWeather_RemovesWeatherFromDatabase()
        {
            // Arrange
            var options = GetDbContextOptions("RemoveWeather");

            using (var context = new AppDbContext(options))
            {
                var savedWeather = new SavedWeather { Id = 1, WeatherId = 1 };
                context.Saved.Add(savedWeather);
                await context.SaveChangesAsync();

                var repository = new SavedRepository(context);

                // Act
                await repository.RemoveWeather(savedWeather.Id);

                // Assert
                var removedWeather = await context.Saved.FirstOrDefaultAsync();
                Assert.Null(removedWeather);
            }
        }

        [Fact]
        public async Task GetSavedCsvBytes_ReturnsCorrectByteArray()
        {
            // Arrange
            var options = GetDbContextOptions("GetSavedCsvBytes_ReturnsCorrectByteArray");
            using (var context = new AppDbContext(options))
            {
                context.Saved.Add(new SavedWeather { Id = 1, WeatherId = 1 });
                await context.SaveChangesAsync();
            }

            using (var context = new AppDbContext(options))
            {
                var repository = new SavedRepository(context);

                // Act
                var result = await repository.GetSavedCsvBytes();

                // Assert
                Assert.NotNull(result);
                Assert.True(result.Length > 0);
            }
        }
    }
}
