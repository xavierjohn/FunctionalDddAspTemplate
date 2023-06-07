namespace Api.Tests;

using System.Text.Json;
using System.Text.Json.Serialization;

public static class HttpContentExtensions
{
    public static async Task<T> ReadAsAsyncWithAssertion<T>(this HttpContent content)
    {
        var options = new JsonSerializerOptions();
        options.Converters.Add(new JsonStringEnumConverter());

        var str = await content.ReadAsStringAsync();
        str.Should().NotBeNull();

        var t = JsonSerializer.Deserialize<T>(str, options);
        if (t == null)
            throw new InvalidOperationException("Failed to deserialize");

        return t;
    }

    public static Task<T> ReadAsExample<T>(this HttpContent content, T example)
        => content.ReadAsAsyncWithAssertion<T>();
}
