using DuckovItemsApi.Categories.Dtos;
using DuckovItemsApi.Categories.Repositories;
using DuckovItemsApi.Items.Services;

namespace DuckovItemsApi.Categories.Services;

public class CategoriesService
{
    private readonly CategoriesRepository _categoriesRepository;

    public CategoriesService(CategoriesRepository categoriesRepository)
    {
        _categoriesRepository = categoriesRepository;
    }

    public CategoryWithItems? GetByIdWithItems(int id)
    {
        var category = _categoriesRepository.GetByIdWithIncludes(id);
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

    public IReadOnlyCollection<CategorySummary> GetCategories()
    {
        var categories = _categoriesRepository
            .GetCategories()
            .Select(category => new CategorySummary
            {
                Id = category.Id,
                Name = category.Name
            });

        return [.. categories];
    }
}