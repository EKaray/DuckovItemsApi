using System.ComponentModel.DataAnnotations;
using Duckov.Api.Categories.Dtos;
using Duckov.Api.Categories.Services;
using Microsoft.AspNetCore.Mvc;

namespace Duckov.Api.Categories.Controllers;

[ApiController]
[Route("categories")]
public class CategoriesController : ControllerBase
{
    private readonly ICategoriesService _categoriesService;

    public CategoriesController(ICategoriesService categoriesService)
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
    public async Task<ActionResult<CategoryWithItems>> GetByIdWithItems([Range(1, int.MaxValue)] int id)
    {
        var category = await _categoriesService.GetByIdWithItems(id);
        return Ok(category);
    }

    /// <summary>
    /// Name of every existing category.
    /// </summary>
    /// <returns>List of CategorySummary</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyCollection<CategorySummary>>> GetAll()
    {
        var categories = await _categoriesService.GetAll();
        return Ok(categories);
    }
}