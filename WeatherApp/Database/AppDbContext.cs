using Microsoft.EntityFrameworkCore;

namespace WeatherApp.Database
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }


    }
}
