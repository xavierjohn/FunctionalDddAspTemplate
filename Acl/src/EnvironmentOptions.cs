namespace BestWeatherForecast.AntiCorruptionLayer;

public class EnvironmentOptions
{
    public string ServiceName { get; set; } = "BWF";

    public string Region { get; set; } = "local";

    public string Environment { get; set; } = "local";

    public string Cloud { get; set; } = "Public";
}
