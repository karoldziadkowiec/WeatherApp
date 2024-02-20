using System.ComponentModel.DataAnnotations;

namespace WeatherApp.Models.Entities
{
    public class SavedWeather
    {
        [Key]
        public int Id { get; set; }
        public int WeatherId { get; set; }
    }
}
