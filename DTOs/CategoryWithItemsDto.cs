namespace DuckovItemsApi.DTOs;

public class CategoryWithItemsDto
{
    public int Id { get; init; }
    public string? Name { get; init; }
    public IReadOnlyCollection<ItemDto> Items { get; init; } = [];
}