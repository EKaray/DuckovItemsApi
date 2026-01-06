using System.ComponentModel.DataAnnotations;

namespace DuckovItemsApi.Models;

public class Category
{
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public required string Name { get; set; }

    public ICollection<Item> Items { get; set; } = [];
}
