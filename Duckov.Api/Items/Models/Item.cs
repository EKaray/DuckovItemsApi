using System.ComponentModel.DataAnnotations;
using Duckov.Api.Categories.Models;
using Microsoft.EntityFrameworkCore;
namespace Duckov.Api.Items.Models;

[Index(nameof(GameId), IsUnique = true)]
public class Item
{
    public int Id { get; set; }

    [Required]
    [Range(1, int.MaxValue)]
    public int GameId { get; set; }

    [Required]
    [MaxLength(100)]
    public required string Name { get; set; }

    [Required]
    public required int CategoryId { get; set; }
    public Category Category { get; set; } = null!;

    [Required]
    [Range(1, int.MaxValue)]
    public required int Value { get; set; }

    [Required]
    [Range(0.1, double.MaxValue)]
    public required double Weight { get; set; }

    [Required]
    [Range(1, int.MaxValue)]
    public required int MaxQuantity { get; set; } = 1;

    public string? Image { get; set; }

    public ICollection<ItemSpawn> Spawns { get; set; } = [];
}
