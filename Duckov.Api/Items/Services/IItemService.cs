using DuckovItemsApi.Items.Dtos;

namespace DuckovItemsApi.Items.Services;

public interface IItemService
{
    public Task<ItemDetails?> GetById(int id);
    public Task<IReadOnlyList<ItemSummary>> SearchByName(IteamSearchQuery query);
}