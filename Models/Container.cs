using System.ComponentModel.DataAnnotations;

namespace DuckovItemsApi.Models;

public class Container
{
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public required string Name { get; set; }

    ICollection<ItemSpawn> ItemSpawns { get; set; } = [];
}