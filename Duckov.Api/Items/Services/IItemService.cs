using Duckov.Api.Items.Dtos;

namespace Duckov.Api.Items.Services;

public interface IItemService
{
    public Task Create(CreateItemRequest request);
    public Task<ItemDetails?> GetByGameId(int id);

    public Task Update(int id, UpdateItemRequest request);
    public Task Delete(int id);
    public Task<IReadOnlyList<ItemSummary>> SearchByName(IteamSearchQuery query);
}