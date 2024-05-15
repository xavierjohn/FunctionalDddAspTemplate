namespace BestWeatherForecast.AntiCorruptionLayer;

public static class EnvironmentOptionsKeyVaultExt
{
    public static string GetKeyVaultName(this EnvironmentOptions settings) => settings.GetResourceName("kv");

    public static string GetKeyVaultUri(this EnvironmentOptions settings)
    {
        if (settings.Cloud == "Public")
            return $"https://{settings.GetKeyVaultName()}.vault.azure.net/";

        throw new NotImplementedException("Currently, only public cloud is supported.");
    }
}
