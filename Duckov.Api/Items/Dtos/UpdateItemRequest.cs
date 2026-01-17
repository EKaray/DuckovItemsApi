using System.ComponentModel.DataAnnotations;
using Duckov.Api.Items.Validations;

namespace Duckov.Api.Items.Dtos;

public class UpdateItemRequest
{
    [ValidName]
    public string? Name { get; init; }
    [Range(1, int.MaxValue)]
    public int? CategoryId { get; init; }

    [Range(1, int.MaxValue)]
    public int? Value { get; init; }
    [Range(0.1, 75)]
    public double? Weight { get; init; }
    [Range(1, 20)]
    public int? MaxQuantity { get; init; }
    public string? Image { get; init; }
}