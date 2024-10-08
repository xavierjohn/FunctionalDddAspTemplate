namespace BestWeatherForecast.AntiCorruptionLayer;

public static class EnvironmentOptionsExt
{
    public static string GetResourceName(this EnvironmentOptions settings, string name) => $"{settings.Environment}-{settings.RegionShortName}-{name}-{settings.ServiceName}".ToLower(System.Globalization.CultureInfo.InvariantCulture);
    public static string GetResourceNameShared(this EnvironmentOptions settings, string name) => $"{settings.Environment}-{name}-{settings.ServiceName}".ToLower(System.Globalization.CultureInfo.InvariantCulture);
}
