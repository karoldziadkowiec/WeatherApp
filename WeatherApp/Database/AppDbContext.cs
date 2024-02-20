using Microsoft.EntityFrameworkCore;
using WeatherApp.Models.Entities;

namespace WeatherApp.Database
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<WeatherData> History { get; set; }
        public DbSet<SavedWeather> Saved { get; set; }
    }
}
