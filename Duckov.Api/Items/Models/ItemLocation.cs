using Duckov.Api.Containers.Models;

namespace Duckov.Api.Items.Models;

public class ItemLocation
{
    public int Id { get; set; }

    public int Sku { get; set; }
    public Item Item { get; set; } = null!;

    public int ContainerId { get; set; }

    public Container Container { get; set; } = null!;
}
