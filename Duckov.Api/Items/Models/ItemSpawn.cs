using Duckov.Api.Containers.Models;

namespace Duckov.Api.Items.Models;

public class ItemSpawn
{
    public int Id { get; set; }

    public int ItemId { get; set; }
    public Item Item { get; set; } = null!;

    public int ContainerId { get; set; }

    public Container Container { get; set; } = null!;
}
