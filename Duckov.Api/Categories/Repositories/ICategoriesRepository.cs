using DuckovItemsApi.Categories.Models;

namespace DuckovItemsApi.Categories.Repositories;

public interface ICategoriesRepository
{
    public Task<Category?> GetByIdWithIncludes(int id);
    public Task<IReadOnlyCollection<Category>> GetCategories();
}