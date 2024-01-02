namespace Application.Tests;

using Microsoft.Extensions.Hosting;
using BestWeatherForecast.Application;
public class Startup
{
    public static void ConfigureHost(IHostBuilder hostBuilder) =>
        hostBuilder
            .ConfigureServices((context, services) =>
            {
                services.AddApplication()
                .AddMockAntiCorruptionLayer();
            });
}
