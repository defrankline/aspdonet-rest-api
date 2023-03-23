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

    public async Task<Item> GetItemAsync(Guid id)
    {
        var filter = _filterDefinitionBuilder.Eq(item => item.Id, id);
        return  await _itemCollection.Find(filter).SingleOrDefaultAsync();
    }

    public async Task<IEnumerable<Item>> GetItemsAsync()
    {
        return await _itemCollection.Find(new BsonDocument()).ToListAsync();
    }

    public async Task CreateItemAsync(Item item)
    {
        await _itemCollection.InsertOneAsync(item);
    }

    public async Task UpdateItemAsync(Item item)
    {
        var filter = _filterDefinitionBuilder.Eq(row => row.Id, item.Id);
        await _itemCollection.ReplaceOneAsync(filter, item);
    }

    public async Task DeleteItemAsync(Guid id)
    {
        var filter = _filterDefinitionBuilder.Eq(row => row.Id, id);
        await _itemCollection.DeleteOneAsync(filter);
    }
}