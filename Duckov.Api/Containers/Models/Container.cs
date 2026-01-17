using System.ComponentModel.DataAnnotations;
using Duckov.Api.Items.Models;
using Duckov.Api.Locations.Models;

namespace Duckov.Api.Containers.Models;

public class Container
{
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public required string Name { get; set; }

    public int? LocationId { get; set; }
    public Location? Location { get; set; }

    public ICollection<ItemLocation> ItemLocations { get; set; } = [];
}