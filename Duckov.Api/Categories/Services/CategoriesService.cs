using DuckovItemsApi.Categories.Dtos;
using DuckovItemsApi.Categories.Repositories;
using DuckovItemsApi.Items.Services;

namespace DuckovItemsApi.Categories.Services;

public class CategoriesService : ICategoriesService
{
    private readonly ICategoriesRepository _categoriesRepository;

    public CategoriesService(ICategoriesRepository categoriesRepository)
    {
        _categoriesRepository = categoriesRepository;
    }

    public async Task<CategoryWithItems?> GetByIdWithItems(int id)
    {
        var category = await _categoriesRepository.GetByIdWithIncludes(id);
        if (category == null)
        {
            return null;
        }

        var categoryWithItems = new CategoryWithItems
        {
            Id = category.Id,
            Name = category.Name,
            Items = category.Items == null ? [] : [.. category.Items.Select(ItemService.SummaryMapper)]
        };

        return categoryWithItems;
    }

    public async Task<IReadOnlyCollection<CategorySummary>> GetCategories()
    {
        var categories = await _categoriesRepository
            .GetCategories();

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