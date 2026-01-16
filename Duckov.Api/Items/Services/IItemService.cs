using Duckov.Api.Items.Dtos;

namespace Duckov.Api.Items.Services;

public interface IItemService
{
    public Task CreateItem(CreateItemRequest item);
    public Task<ItemDetails?> GetByGameId(int id);
    public Task<IReadOnlyList<ItemSummary>> SearchByName(IteamSearchQuery query);
}