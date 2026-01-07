using DuckovItemsApi.Models.Enums;

namespace DuckovItemsApi.DTOs;

public class ItemSpawnDto
{
    public int Id { get; init; }
    public SpawnType SpawnType { get; init; }
    public string? Container { get; init; }
    public string? Map { get; init; }
}