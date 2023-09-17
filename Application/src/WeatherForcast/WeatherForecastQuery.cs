namespace BestWeatherForecast.Application.WeatherForcast;
using BestWeatherForecast.Domain;
using FluentValidation;
using FunctionalDDD.FluentValidation;
using Mediator;

public class WeatherForecastQuery : IRequest<Result<WeatherForecast>>
{
    public ZipCode ZipCode { get; }

    public static Result<WeatherForecastQuery> New(ZipCode zipCode)
        => s_validator.ValidateToResult(new WeatherForecastQuery(zipCode));

    private WeatherForecastQuery(ZipCode zipCode) => ZipCode = zipCode;
    private static readonly InlineValidator<WeatherForecastQuery> s_validator = new()
    {
        v => v.RuleFor(x => x.ZipCode).NotNull(),
    };
}
