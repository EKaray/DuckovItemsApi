using Duckov.Api.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Duckov.Api.Handlers;

public class AlreadyExistsExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception, CancellationToken token)
    {
        if (exception is not AlreadyExistsException || context.Response.HasStarted)
        {
            return false;
        }

        context.Response.StatusCode = StatusCodes.Status409Conflict;
        await context.Response.WriteAsJsonAsync(new ProblemDetails
        {
            Status = StatusCodes.Status409Conflict,
            Title = "Entity already exists",
            Detail = exception.Message,
            Instance = context.Request.Path
        }, token);


        return true;
    }
}