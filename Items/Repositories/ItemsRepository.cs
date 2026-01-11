using DuckovItemsApi.Data;
using DuckovItemsApi.Items.Models;
using Microsoft.EntityFrameworkCore;

namespace DuckovItemsApi.Items.Repositories;

public class ItemsRepository
{
    private readonly DuckovDbContext _dbContext;

    public ItemsRepository(DuckovDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Item? GetByIdWithIncludes(int id)
    {
        return _dbContext.Items
            .AsNoTracking()
            .Include(item => item.Category)
            .Include(item => item.Spawns)
                .ThenInclude(spawn => spawn.Container)
            .FirstOrDefault(item => item.Id == id);
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