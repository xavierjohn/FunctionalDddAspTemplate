namespace BestWeatherForecast.Api;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using ServiceLevelIndicators;
using Swashbuckle.AspNetCore.SwaggerGen;

internal static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.ConfigureOpenTelemetry();
        services.ConfigureServiceLevelIndicators();
        services.AddProblemDetails();
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddMvcCore().AddApiExplorer();
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
        _2023_06_06.Models.ConfigureMapster.Config();
        return services;
    }

    private static IServiceCollection ConfigureOpenTelemetry(this IServiceCollection services)
    {
        static void configureResource(ResourceBuilder r) => r.AddService(
            serviceName: "BestWeatherForcastService",
            serviceVersion: typeof(Program).Assembly.GetName().Version?.ToString() ?? "unknown");

        services.AddOpenTelemetry()
            .ConfigureResource(configureResource)
            .WithMetrics(builder =>
            {
                builder.AddAspNetCoreInstrumentation();
                builder.AddMeter(
                    ApiMeters.MeterName,
                    "Microsoft.AspNetCore.Hosting",
                    "Microsoft.AspNetCore.Server.Kestrel",
                    "System.Net.Http");
                builder.AddOtlpExporter();
            })
            .WithTracing(builder =>
            {
                builder.AddAspNetCoreInstrumentation();
                builder.AddOtlpExporter();
            });

        services.AddSingleton<ApiMeters>();
        return services;
    }

    private static IServiceCollection ConfigureServiceLevelIndicators(this IServiceCollection services)
    {
        services.TryAddEnumerable(ServiceDescriptor.Singleton<IConfigureOptions<ServiceLevelIndicatorOptions>, ConfigureServiceLevelIndicatorOptions>());
        services.AddServiceLevelIndicator(options =>
        {
            options.LocationId = ServiceLevelIndicator.CreateLocationId("public", "West US 3");
        })
        .AddMvc()
        .AddApiVersion();

        return services;
    }
}
