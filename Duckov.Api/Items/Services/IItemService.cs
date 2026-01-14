using Duckov.Api.Items.Dtos;

namespace Duckov.Api.Items.Services;

public interface IItemService
{
    public Task<ItemDetails?> GetById(int id);
    public Task<IReadOnlyList<ItemSummary>> SearchByName(IteamSearchQuery query);
}