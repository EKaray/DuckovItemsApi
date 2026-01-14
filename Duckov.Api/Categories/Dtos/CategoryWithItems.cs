using Duckov.Api.Items.Dtos;

namespace Duckov.Api.Categories.Dtos;

public class CategoryWithItems
{
    public int Id { get; init; }
    public required string Name { get; init; }
    public IReadOnlyCollection<ItemSummary> Items { get; init; } = [];
}