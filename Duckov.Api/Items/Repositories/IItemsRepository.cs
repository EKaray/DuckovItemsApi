using Duckov.Api.Items.Models;

namespace Duckov.Api.Items.Repositories;

public interface IItemsRepository
{
    public Task CreateItem(Item item);
    public Task<Item?> GetByGameIdWithIncludes(int id);
    public Task UpdateItem(Item item);
    public Task<IReadOnlyList<Item>> SearchWithCategory(string? query, int skip, int take);
}