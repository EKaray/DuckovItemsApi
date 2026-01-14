using DuckovItemsApi.Categories.Dtos;

namespace DuckovItemsApi.Categories.Services;

public interface ICategoriesService
{
    public Task<CategoryWithItems?> GetByIdWithItems(int id);
    public Task<IReadOnlyCollection<CategorySummary>> GetCategories();
}