namespace Domain.Tests;

using BestWeatherForcast.Domain;

public class ZipCodeTests
{
    [Theory]
    [InlineData("123456")]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("ABCDE")]
    public void Cannot_create_invalid_zip(string zip)
    {
        var result = ZipCode.New(zip);

        result.IsFailure.Should().BeTrue();
    }

    [Theory]
    [InlineData("98052")]
    public void Can_create_valid_zip(string zip)
    {
        var result = ZipCode.New(zip);

        result.IsSuccess.Should().BeTrue();
    }
}
