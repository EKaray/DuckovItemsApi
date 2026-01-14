using System.ComponentModel.DataAnnotations;
using Duckov.Api.Items.Models;
using Duckov.Api.Maps.Models;

namespace Duckov.Api.Containers.Models;

public class Container
{
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public required string Name { get; set; }

    public int? MapId { get; set; }
    public Map? Map { get; set; }

    public ICollection<ItemSpawn> ItemSpawns { get; set; } = [];
}