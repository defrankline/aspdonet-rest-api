using SuperHeroAPI.DTOs;
using SuperHeroAPI.Entities;

namespace SuperHeroAPI.Extensions;

public static class ItemMapper
{
    public static ItemDto asDto(this Item item)
    {
        return new ItemDto
        {
            Id = item.Id,
            Name = item.Name,
            Price = item.Price,
            CreatedAt = item.CreatedAt
        };
    }
}