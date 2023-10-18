﻿namespace BestWeatherForecast.Api;

internal static partial class LoggerMessageDefinition
{
    private static readonly Action<ILogger, string, Exception?> ErrorHandlingMiddlewareLogMessageDefinition =
        LoggerMessage.Define<string>(LogLevel.Error, 0, "Exception caught by error handling middle ware. {ExceptionToString}");

    public static void LogErrorHandlingMiddlewareMessage(this ILogger logger, Exception exception)
        => ErrorHandlingMiddlewareLogMessageDefinition(logger, exception.ToString(), exception);
}
