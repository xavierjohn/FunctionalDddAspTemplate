namespace Api.Tests._2023_06_06;

using System.Net;
using System.Threading.Tasks;
using FunctionalDDD;

[Collection(TestWebApplicationFactoryCollectionFixture.Id)]
public class WeatherForecastTests
{
    private readonly TestWebApplicationFactoryFixture _factory;

    public WeatherForecastTests(TestWebApplicationFactoryFixture factory) => _factory = factory;

    [Fact]
    public async Task Get_forecast_for_Redmond()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync("/WeatherForecast/?api-version=2023-06-06");

        // Assert
        response.EnsureSuccessStatusCode();
        var forecast = await response.Content.ReadAsAsyncWithAssertion<WeatherForecast>();
        forecast.Should().NotBeNull();
        forecast.zipCode.Should().Be("98052");
        var dailyTemperatures = forecast.dailyTemperatures;
        Assert.NotNull(dailyTemperatures);

        dailyTemperatures.Length.Should().Be(3);
        var day = dailyTemperatures[0];
        day.date.Should().Be(new DateOnly(2023, 6, 6));
        day.temperatureC.Should().Be(10);
        day.temperatureF.Should().BeApproximately(50, 0.001);
        day.summary.Should().Be("Sunny");

        day = dailyTemperatures[1];
        day.date.Should().Be(new DateOnly(2023, 6, 7));
        day.temperatureC.Should().Be(20);
        day.temperatureF.Should().BeApproximately(68.0, 0.001);
        day.summary.Should().Be("Cloudy");

        day = dailyTemperatures[2];
        day.date.Should().Be(new DateOnly(2023, 6, 8));
        day.temperatureC.Should().Be(30);
        day.temperatureF.Should().BeApproximately(86.0, 0.001);
        day.summary.Should().Be("Rainy");
    }

    [Fact]
    public async Task Get_forecast_for_Irvine()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync("/WeatherForecast/75014?api-version=2023-06-06");

        // Assert
        response.EnsureSuccessStatusCode();
        var forecast = await response.Content.ReadAsAsyncWithAssertion<WeatherForecast>();
        forecast.Should().NotBeNull();
        forecast.zipCode.Should().Be("75014");
        var dailyTemperatures = forecast.dailyTemperatures;
        Assert.NotNull(dailyTemperatures);

        dailyTemperatures.Length.Should().Be(3);
        var day = dailyTemperatures[0];
        day.date.Should().Be(new DateOnly(2023, 6, 6));
        day.temperatureC.Should().Be(10);
        day.temperatureF.Should().BeApproximately(50, 0.001);
        day.summary.Should().Be("Sunny");

        day = dailyTemperatures[1];
        day.date.Should().Be(new DateOnly(2023, 6, 7));
        day.temperatureC.Should().Be(20);
        day.temperatureF.Should().BeApproximately(68.0, 0.001);
        day.summary.Should().Be("Cloudy");

        day = dailyTemperatures[2];
        day.date.Should().Be(new DateOnly(2023, 6, 8));
        day.temperatureC.Should().Be(30);
        day.temperatureF.Should().BeApproximately(86.0, 0.001);
        day.summary.Should().Be("Rainy");
    }

    [Fact]
    public async Task Unknown_zip_code_will_return_NotFound()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync("/WeatherForecast/12345?api-version=2023-06-06");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        var message = await response.Content.ReadAsStringAsync();
        message.Should().Be("{\"code\":\"not.found.error\",\"message\":\"No weather forecast found for the zip code.\",\"target\":\"12345\"}");
    }
}
