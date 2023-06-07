namespace BestWeatherForcast.Api._2023_06_06.Models;

public class DailyTemperature
{
    public DateOnly Date { get; set; }

    public double TemperatureC { get; set; }

    public double TemperatureF { get; set; }

    public string Summary { get; set; } = string.Empty;
}
