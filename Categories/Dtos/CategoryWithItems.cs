using DuckovItemsApi.Items.Dtos;

namespace DuckovItemsApi.Categories.Dtos;

public class CategoryWithItems
{
    public int Id { get; init; }
    public required string Name { get; init; }
    public IReadOnlyCollection<ItemSummary> Items { get; init; } = [];
}