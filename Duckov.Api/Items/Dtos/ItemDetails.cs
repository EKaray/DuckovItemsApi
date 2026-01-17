namespace Duckov.Api.Items.Dtos;

public class ItemDetails
{
    public int Id { get; init; }
    public required string Name { get; init; }
    public required string Category { get; init; }

    public int Value { get; init; }
    public double Weight { get; init; }
    public int MaxQuantity { get; init; }
    public double ValuePerSlot { get; init; }
    public string? Image { get; init; }

    public IReadOnlyList<string> Containers { get; init; } = [];
}