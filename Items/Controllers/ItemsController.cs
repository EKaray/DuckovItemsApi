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
}