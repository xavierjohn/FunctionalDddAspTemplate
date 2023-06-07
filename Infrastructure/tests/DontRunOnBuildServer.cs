namespace Infrastructure.Tests;

using Xunit.Categories;

[Category(Constants.FunctionalTestCategory)]
public class DontRunOnBuildServer
{
    [Fact]
    public void This_test_should_be_skipped_on_the_build_server()
    {
        // Arrange
        var isBuildServer = Environment.GetEnvironmentVariable("IS_BUILD_SERVER");

        // Act
        var isSet = !string.IsNullOrEmpty(isBuildServer);

        // Assert
        isSet.Should().BeFalse();
    }
}
