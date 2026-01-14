using Duckov.Api.Categories.Models;

namespace Duckov.Api.Categories.Repositories;

public interface ICategoriesRepository
{
    public Task<Category?> GetByIdWithIncludes(int id);
    public Task<IReadOnlyCollection<Category>> GetCategories();
}