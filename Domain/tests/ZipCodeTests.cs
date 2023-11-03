﻿namespace Domain.Tests;

using BestWeatherForecast.Domain;

public class ZipCodeTests
{
    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void Cannot_create_empty_zip(string zip)
    {
        var result = ZipCode.TryCreate(zip);

        result.IsFailure.Should().BeTrue();
        var error = (ValidationError)result.Error;
        error.Errors[0].Message.Should().Be("'zip Code' must not be empty.");
        error.Errors[0].FieldName.Should().Be("zipCode");
    }

    [Theory]
    [InlineData("123456")]
    [InlineData("1234")]
    [InlineData("ABCDE")]
    public void Cannot_create_invalid_zip(string zip)
    {
        var result = ZipCode.TryCreate(zip);

        result.IsFailure.Should().BeTrue();
        var error = (ValidationError)result.Error;
        error.Errors[0].Message.Should().Be("'zip Code' is not in the correct format.");
        error.Errors[0].FieldName.Should().Be("zipCode");
    }

    [Theory]
    [InlineData("98052")]
    public void Can_create_valid_zip(string zip)
    {
        var result = ZipCode.TryCreate(zip);

        result.IsSuccess.Should().BeTrue();
    }
}
