namespace Duckov.Api.Extensions;

public static class MiddlewareExtensions
{
    public static IApplicationBuilder UseSecurity(
        this IApplicationBuilder app)
    {
        app.UseAuthentication();
        app.UseAuthorization();
        return app;
    }

    public static IApplicationBuilder UseSwaggerIfDevelopment(
        this IApplicationBuilder app)
    {
        var env = app.ApplicationServices
            .GetRequiredService<IHostEnvironment>();

        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        return app;
    }
}