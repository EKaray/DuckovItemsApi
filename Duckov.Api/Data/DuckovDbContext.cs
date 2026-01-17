using Duckov.Api.Categories.Models;
using Duckov.Api.Containers.Models;
using Duckov.Api.Items.Models;
using Duckov.Api.Locations.Models;
using Microsoft.EntityFrameworkCore;

namespace Duckov.Api.Data;

public class DuckovDbContext : DbContext
{
    public DbSet<Item> Items { get; set; }
    public DbSet<ItemLocation> ItemLocations { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Container> Containers { get; set; }
    public DbSet<Location> Locations { get; set; }

    public DuckovDbContext(DbContextOptions options) : base(options)
    {
    }

    protected DuckovDbContext()
    {
    }
}