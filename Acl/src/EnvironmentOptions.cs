namespace BestWeatherForecast.AntiCorruptionLayer;

public class EnvironmentOptions
{
    public string ServiceName { get; set; } = "BWF";

    public string Region { get; set; } = "Local";

    public string Environment { get; set; } = "Development";

    public string Cloud { get; set; } = "Public";
}
