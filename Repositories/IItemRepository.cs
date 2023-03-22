using SuperHeroAPI.Entities;

namespace SuperHeroAPI.Repositories;

public interface IItemRepository
{
    Item GetItem(Guid id);
    IEnumerable<Item> GetItems();
}