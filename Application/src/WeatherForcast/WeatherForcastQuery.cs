namespace BestWeatherForcast.Application.WeatherForcast;
using BestWeatherForcast.Domain;
using FluentValidation;
using Mediator;

public class WeatherForcastQuery : IRequest<Result<WeatherForecast>>
{
    public ZipCode ZipCode { get; }

    public static Result<WeatherForcastQuery> New(ZipCode zipCode)
        => s_validator.ValidateToResult(new WeatherForcastQuery(zipCode));

    private WeatherForcastQuery(ZipCode zipCode) => ZipCode = zipCode;
    private static readonly InlineValidator<WeatherForcastQuery> s_validator = new()
    {
        v => v.RuleFor(x => x.ZipCode).NotNull(),
    };
}
