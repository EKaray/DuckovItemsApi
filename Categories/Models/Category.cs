using System.ComponentModel.DataAnnotations;
using DuckovItemsApi.Items.Models;

namespace DuckovItemsApi.Categories.Models;

public class Category
{
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public required string Name { get; set; }

    public ICollection<Item> Items { get; set; } = [];
}
