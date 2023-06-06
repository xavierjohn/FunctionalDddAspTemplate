namespace BestWeatherForcast.Api.Controllers
{
    using BestWeatherForcast.Application.WeatherForcast;
    using BestWeatherForcast.Domain;
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
        public async ValueTask<ActionResult<WeatherForecast>> Get(string? zipCode, CancellationToken cancellationToken)
            => await ZipCode.New(zipCode ?? "98052")
                .Bind(static zipCode => WeatherForcastQuery.New(zipCode))
                .BindAsync(q => _sender.Send(q, cancellationToken))
                .ToOkActionResultAsync(this);
    }
}
