using DuckovItemsApi.Items.Repositories;
using DuckovItemsApi.Items.Services;

namespace DuckovItemsApi.Items;

public static class ItemsDepedencyInjection
{
    public static IServiceCollection AddItemsDependencies(this IServiceCollection services)
    {
        services.AddScoped<IItemsRepository, ItemsRepository>();
        services.AddScoped<IItemService, ItemService>();

        return services;
    }
}