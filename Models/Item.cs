using System.ComponentModel.DataAnnotations;

namespace DuckovItemsApi.Models;

public class Item
{
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public required string Name { get; set; }

    [Required]
    public int CategoryId { get; set; }
    public required Category Category { get; set; }

    [Required]
    [Range(1, int.MaxValue)]
    public int Value { get; set; }

    [Required]
    [Range(1, int.MaxValue)]
    public int Weight { get; set; }

    public int MaxQuantity { get; set; } = 1;

    public string? Image { get; set; }
}
