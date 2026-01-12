using DuckovItemsApi.Categories.Models;
using DuckovItemsApi.Data;
using Microsoft.EntityFrameworkCore;

namespace DuckovItemsApi.Categories.Repositories;

public class CategoriesRepository
{
    private readonly DuckovDbContext _dbContext;

    public CategoriesRepository(DuckovDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Category?> GetByIdWithIncludes(int id)
    {
        return await _dbContext.Categories
            .AsNoTracking()
            .Include(categories => categories.Items)
            .FirstOrDefaultAsync(category => category.Id == id);
    }

    public async Task<IReadOnlyCollection<Category>> GetCategories()
    {
        var categories = await _dbContext.Categories
            .AsNoTracking()
            .ToListAsync();

        return categories;
    }
}