using System.ComponentModel.DataAnnotations;
using Duckov.Api.Items.Validations;

namespace Duckov.Api.Items.Dtos;

public class CreateItemRequest
{
    [Range(1, int.MaxValue)]
    public required int Id { get; init; }
    [ValidName]
    public required string Name { get; init; }
    [Range(1, int.MaxValue)]
    public required int CategoryId { get; init; }

    [Range(1, int.MaxValue)]
    public required int Value { get; init; }
    [Range(0.1, 75)]
    public required double Weight { get; init; }
    [Range(1, 20)]
    public int MaxQuantity { get; init; } = 1;
    public string? Image { get; init; }
}