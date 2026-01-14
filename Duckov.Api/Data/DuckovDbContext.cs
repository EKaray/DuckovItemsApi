using DuckovItemsApi.Categories.Models;
using DuckovItemsApi.Containers.Models;
using DuckovItemsApi.Items.Models;
using DuckovItemsApi.Maps.Models;
using Microsoft.EntityFrameworkCore;

namespace DuckovItemsApi.Data;

public class DuckovDbContext : DbContext
{
    public DbSet<Item> Items { get; set; }
    public DbSet<ItemSpawn> ItemSpawns { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Container> Containers { get; set; }
    public DbSet<Map> Maps { get; set; }

    public DuckovDbContext(DbContextOptions options) : base(options)
    {
    }

    protected DuckovDbContext()
    {
    }
}