using Duckov.Api.Items.Dtos;
using Duckov.Api.Items.Helpers;
using Duckov.Api.Items.Models;

namespace Duckov.Api.Items.Mappers;

public static class ItemMapper
{
    public static Item Create(CreateItemRequest item)
    {
        return new Item
        {
            GameId = item.GameId,
            Name = item.Name,
            CategoryId = item.CategoryId,
            Value = item.Value,
            Weight = item.Weight,
            MaxQuantity = item.MaxQuantity,
            Image = item.Image
        };
    }

    public static ItemSummary Summary(Item item)
    {
        return new ItemSummary
        {
            GameId = item.GameId,
            Name = item.Name,
            Category = item.Category.Name,
            ValuePerSlot = ValueCalculator.CalculateValuePerSlot(item.Value, item.MaxQuantity, item.Weight),
            Image = item.Image
        };
    }

    public static ItemDetails Details(Item item)
    {
        return new ItemDetails()
        {
            GameId = item.GameId,
            Name = item.Name,
            Category = item.Category.Name,
            Value = item.Value,
            Weight = item.Weight,
            ValuePerSlot = ValueCalculator.CalculateValuePerSlot(item.Value, item.MaxQuantity, item.Weight),
            Image = item.Image,
            Containers = [.. item.Spawns.Select(x => x.Container.Name)]
        };
    }
}