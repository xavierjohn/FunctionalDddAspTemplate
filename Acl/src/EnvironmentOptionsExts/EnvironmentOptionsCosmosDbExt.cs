namespace BestWeatherForecast.AntiCorruptionLayer;

public static class EnvironmentOptionsCosmosDbExt
{
    public static string GetCosmosDbNameShared(this EnvironmentOptions settings) => settings.GetResourceNameShared("docdb");

    public static string GetCosmosDbNameSharedUrl(this EnvironmentOptions settings) => $"https://{GetCosmosDbNameShared(settings)}.documents.azure.com:443/";
}
