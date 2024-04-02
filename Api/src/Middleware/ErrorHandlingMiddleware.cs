namespace BestWeatherForecast.Api.Middleware;

using System.Diagnostics;
using Microsoft.AspNetCore.Http;
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

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        _logger.LogErrorHandlingMiddlewareMessage(exception);

        // if you know you're in MVC-land, you can fall back to ProblemDetailsFactory
        if (context.RequestServices.GetService<IProblemDetailsService>() is not { } problem)
        {
            return;
        }

        var ctx = new ProblemDetailsContext()
        {
            HttpContext = context,
            ProblemDetails =
            {
                Status = StatusCodes.Status500InternalServerError,
                Detail = "An error occurred in our API. Please refer the trace id with our support team.",
            }
        };
        var traceId = Activity.Current?.Id ?? context.TraceIdentifier;
        ctx.ProblemDetails.Extensions["traceId"] = traceId;

        await problem.TryWriteAsync(ctx);
    }
}

