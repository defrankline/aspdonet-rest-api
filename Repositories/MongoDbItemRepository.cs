using MongoDB.Driver;
using SuperHeroAPI.Entities;

namespace SuperHeroAPI.Repositories;

public class MongoDbItemRepository:IItemRepository
{
    private const string DatabaseName = "catalog";
    private const string CollectionName = "items";
    private readonly IMongoCollection<Item> _itemCollection;
    public MongoDbItemRepository(IMongoClient mongoClient)
    {
        IMongoDatabase database = mongoClient.GetDatabase(DatabaseName);
        _itemCollection = database.GetCollection<Item>(CollectionName);
    }
    public Item GetItem(Guid id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Item> GetItems()
    {
        throw new NotImplementedException();
    }

    public void CreateItem(Item item)
    {
        _itemCollection.InsertOne(item);
    }

    public void UpdateItem(Item item)
    {
        throw new NotImplementedException();
    }

    public void DeleteItem(Guid id)
    {
        throw new NotImplementedException();
    }
}