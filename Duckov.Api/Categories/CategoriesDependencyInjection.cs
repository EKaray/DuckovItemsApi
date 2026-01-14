using Duckov.Api.Categories.Repositories;
using Duckov.Api.Categories.Services;

namespace Duckov.Api.Categories;

public static class CategoriesDependencyInjection
{
    public static IServiceCollection AddCategoriesDependencies(this IServiceCollection services)
    {
        services.AddScoped<ICategoriesRepository, CategoriesRepository>();
        services.AddScoped<ICategoriesService, CategoriesService>();

        return services;
    }
}