namespace BestWeatherForcast.Infrastructure;
using BestWeatherForcast.Application.Abstractions;
using Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        => services.AddSingleton<IWeatherForcastService, WeatherForcastService>();
}
