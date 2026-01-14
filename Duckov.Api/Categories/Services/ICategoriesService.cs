using Duckov.Api.Categories.Dtos;

namespace Duckov.Api.Categories.Services;

public interface ICategoriesService
{
    public Task<CategoryWithItems?> GetByIdWithItems(int id);
    public Task<IReadOnlyCollection<CategorySummary>> GetCategories();
}