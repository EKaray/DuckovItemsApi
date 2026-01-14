using System.ComponentModel.DataAnnotations;

namespace Duckov.Api.Maps.Models;

public class Map
{
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public required string Name { get; set; }
}