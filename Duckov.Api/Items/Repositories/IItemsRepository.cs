using Duckov.Api.Items.Models;

namespace Duckov.Api.Items.Repositories;

public interface IItemsRepository
{
    public Task<Item?> GetByIdWithIncludes(int id);
    public Task<IReadOnlyList<Item>> SearchWithCategory(string? query, int skip, int take);
}