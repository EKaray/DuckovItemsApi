using System.ComponentModel.DataAnnotations;
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
    /// Returns <see cref="StatusCodes.Status204NoContent"/> if the item was successfully created,
    /// or <see cref="StatusCodes.Status400BadRequest"/> if the request data is invalid,
    /// or <see cref="StatusCodes.Status409Conflict"/> if identifier already exists.
    /// </returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult> Create([FromQuery] CreateItemRequest request)
    {
        // ? If necessary, this could return the newly created item 
        // ? instead of doing a separate get request
        await _itemService.Create(request);
        return NoContent();
    }

    /// <summary>
    /// Updates an item using the provided item data.
    /// </summary>
    /// <param name="Id">
    /// <param name="request">
    /// The data required to update the item.
    /// </param>
    /// <returns>
    /// Returns <see cref="StatusCodes.Status204NoContent"/> if the item was successfully updated,
    /// or <see cref="StatusCodes.Status400BadRequest"/> if the request data is invalid,
    /// or <see cref="StatusCodes.Status404NotFound"/> if the item does not exist.
    /// </returns>
    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Update([Range(1, int.MaxValue)] int id, [FromQuery] UpdateItemRequest request)
    {
        await _itemService.Update(id, request);
        return NoContent();
    }

    /// <summary>
    /// Retrieves an item by its game identifier.
    /// </summary>
    /// <param name="id">
    /// The game identifier of the item.
    /// </param>
    /// <returns>
    /// Returns the <see cref="ItemDetails"/> item if found.
    /// or <see cref="StatusCodes.Status400BadRequest"/> if the identifier is invalid,
    /// or <see cref="StatusCodes.Status404NotFound"/> if the item does not exist.
    /// </returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ItemDetails>> GetById([Range(1, int.MaxValue)] int id)
    {
        var item = await _itemService.GetByGameId(id);
        return Ok(item);
    }

    /// <summary>
    /// Deletes an item using the provided item id.
    /// </summary>
    /// <param name="Id">
    /// The id required to delete the item.
    /// </param>
    /// <returns>
    /// Returns <see cref="StatusCodes.Status204NoContent"/> if the item was successfully deleted,
    /// or <see cref="StatusCodes.Status400BadRequest"/> if the identifier is invalid,
    /// or <see cref="StatusCodes.Status404NotFound"/> if the item does not exist.
    /// </returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete([Range(1, int.MaxValue)] int id)
    {
        await _itemService.Delete(id);
        return NoContent();
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
        var items = await _itemService.SearchByName(query);
        return Ok(items);
    }
}