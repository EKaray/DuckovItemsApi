using Duckov.Api.Items.Repositories;
using Duckov.Api.Items.Services;

namespace Duckov.Api.Items;

public static class ItemsDepedencyInjection
{
    public static IServiceCollection AddItemsDependencies(this IServiceCollection services)
    {
        services.AddScoped<IItemsRepository, ItemsRepository>();
        services.AddScoped<IItemService, ItemService>();

        return services;
    }
}