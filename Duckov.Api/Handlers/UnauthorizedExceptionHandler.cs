using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Duckov.Api.Handlers;

public class UnauthorizedExceptionHandler(ILogger<UnauthorizedExceptionHandler> logger) : IExceptionHandler
{
    private readonly ILogger<UnauthorizedExceptionHandler> _logger = logger;

    public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception, CancellationToken token)
    {
        if (exception is not UnauthorizedAccessException || context.Response.HasStarted)
        {
            return false;
        }

        _logger.LogWarning(exception, "Exception occurred: {Message}", exception.Message);

        context.Response.StatusCode = StatusCodes.Status403Forbidden;
        await context.Response.WriteAsJsonAsync(new ProblemDetails
        {
            Status = StatusCodes.Status403Forbidden,
            Title = "Internal Server Error",
            Detail = "An unexpected error occurred.",
            Instance = context.Request.Path
        }, token);


        return true;
    }
}