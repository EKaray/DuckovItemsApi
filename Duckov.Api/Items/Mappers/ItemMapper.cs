using Duckov.Api.Items.Dtos;
using Duckov.Api.Items.Helpers;
using Duckov.Api.Items.Models;

namespace Duckov.Api.Items.Mappers;

public static class ItemMapper
{
    public static Item Create(CreateItemRequest request)
    {
        return new Item
        {
            Sku = request.Id,
            Name = request.Name,
            CategoryId = request.CategoryId,
            Value = request.Value,
            Weight = request.Weight,
            MaxQuantity = request.MaxQuantity,
            Image = request.Image
        };
    }

    public static ItemSummary Summary(Item item)
    {
        return new ItemSummary
        {
            Id = item.Sku,
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
            Id = item.Sku,
            Name = item.Name,
            Category = item.Category.Name,
            Value = item.Value,
            Weight = item.Weight,
            MaxQuantity = item.MaxQuantity,
            ValuePerSlot = ValueCalculator.CalculateValuePerSlot(item.Value, item.MaxQuantity, item.Weight),
            Image = item.Image,
            Containers = [.. item.Locations.Select(x => x.Container.Name)]
        };
    }
}