namespace BestWeatherForcast.Application.Abstractions;

using Domain;

public interface IWeatherForcastService
{
    ValueTask<Result<WeatherForecast>> GetWeatherForcast(ZipCode zipCode);
}
