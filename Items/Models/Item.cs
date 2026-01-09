using System.ComponentModel.DataAnnotations;
using DuckovItemsApi.Categories.Models;
namespace DuckovItemsApi.Items.Models;

public class Item
{
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public required string Name { get; set; }

    public int CategoryId { get; set; }
    public Category Category { get; set; } = null!;

    [Required]
    [Range(1, int.MaxValue)]
    public int Value { get; set; }

    [Required]
    [Range(1, int.MaxValue)]
    public double Weight { get; set; }

    public int MaxQuantity { get; set; } = 1;

    public string? Image { get; set; }

    public ICollection<ItemSpawn> Spawns { get; set; } = [];
}
