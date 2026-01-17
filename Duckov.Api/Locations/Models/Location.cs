using System.ComponentModel.DataAnnotations;

namespace Duckov.Api.Locations.Models;

public class Location
{
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public required string Name { get; set; }
}