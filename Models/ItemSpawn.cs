using System.ComponentModel.DataAnnotations;
using DuckovItemsApi.Models.Enums;

namespace DuckovItemsApi.Models;

public class ItemSpawn
{
    public int Id { get; set; }

    public int ItemId { get; set; }
    [Required]
    public Item Item { get; set; } = null!;

    public int MapId { get; set; }
    [Required]
    public Map Map { get; set; } = null!;

    [Required]
    public SpawnType SpawnType { get; set; } = SpawnType.Container;

    public int? ContainerId { get; set; }
    public Container? Container { get; set; }
}
