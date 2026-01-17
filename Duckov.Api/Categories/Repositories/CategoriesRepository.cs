using Duckov.Api.Categories.Models;
using Duckov.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace Duckov.Api.Categories.Repositories;

public class CategoriesRepository : ICategoriesRepository
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

    public async Task<IReadOnlyCollection<Category>> GetAll()
    {
        var categories = await _dbContext.Categories
            .AsNoTracking()
            .ToListAsync();

        return categories;
    }
}