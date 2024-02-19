namespace WeatherApp.Models.DTOs
{
    public class WeatherDTO
    {
        public List<Weather> Weather { get; set; }
        public MainData Main { get; set; }
        public Wind Wind { get; set; }
        public int Dt { get; set; }
        public Sys Sys { get; set; }
        public string Name { get; set; }
    }
}
