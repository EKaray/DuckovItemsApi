using Duckov.Api.Items.Models;

namespace Duckov.Api.Items.Repositories;

public interface IItemsRepository
{
    public Task Create(Item item);
    public Task<Item?> GetByGameIdWithIncludes(int id);
    public Task Update(Item item);
    public Task Delete(Item item);
    public Task<bool> Exists(int id);
    public Task<IReadOnlyList<Item>> SearchWithCategory(string? query, int skip, int take);
}