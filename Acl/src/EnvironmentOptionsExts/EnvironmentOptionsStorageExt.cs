namespace BestWeatherForecast.AntiCorruptionLayer;

using System.Text.RegularExpressions;

/// <summary>
/// Extension methods for Storage Account EnvironmentOptions.
/// https://learn.microsoft.com/en-us/azure/storage/common/storage-account-overview
/// </summary>
public static partial class EnvironmentOptionsStorageExt
{
    public static string GetStorageNameShared(this EnvironmentOptions settings)
    {
        var name = settings.GetResourceNameShared("stg");
        name = GetOnlyLettersAndNumbers().Replace(name, "");

        if (name.Length < 3 || name.Length > 24)
            throw new ArgumentException("The storage name must be between 3 and 24 characters long.");

        return name;
    }

    public static string GetBlobStorageSharedUrl(this EnvironmentOptions settings) => $"https://{GetStorageNameShared(settings)}.blob.core.windows.net";

    [GeneratedRegex("[^a-z0-9]")]
    public static partial Regex GetOnlyLettersAndNumbers();
}
