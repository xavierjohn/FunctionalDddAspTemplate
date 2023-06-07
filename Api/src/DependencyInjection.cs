namespace BestWeatherForcast.Api;

using Microsoft.Extensions.DependencyInjection;

internal static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        _2023_06_06.Models.ConfigureMapster.Config();
        return services;
    }
}
