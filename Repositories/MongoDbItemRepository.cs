using MongoDB.Bson;
using MongoDB.Driver;
using SuperHeroAPI.Entities;

namespace SuperHeroAPI.Repositories;

public class MongoDbItemRepository : IItemRepository
{
    private const string DatabaseName = "catalog";
    private const string CollectionName = "items";
    private readonly FilterDefinitionBuilder<Item> _filterDefinitionBuilder = Builders<Item>.Filter;
    private readonly IMongoCollection<Item> _itemCollection;

    public MongoDbItemRepository(IMongoClient mongoClient)
    {
        var database = mongoClient.GetDatabase(DatabaseName);
        _itemCollection = database.GetCollection<Item>(CollectionName);
    }

    public Item GetItem(Guid id)
    {
        var filter = _filterDefinitionBuilder.Eq(item => item.Id, id);
        return _itemCollection.Find(filter).SingleOrDefault();
    }

    public IEnumerable<Item> GetItems()
    {
        return _itemCollection.Find(new BsonDocument()).ToList();
    }

    public void CreateItem(Item item)
    {
        _itemCollection.InsertOne(item);
    }

    public void UpdateItem(Item item)
    {
        var filter = _filterDefinitionBuilder.Eq(row => row.Id, item.Id);
        _itemCollection.ReplaceOne(filter, item);
    }

    public void DeleteItem(Guid id)
    {
        var filter = _filterDefinitionBuilder.Eq(row => row.Id, id);
        _itemCollection.DeleteOne(filter);
    }
}