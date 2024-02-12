namespace WeatherApp.DTOs
{
    public class WeatherDTO
    {
        public string City { get; set; }
        public MainData Main { get; set; }
    }

    public class MainData
    {
        public double Temp { get; set; }
    }
}
