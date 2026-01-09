using DuckovItemsApi.Items.Repositories;
using DuckovItemsApi.Items.Services;

namespace DuckovItemsApi.Items;

public static class DepedencyInjection
{
    public static IServiceCollection AddItemsDependencies(this IServiceCollection services)
    {
        services.AddScoped<ItemsRepository>();
        services.AddScoped<ItemService>();

        return services;
    }
}