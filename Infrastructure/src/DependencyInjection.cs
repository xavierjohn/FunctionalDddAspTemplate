namespace BestWeatherForecast.Infrastructure;
using BestWeatherForecast.Application.Abstractions;
using Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        => services.AddSingleton<IWeatherForecastService, WeatherForcastService>();
}
