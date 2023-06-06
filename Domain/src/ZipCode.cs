namespace BestWeatherForcast.Domain;
using FluentValidation;

public class ZipCode : SimpleValueObject<string>
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
        v => v.RuleFor(x => x.Value).NotNull().Matches(@"^\d{5}(?:[-\s]\d{4})?$") //US Zip codes.
    };
}
