namespace BestWeatherForecast.Api.Middleware;

using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

/// <summary>
/// Class Error handeling Middleware.
/// </summary>
internal class ErrorHandlingMiddleware : IMiddleware
{
    private readonly ILogger<ErrorHandlingMiddleware> _logger;

    public ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger) => _logger = logger;

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var problemDetails = new ProblemDetails
        {
            Status = StatusCodes.Status500InternalServerError,
            Title = "An error occurred in our API. Please refer the trace id with our support team.",
            Detail = exception.Message,
            Instance = Activity.Current?.Id,
        };
        _logger.LogErrorHandlingMiddlewareMessage(exception);

        var response = new ObjectResult(problemDetails)
        {
            StatusCode = StatusCodes.Status500InternalServerError,
        };
        var actionContext = new ActionContext() { HttpContext = context };
        return response.ExecuteResultAsync(actionContext);
    }
}

