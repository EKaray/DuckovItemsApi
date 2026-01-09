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
}