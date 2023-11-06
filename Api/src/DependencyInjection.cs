namespace BestWeatherForecast.Api;

using BestWeatherForecast.Api.Middleware;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using ServiceLevelIndicators;
using Swashbuckle.AspNetCore.SwaggerGen;

internal static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddOpenTelemetryAndSLI();
        services.AddProblemDetails();
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
        services.AddSwaggerGen(
            options =>
            {
                // add a custom operation filter which sets default values
                options.OperationFilter<AddApiVersionMetadata>();
                options.OperationFilter<AddTraceParentParameter>();

                var fileName = typeof(Program).Assembly.GetName().Name + ".xml";
                var filePath = Path.Combine(AppContext.BaseDirectory, fileName);

                // integrate xml comments
                options.IncludeXmlComments(filePath);
            });
        services.AddApiVersioning()
                .AddMvc()
                .AddApiExplorer();
        services.AddScoped<ErrorHandlingMiddleware>();
        _2023_06_06.Models.ConfigureMapster.Config();
        return services;
    }

    private static IServiceCollection AddOpenTelemetryAndSLI(this IServiceCollection services)
    {
        Action<ResourceBuilder> configureResource = r => r.AddService(
            serviceName: "ApiService",
            serviceVersion: typeof(Program).Assembly.GetName().Version?.ToString() ?? "unknown");

        services.AddOpenTelemetry()
            .ConfigureResource(configureResource)
            .WithMetrics(options =>
            {
                options.AddMeter(ApiMeters.MeterName);
                options.AddOtlpExporter();
            });

        services.AddSingleton<ApiMeters>();
        services.TryAddEnumerable(ServiceDescriptor.Singleton<IConfigureOptions<ServiceLevelIndicatorOptions>, ConfigureServiceLevelIndicatorOptions>());
        services.AddServiceLevelIndicator(options =>
        {
            options.CustomerResourceId = "ServiceId";
            options.LocationId = ServiceLevelIndicator.CreateLocationId("public", "West US 3");
        });
        return services;
    }
}
