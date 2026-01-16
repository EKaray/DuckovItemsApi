using System.ComponentModel.DataAnnotations;
using Duckov.Api.Items.Validations;

namespace Duckov.Api.Items.Dtos;

public class IteamSearchQuery
{
    [ValidName]
    public string? Name { get; set; }

    [Range(1, 20)]
    public int PageSize { get; set; } = 10;

    [Range(1, int.MaxValue)]
    public int PageNumber { get; set; } = 1;
}