using Duckov.Api.Categories.Dtos;
using Duckov.Api.Categories.Models;
using Duckov.Api.Categories.Repositories;
using Duckov.Api.Items.Mappers;

namespace Duckov.Api.Categories.Services;

public class CategoriesService : ICategoriesService
{
    private readonly ICategoriesRepository _categoriesRepository;

    public CategoriesService(ICategoriesRepository categoriesRepository)
    {
        _categoriesRepository = categoriesRepository;
    }

    public async Task<CategoryWithItems?> GetByIdWithItems(int id)
    {
        var category = await _categoriesRepository.GetByIdWithIncludes(id)
        ?? throw new KeyNotFoundException($"{nameof(Category)} with identifier '{id}' does not exist.");

        var categoryWithItems = new CategoryWithItems
        {
            Id = category.Id,
            Name = category.Name,
            Items = category.Items == null ? [] : [.. category.Items.Select(ItemMapper.Summary)]
        };

        return categoryWithItems;
    }

    public async Task<IReadOnlyCollection<CategorySummary>> GetAll()
    {
        var categories = await _categoriesRepository
            .GetAll();

        var categorySummaries = categories
            .Select(category => new CategorySummary
            {
                Id = category.Id,
                Name = category.Name
            })
            .ToList();

        return categorySummaries;
    }
}