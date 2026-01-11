using DuckovItemsApi.Items.Dtos;
using DuckovItemsApi.Items.Services;
using Microsoft.AspNetCore.Mvc;

namespace DuckovItemsApi.Items.Controllers;

[ApiController]
[Route("items")]
public class ItemsController : ControllerBase
{
    public ItemService _itemService;

    public ItemsController(ItemService itemService)
    {
        _itemService = itemService;
    }

    /// <summary>
    /// Get every information of item by id.
    /// </summary>
    /// <param name="id">Id of item</param>
    /// <returns>Single ItemDetails</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<ItemDetails> GetById(int id)
    {
        if (id <= 0)
        {
            return BadRequest();
        }

        var item = _itemService.GetById(id);
        if (item == null)
        {
            return NotFound();
        }

        return Ok(item);
    }

    /// <summary>
    /// Searches items by name with optional paging.
    /// </summary>
    /// <param name="query">
    /// Query parameters for the search:
    /// - <c>Name</c>: optional search term; can include letters, numbers, spaces, dots (.), dashes (-), and colons (:); max length enforced by validation.
    /// - <c>PageNumber</c>: 1-based page number (default: 1).
    /// - <c>PageSize</c>: number of items per page (default: 20, max: 100).
    /// </param>
    /// <returns>List of <see cref="ItemSummary"/> objects matching the search criteria.</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<IReadOnlyList<ItemSummary>> SearchItems([FromQuery] IteamSearchQuery query)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var items = _itemService.SearchByName(query);
        return Ok(items);
    }
}