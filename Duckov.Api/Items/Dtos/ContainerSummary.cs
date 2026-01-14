namespace Duckov.Api.Items.Dtos;

public class ContainerSummary
{
    public int Id { get; init; }
    public required string Name { get; init; }
    public IReadOnlyList<ItemSummary> Items { get; init; } = [];
    public string? Map { get; init; }
}