namespace BestWeatherForecast.AntiCorruptionLayer;

public static class EnvironmentOptionsExt
{
    public static string GetResourceName(this EnvironmentOptions settings, string name) => $"{settings.Environment}-{settings.Region}-{name}-{settings.ServiceName}".ToLower(System.Globalization.CultureInfo.InvariantCulture);
    public static string GetResourceNameShared(this EnvironmentOptions settings, string name) => $"{settings.Environment}-shared-{name}-{settings.ServiceName}".ToLower(System.Globalization.CultureInfo.InvariantCulture);
}
