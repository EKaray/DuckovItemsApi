using Duckov.Api.Exceptions;
using Duckov.Api.Items.Dtos;
using Duckov.Api.Items.Mappers;
using Duckov.Api.Items.Models;
using Duckov.Api.Items.Repositories;

namespace Duckov.Api.Items.Services;

public class ItemService : IItemService
{
    private readonly IItemsRepository _itemRepository;
    private readonly ILogger<ItemService> _logger;

    public ItemService(IItemsRepository itemRepository, ILogger<ItemService> logger)
    {
        _itemRepository = itemRepository;
        _logger = logger;
    }

    public async Task Create(CreateItemRequest request)
    {
        if (await _itemRepository.Exists(request.Id))
        {
            throw new AlreadyExistsException(nameof(Item), request.Id);
        }

        var item = ItemMapper.Create(request);
        await _itemRepository.Create(item);
    }

    public async Task Update(int id, UpdateItemRequest request)
    {
        var item = await GetByGameIdWithIncludes(id);

        if (request.Name is not null)
        {
            item.Name = request.Name;
        }

        if (request.CategoryId.HasValue)
        {
            item.CategoryId = request.CategoryId.Value;
        }

        if (request.Value.HasValue)
        {
            item.Value = request.Value.Value;
        }

        if (request.Weight.HasValue)
        {
            item.Weight = request.Weight.Value;
        }

        if (request.MaxQuantity.HasValue)
        {
            item.MaxQuantity = request.MaxQuantity.Value;
        }

        if (request.Image is not null)
        {
            item.Image = request.Image;
        }

        await _itemRepository.Update(item);
    }

    public async Task<ItemDetails?> GetByGameId(int id)
    {
        var item = await GetByGameIdWithIncludes(id);
        return ItemMapper.Details(item);
    }

    public async Task Delete(int id)
    {
        var item = await GetByGameIdWithIncludes(id);
        await _itemRepository.Delete(item);
    }

    public async Task<IReadOnlyList<ItemSummary>> SearchByName(IteamSearchQuery query)
    {
        var skip = (query.PageNumber - 1) * query.PageSize;

        var items = await _itemRepository.SearchWithCategory(query.Name, skip, query.PageSize);
        if (!items.Any())
        {
            if (!string.IsNullOrEmpty(query.Name))
            {
                // Only log non-empty search terms
                _logger.LogInformation("No items found for query: {queryName}", query.Name);
                // TODO: push metric to telemetry system
            }

            return [];
        }

        var itemSummaries = items
            .Select(ItemMapper.Summary)
            .ToList();

        return itemSummaries;
    }

    private async Task<Item> GetByGameIdWithIncludes(int id)
    {
        return await _itemRepository.GetByGameIdWithIncludes(id)
            ?? throw new KeyNotFoundException($"{nameof(Item)} with identifier '{id}' does not exist.");
    }
}