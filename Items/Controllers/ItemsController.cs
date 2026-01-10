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
    /// <param name="id">Id of item/param>
    /// <returns>Single ItemDetails</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<ItemDetails> GetItemById(int id)
    {
        if (id <= 0)
        {
            return BadRequest();
        }

        var item = _itemService.GetItemById(id);
        if (item == null)
        {
            return NotFound();
        }

        return Ok(item);
    }

    /// <summary>
    /// Search items by name with optional paging.
    /// </summary>
    /// <param name="query">Optional search term</param>
    /// <param name="page">Page number, 1-based</param>
    /// <param name="count">Items per page</param>
    /// <returns>List of ItemSummary</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<IReadOnlyList<ItemSummary>> SearchItems(string? query, int page = 1, int count = 10)
    {
        if (page <= 0 || count <= 0)
        {
            return BadRequest();
        }

        var items = _itemService.SearchItems(query, page, count);
        return Ok(items);
    }
}