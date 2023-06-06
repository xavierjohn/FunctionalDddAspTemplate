namespace BestWeatherForcast.Infrastructure;

using System.Threading.Tasks;
using BestWeatherForcast.Application.Abstractions;
using BestWeatherForcast.Domain;

public class WeatherForcastService : IWeatherForcastService
{
    private static readonly string[] s_summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    public ValueTask<Result<WeatherForecast>> GetWeatherForcast(ZipCode zipCode)
    {
        var dailyTempratures = Enumerable.Range(1, 5).Select(index => new DailyTemperature
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            s_summaries[Random.Shared.Next(s_summaries.Length)]
        )).ToArray();

        return ValueTask.FromResult(zipCode.Value switch
        {
            "98052" => Result.Success(new WeatherForecast(zipCode, dailyTempratures)),
            _ => Result.Failure<WeatherForecast>(Error.NotFound("Zip code is not supported", target: zipCode))
        });
    }
}
