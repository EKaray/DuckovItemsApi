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

    public Category? GetByIdWithIncludes(int id)
    {
        return _dbContext.Categories
            .AsNoTracking()
            .Include(categories => categories.Items)
            .FirstOrDefault(category => category.Id == id);
    }

    public IReadOnlyCollection<Category> GetCategories()
    {
        var categories = _dbContext.Categories
            .AsNoTracking()
            .ToList();

        return categories;
    }
}