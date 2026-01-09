using DuckovItemsApi.Items.Dtos;
using DuckovItemsApi.Items.Repositories;

namespace DuckovItemsApi.Items.Services;

public class ItemService
{
    private readonly ItemsRepository _itemRepository;

    public ItemService(ItemsRepository itemRepository)
    {
        _itemRepository = itemRepository;
    }

    public ItemDetails? GetItemById(int id, int maxSlots = 10, int maxWeight = 45)
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
            ValuePerSlot = CalculateValuePerSlot(item.Value, item.MaxQuantity, item.Weight, maxSlots, maxWeight),
            Image = item.Image,
            Containers = [.. item.Spawns.Select(x => x.Container.Name)]
        };
    }

    private static double CalculateValuePerSlot(int value, int maxQuantity, double weight, int maxSlots, int maxWeight)
    {
        int unitsBySlots = maxSlots * maxQuantity;
        int unitsByWeight = (int)Math.Floor(maxWeight / weight);
        int effectiveUnits = Math.Min(unitsBySlots, unitsByWeight);
        int effectiveSlotsUsed = (int)Math.Ceiling((double)effectiveUnits / maxQuantity);

        double effectiveValue = effectiveUnits * value; 

        return effectiveValue / effectiveSlotsUsed;
    }
}