using Duckov.Api.Data;
using Duckov.Api.Items.Models;
using Microsoft.EntityFrameworkCore;

namespace Duckov.Api.Items.Repositories;

public class ItemsRepository : IItemsRepository
{
    private readonly DuckovDbContext _dbContext;

    public ItemsRepository(DuckovDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task CreateItem(Item item)
    {
        _dbContext.Items.Add(item);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Item?> GetByGameIdWithIncludes(int id)
    {
        return await _dbContext.Items
            .AsNoTracking()
            .Include(item => item.Category)
            .Include(item => item.Spawns)
                .ThenInclude(spawn => spawn.Container)
            .FirstOrDefaultAsync(item => item.GameId == id);
    }

    // NOTE: Empty query intentionally returns all items.
    // If search behavior changes (e.g. minimum length),
    // revisit this condition.  
    public async Task<IReadOnlyList<Item>> SearchWithCategory(string? query, int skip, int take)
    {
        query ??= "";
        return await _dbContext.Items
            .AsNoTracking()
            .Include(item => item.Category)
            .Where(item => EF.Functions.Like(item.Name, $"%{query.Trim()}%"))
            .Skip(skip)
            .Take(take)
            .ToListAsync();
    }
}