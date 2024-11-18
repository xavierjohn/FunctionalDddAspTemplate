namespace BestWeatherForecast.AntiCorruptionLayer;

public static class EnvironmentOptionsExt
{
    public static string GetResourceName(this EnvironmentOptions settings, string name) => $"{settings.Environment}-{settings.RegionShortName}-{name}-{settings.ServiceName}".ToLowerInvariant();
    public static string GetResourceNameShared(this EnvironmentOptions settings, string name) => $"{settings.Environment}-{name}-{settings.ServiceName}".ToLowerInvariant();
}
