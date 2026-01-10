using DuckovItemsApi.Items.Dtos;
using DuckovItemsApi.Items.Repositories;

namespace DuckovItemsApi.Items.Services;

public class ItemService
{
    private readonly ItemsRepository _itemRepository;
    private readonly ILogger<ItemService> _logger;

    public ItemService(ItemsRepository itemRepository, ILogger<ItemService> logger)
    {
        _itemRepository = itemRepository;
        _logger = logger;
    }

    public ItemDetails? GetItemById(int id)
    {
        var item = _itemRepository.GetByIdWithIncludes(id);
        if (item == null)
        {
            return null;
        }

        return new ItemDetails()
        {
            Id = item.Id,
            Name = item.Name,
            Category = item.Category.Name,
            Value = item.Value,
            Weight = item.Weight,
            ValuePerSlot = CalculateValuePerSlot(item.Value, item.MaxQuantity, item.Weight),
            Image = item.Image,
            Containers = [.. item.Spawns.Select(x => x.Container.Name)]
        };
    }

    public IReadOnlyList<ItemSummary> SearchItems(string? query, int page, int count)
    {
        count = Math.Min(count, 100); // Prevent huge queries
        var skip = (page - 1) * count;

        var items = _itemRepository.SearchWithCategory(query, skip, count);
        if (!items.Any())
        {
            if (!string.IsNullOrEmpty(query))
            {
                // Only log non-empty search terms
                _logger.LogInformation("No items found for query: {query}", query);
                // TODO: push metric to telemetry system
            }

            return [];
        }

        return [.. items.Select(item => new ItemSummary()
        {
            Id = item.Id,
            Name = item.Name,
            Category = item.Category.Name,
            Image = item.Image,
            ValuePerSlot = CalculateValuePerSlot(item.Value, item.MaxQuantity, item.Weight)
        })];
    }

    private static double CalculateValuePerSlot(int value, int maxQuantity, double weight)
    {
        // TODO: Replace these with values from the cookie
        var maxSlots = 10;
        var maxWeight = 45;

        int unitsBySlots = maxSlots * maxQuantity;
        int unitsByWeight = (int)Math.Floor(maxWeight / weight);
        int effectiveUnits = Math.Min(unitsBySlots, unitsByWeight);
        int effectiveSlotsUsed = (int)Math.Ceiling((double)effectiveUnits / maxQuantity);

        double effectiveValue = effectiveUnits * value;

        return effectiveValue / effectiveSlotsUsed;
    }
}