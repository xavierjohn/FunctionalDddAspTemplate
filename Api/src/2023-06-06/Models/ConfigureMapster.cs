namespace BestWeatherForcast.Api._2023_06_06.Models;

using Mapster;

public class ConfigureMapster
{
    public static void Config()
    {
        TypeAdapterConfig<Domain.WeatherForecast, WeatherForcast>.NewConfig()
            .Map(dest => dest.ZipCode, src => src.Id);
    }
}
