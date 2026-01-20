using Duckov.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace Duckov.Api.Extensions;

public static class DatabaseExtensions
{
    public static IServiceCollection AddDatabase(
        this IServiceCollection services,
        IConfiguration configuration,
        IHostEnvironment environment)
    {
        services.AddDbContext<DuckovDbContext>(options =>
        {
            var connectionString =
                configuration.GetConnectionString("DefaultConnection");

            if (environment.IsDevelopment())
            {
                var folderPath = Path.Combine(environment.ContentRootPath, "App_Data");
                Directory.CreateDirectory(folderPath);
            }

            options.UseSqlite(connectionString);
        });

        return services;
    }
}