namespace BestWeatherForecast.AntiCorruptionLayer;

public static class EnvironmentOptionsCosmosDbExt
{
    public static string GetCosmosDbName(this EnvironmentOptions settings) => settings.GetResourceNameShared("docdb");
}
