namespace Duckov.Api.Items.Dtos;

public class ItemSummary
{
    public int GameId { get; init; }
    public required string Name { get; init; }
    public required string Category { get; init; }

    public string? Image { get; init; }
    public double ValuePerSlot { get; init; }
}