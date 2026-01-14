using System.ComponentModel.DataAnnotations;
using Duckov.Api.Items.Validations;

namespace Duckov.Api.Items.Dtos;

public class IteamSearchQuery
{
    /// <summary>
    /// Search term. Only letters, numbers, spaces, dots (.), dashes (-), and colons (:). Max length specified.
    /// </summary>
    [ValidSearchTerm(50)]
    public string? Name { get; set; }

    [Range(1, 20)]
    public int PageSize { get; set; } = 10;

    [Range(1, int.MaxValue)]
    public int PageNumber { get; set; } = 1;
}