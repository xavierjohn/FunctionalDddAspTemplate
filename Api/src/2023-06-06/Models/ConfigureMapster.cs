namespace BestWeatherForecast.Api._2023_06_06.Models;

using Mapster;

internal class ConfigureMapster
{
    public static void Config()
    {
        TypeAdapterConfig<Domain.WeatherForecast, WeatherForecast>.NewConfig()
            .Map(dest => dest.ZipCode, src => src.Id);
    }
}
