using DuckovItemsApi.Categories.Dtos;
using DuckovItemsApi.Categories.Services;
using Microsoft.AspNetCore.Mvc;

namespace DuckovItemsApi.Categories.Controllers;

[ApiController]
[Route("Categories")]
public class CategoriesController : ControllerBase
{
    private readonly CategoriesService _categoriesService;

    public CategoriesController(CategoriesService categoriesService)
    {
        _categoriesService = categoriesService;
    }

    /// <summary>
    /// Get category by id with related items.
    /// </summary>
    /// <param name="id">Id of category</param>
    /// <returns>Single CategoryWithItems</returns>
    [HttpGet("{id}/items")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<CategoryWithItems> GetByIdWithItems(int id)
    {
        if (id <= 0)
        {
            return BadRequest();
        }

        var category = _categoriesService.GetByIdWithItems(id);
        if (category == null)
        {
            return NotFound();
        }

        return Ok(category);
    }

    /// <summary>
    /// Name of every existing category.
    /// </summary>
    /// <returns>List of CategorySummary</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<IReadOnlyCollection<CategorySummary>> GetCategories()
    {
        var categories = _categoriesService.GetCategories();
        return Ok(categories);
    }
}