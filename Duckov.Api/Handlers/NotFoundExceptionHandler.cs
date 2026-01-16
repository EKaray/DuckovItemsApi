using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Duckov.Api.Handlers;

public class NotFoundExceptionHandler : IExceptionHandler
{


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
            Title = "Not Found",
            Detail = exception.Message,
            Instance = context.Request.Path
        }, token);


        return true;
    }
}