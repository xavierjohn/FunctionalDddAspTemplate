namespace BestWeatherForecast.Api._2023_06_06.Controllers
{
    using System;
    using System.Net;
    using Asp.Versioning;
    using BestWeatherForecast.Application.WeatherForcast;
    using BestWeatherForecast.Domain;
    using FunctionalDDD.Asp;
    using Mapster;
    using Mediator;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Weather forecast controller.
    /// </summary>
    [ApiController]
    [ApiVersion("2023-06-06")]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ISender _sender;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="sender"></param>
        public WeatherForecastController(ISender sender) => _sender = sender;

        /// <summary>
        /// Get the weather forecast for Redmond,WA.
        /// </summary>
        /// <param name="cancellationToken">Cancellation Token.</param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Models.WeatherForecast>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async ValueTask<ActionResult<Models.WeatherForecast>> GetRedmond(CancellationToken cancellationToken)
            => await ZipCode.New("98052")
                .Bind(static zipCode => WeatherForecastQuery.New(zipCode))
                .BindAsync(q => _sender.Send(q, cancellationToken))
                .MapAsync(r => r.Adapt<Models.WeatherForecast>())
                .ToOkActionResultAsync(this);

        /// <summary>
        /// Get the weather forecast for the given zip code.
        /// </summary>
        /// <param name="zipCode">Zip code.</param>
        /// <param name="cancellationToken">Cancellation Token.</param>
        /// <returns></returns>
        [HttpGet("{zipCode}")]
        [ProducesResponseType(typeof(IEnumerable<Models.WeatherForecast>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async ValueTask<ActionResult<Models.WeatherForecast>> Get(string zipCode, CancellationToken cancellationToken)
            => await ZipCode.New(zipCode)
                .Bind(static zipCode => WeatherForecastQuery.New(zipCode))
                .BindAsync(q => _sender.Send(q, cancellationToken))
                .MapAsync(r => r.Adapt<Models.WeatherForecast>())
                .ToOkActionResultAsync(this);
    }
}
