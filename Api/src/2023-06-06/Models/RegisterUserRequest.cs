namespace BestWeatherForecast.Api._2023_06_06.Models;

/// <summary>
/// Request model for registering a user.
/// </summary>
/// <param name="FirstName"></param>
/// <param name="LastName"></param>
/// <param name="Email"></param>
/// <param name="Password"></param>
public record RegisterUserRequest(
    string FirstName,
    string LastName,
    string Email,
    string Password
);
