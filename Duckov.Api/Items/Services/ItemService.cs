using Duckov.Api.Items.Dtos;
using Duckov.Api.Items.Models;
using Duckov.Api.Items.Repositories;

namespace Duckov.Api.Items.Services;

public class ItemService : IItemService
{
    private readonly IItemsRepository _itemRepository;
    private readonly ILogger<ItemService> _logger;

    public ItemService(IItemsRepository itemRepository, ILogger<ItemService> logger)
    {
        _itemRepository = itemRepository;
        _logger = logger;
    }

    public async Task<ItemDetails?> GetById(int id)
    {
        var item = await _itemRepository.GetByIdWithIncludes(id);
        if (item == null)
        {
            return null;
        }

        return DetailsMapper(item);
    }

    public async Task<IReadOnlyList<ItemSummary>> SearchByName(IteamSearchQuery query)
    {
        var skip = (query.PageNumber - 1) * query.PageSize;

        var items = await _itemRepository.SearchWithCategory(query.Name, skip, query.PageSize);
        if (!items.Any())
        {
            if (!string.IsNullOrEmpty(query.Name))
            {
                // Only log non-empty search terms
                _logger.LogInformation("No items found for query: {queryName}", query.Name);
                // TODO: push metric to telemetry system
            }

            return [];
        }

        var itemSummaries = items
            .Select(SummaryMapper)
            .ToList();

        return itemSummaries;
    }

    public static ItemSummary SummaryMapper(Item item)
    {
        return new ItemSummary
        {
            Id = item.Id,
            Name = item.Name,
            Category = item.Category.Name,
            ValuePerSlot = CalculateValuePerSlot(item.Value, item.MaxQuantity, item.Weight),
            Image = item.Image
        };
    }

    public static ItemDetails DetailsMapper(Item item)
    {
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

    private static double CalculateValuePerSlot(int value, int maxQuantity, double weight)
    {
        // TODO: Replace these with values from the cookie
        var maxSlots = 10;
        var maxWeight = 45;

        int unitsBySlots = maxSlots * maxQuantity;
        int unitsByWeight = (int)Math.Floor(maxWeight / weight);
        int effectiveUnits = Math.Min(unitsBySlots, unitsByWeight);
        int effectiveSlotsUsed = (int)Math.Ceiling((double)effectiveUnits / maxQuantity);

        double valuePerSlot = effectiveUnits * value / effectiveSlotsUsed;

        return valuePerSlot;
    }
}