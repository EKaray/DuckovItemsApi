using DuckovItemsApi.Data;
using DuckovItemsApi.Items.Models;
using Microsoft.EntityFrameworkCore;

namespace DuckovItemsApi.Items.Repositories;

public class ItemsRepository
{
    private readonly DuckovDbContext _dbContext;
    private readonly ILogger<ItemsRepository> _logger;

    public ItemsRepository(DuckovDbContext dbContext, ILogger<ItemsRepository> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public Item? GetByIdWithIncludes(int id)
    {
        var item = _dbContext.Items
            .AsNoTracking()
            .Include(item => item.Category)
            .Include(item => item.Spawns)
                .ThenInclude(spawn => spawn.Container)
            .FirstOrDefault(item => item.Id == id);

        if (item == null)
        {
            _logger.LogWarning("Item for {id} not found", id);
            return null;
        }

        return item;
    }

    // NOTE: Empty query intentionally returns all items.
    // If search behavior changes (e.g. minimum length),
    // revisit this condition.  
    public IReadOnlyList<Item> SearchWithCategory(string? query, int skip, int take)
    {
        query ??= "";
        return [.. _dbContext.Items
            .AsNoTracking()
            .Include(item => item.Category)
            .Where(item => EF.Functions.Like(item.Name, $"%{query}%"))
            .Skip(skip)
            .Take(take)];
    }
}