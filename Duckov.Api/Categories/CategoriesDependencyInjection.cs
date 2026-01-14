using DuckovItemsApi.Categories.Repositories;
using DuckovItemsApi.Categories.Services;

namespace DuckovItemsApi.Categories;

public static class CategoriesDependencyInjection
{
    public static IServiceCollection AddCategoriesDependencies(this IServiceCollection services)
    {
        services.AddScoped<ICategoriesRepository, CategoriesRepository>();
        services.AddScoped<ICategoriesService, CategoriesService>();

        return services;
    }
}