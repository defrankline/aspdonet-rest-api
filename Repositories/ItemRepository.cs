using SuperHeroAPI.Entities;

namespace SuperHeroAPI.Repositories;

public class ItemRepository:IItemRepository
{
    private readonly List<Item> items = new()
    {
        new() { Id = Guid.NewGuid(), Name = "Potion", Price = 450, CreatedAt = DateTimeOffset.UtcNow },
        new() { Id = Guid.NewGuid(), Name = "Iron Man", Price = 300, CreatedAt = DateTimeOffset.UtcNow },
        new() { Id = Guid.NewGuid(), Name = "Bronze Shield", Price = 600, CreatedAt = DateTimeOffset.UtcNow },
        new() { Id = Guid.NewGuid(), Name = "CK", Price = 1000, CreatedAt = DateTimeOffset.UtcNow },
        new() { Id = Guid.NewGuid(), Name = "Lady", Price = 250, CreatedAt = DateTimeOffset.UtcNow },
        new() { Id = Guid.NewGuid(), Name = "Nivea", Price = 500, CreatedAt = DateTimeOffset.UtcNow }
    };

    public IEnumerable<Item> GetItems()
    {
        return items;
    }

    public Item GetItem(Guid id)
    {
        return items.Where(item => item.Id == id).SingleOrDefault();
    }
}