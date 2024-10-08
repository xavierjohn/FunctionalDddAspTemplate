namespace BestWeatherForecast.AntiCorruptionLayer;

public static class EnvironmentOptionsServiceBusExt
{
    public static string GetServiceBusName(this EnvironmentOptions settings) => settings.GetResourceNameShared("servicebus");

    public static string GetServiceBusNamespace(this EnvironmentOptions settings) => $"{GetServiceBusName(settings)}.servicebus.windows.net";
}
