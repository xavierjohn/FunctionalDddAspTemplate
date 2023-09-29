namespace BestWeatherForecast.Domain;
using FluentValidation;
using FunctionalDDD.Domain;

public class ZipCode : ScalarValueObject<string>
{
    private ZipCode(string value) : base(value)
    {
    }

    public static Result<ZipCode> New(string value)
    {
        var zipCode = new ZipCode(value);
        return s_validation.ValidateToResult(zipCode);
    }

    static readonly InlineValidator<ZipCode> s_validation = new()
    {
        v => v.RuleFor(x => x.Value)
        .NotEmpty().OverridePropertyName("zipCode")
        .Matches(@"^\d{5}(?:[-\s]\d{4})?$").OverridePropertyName("zipCode") //US Zip codes.
    };
}
