namespace BestWeatherForcast.Api._2023_06_06.Models;

public class WeatherForcast
{
    public string? ZipCode { get; set; }

    public DailyTemperature[] DailyTemperatures { get; set; } = Array.Empty<DailyTemperature>();
}
