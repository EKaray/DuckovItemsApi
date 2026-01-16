
namespace Duckov.Api.Handlers;

public static class HandlersDepedencyInjection
{
    public static IServiceCollection AddExceptionHandlers(this IServiceCollection services)
    {
        services.AddProblemDetails();

        services.AddExceptionHandler<BadRequestExceptionHandler>();
        services.AddExceptionHandler<UnauthorizedExceptionHandler>();
        services.AddExceptionHandler<NotFoundExceptionHandler>();
        services.AddExceptionHandler<AlreadyExistsExceptionHandler>();
        services.AddExceptionHandler<GlobalExceptionHandler>();

        return services;
    }
}