using DuckovItemsApi.Categories.Repositories;
using DuckovItemsApi.Categories.Services;

namespace DuckovItemsApi.Categories;

public static class CategoriesDependencyInjection
{
    public static IServiceCollection AddCategoriesDpendencies(this IServiceCollection services)
    {
        services.AddScoped<CategoriesRepository>();
        services.AddScoped<CategoriesService>();

        return services;
    }
}