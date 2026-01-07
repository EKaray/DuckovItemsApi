namespace DuckovItemsApi.DTOs;

public class ItemDetailsDto
{
    public int Id { get; init; }
    public required string Name { get; init; }
    public required string Category { get; init; }

    public int Value { get; init; }
    public int Weight { get; init; }
    public int MaxQuantity { get; init; } = 1;
    public int ValuePerSlot { get; init; }
    public string? Image { get; init; }

    public IReadOnlyList<ItemSpawnDto> Spawns { get; init; } = [];
}