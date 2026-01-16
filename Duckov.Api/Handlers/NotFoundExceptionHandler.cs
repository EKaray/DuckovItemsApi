using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Duckov.Api.Handlers;

public class NotFoundExceptionHandler(ILogger<NotFoundExceptionHandler> logger) : IExceptionHandler
{
    private readonly ILogger<NotFoundExceptionHandler> _logger = logger;

    public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception, CancellationToken token)
    {
        if (exception is not KeyNotFoundException && exception is not FileNotFoundException || context.Response.HasStarted)
        {
            return false;
        }

        context.Response.StatusCode = StatusCodes.Status404NotFound;
        await context.Response.WriteAsJsonAsync(new ProblemDetails
        {
            Status = StatusCodes.Status404NotFound,
            Title = "Internal Server Error",
            Detail = exception.Message,
            Instance = context.Request.Path
        }, token);


        return true;
    }
}