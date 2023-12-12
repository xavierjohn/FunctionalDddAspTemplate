namespace Api.Tests;

using Xunit.Abstractions;

[Collection(TestWebApplicationFactoryCollectionFixture.Id)]
public class ErrorHandlingMiddlewareTests
{
    private readonly TestWebApplicationFactoryFixture _factory;

    public ErrorHandlingMiddlewareTests(TestWebApplicationFactoryFixture factory, ITestOutputHelper output)
    {
        _factory = factory;
        _factory.OutputHelper = output;
    }

    [Fact]
    public async void Unhandled_exceptions_are_handled_by_middleware()
    {
        // Arrange
        var client = _factory.CreateClient();
        var traceId = "0af7651916cd43dd8448eb211c80319c";
        var traceParent = $"00-{traceId}-00f067aa0ba902b7-01";
        var request = new HttpRequestMessage(HttpMethod.Get, "api/WeatherForecast/Throw?api-version=2023-06-06");
        request.Headers.Add("traceparent", traceParent);

        // Act
        var response = await client.SendAsync(request);

        // Assert
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.InternalServerError);
        var errorResponse = await response.Content.ReadAsExample(new { traceId = default(string), message = default(string) });
        errorResponse.traceId.Should().Be(traceId);
        errorResponse.message.Should().Be("An error occurred in our API. Please refer the trace id with our support team.");
    }

}
