namespace BestWeatherForcast.Api._2023_06_06.Controllers
{
    using BestWeatherForcast.Application.WeatherForcast;
    using BestWeatherForcast.Domain;
    using Mapster;
    using Mediator;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ISender _sender;

        public WeatherForecastController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet(Name = "WeatherForecast/{zipCode?}")]
        public async ValueTask<ActionResult<Models.WeatherForcast>> Get(string? zipCode, CancellationToken cancellationToken)
            => await ZipCode.New(zipCode ?? "98052")
                .Bind(static zipCode => WeatherForcastQuery.New(zipCode))
                .BindAsync(q => _sender.Send(q, cancellationToken))
                .MapAsync(r => r.Adapt<Models.WeatherForcast>())
                .ToOkActionResultAsync(this);
    }
}
