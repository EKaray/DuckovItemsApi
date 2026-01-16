using Duckov.Api.Items.Dtos;
using Duckov.Api.Items.Services;
using Microsoft.AspNetCore.Mvc;

namespace Duckov.Api.Items.Controllers;

[ApiController]
[Route("items")]
public class ItemsController : ControllerBase
{
    public IItemService _itemService;

    public ItemsController(IItemService itemService)
    {
        _itemService = itemService;
    }

    /// <summary>
    /// Creates a new item using the provided item data.
    /// </summary>
    /// <param name="request">
    /// The data required to create the item.
    /// </param>
    /// <returns>
    /// Returns <see cref="StatusCodes.Status201Created"/> if the item was successfully created,
    /// or <see cref="StatusCodes.Status400BadRequest"/> if the request data is invalid.
    /// </returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ItemDetails>> CreateItem([FromQuery] CreateItemRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        await _itemService.CreateItem(request);
        return Created();
    }

    /// <summary>
    /// Retrieves an item by its game identifier.
    /// </summary>
    /// <param name="id">
    /// The game identifier of the item.
    /// </param>
    /// <returns>
    /// Returns the <see cref="ItemDetails"/> item if found.
    /// Returns <see cref="StatusCodes.Status400BadRequest"/> if the identifier is invalid,
    /// or <see cref="StatusCodes.Status404NotFound"/> if the item does not exist.
    /// </returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ItemDetails>> GetById(int id)
    {
        if (id <= 0)
        {
            return BadRequest();
        }

        var item = await _itemService.GetByGameId(id);
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
    public async Task<ActionResult<IReadOnlyList<ItemSummary>>> SearchItems([FromQuery] IteamSearchQuery query)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var items = await _itemService.SearchByName(query);
        return Ok(items);
    }
}