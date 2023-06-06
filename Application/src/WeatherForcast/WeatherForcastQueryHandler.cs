namespace BestWeatherForcast.Application.WeatherForcast;
using System.Threading.Tasks;
using BestWeatherForcast.Application.Abstractions;
using BestWeatherForcast.Domain;
using Mediator;

public class WeatherForcastQueryHandler : IRequestHandler<WeatherForcastQuery, Result<WeatherForecast>>
{
    private readonly IWeatherForcastService _weatherForcastService;

    public WeatherForcastQueryHandler(IWeatherForcastService weatherForcastService) => _weatherForcastService = weatherForcastService;

    public async ValueTask<Result<WeatherForecast>> Handle(WeatherForcastQuery request, CancellationToken cancellationToken)
        => await _weatherForcastService.GetWeatherForcast(request.ZipCode);
}
