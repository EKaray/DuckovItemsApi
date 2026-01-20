using Duckov.Api.Logins.Services;

namespace Duckov.Api.Logins;

public static class LoginsDepedencyInjection
{
    public static IServiceCollection AddLoginsDependencies(this IServiceCollection services)
    {
        services.AddScoped<ILoginService, LoginService>();
        services.AddScoped<IJwtService, JwtService>();

        return services;
    }
}