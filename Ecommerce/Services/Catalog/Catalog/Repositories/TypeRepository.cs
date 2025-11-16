using Catalog.Entities;
using MongoDB.Driver;

namespace Catalog.Repositories;

public class TypeRepository : ITypeRepository
{
    private readonly IMongoCollection<ProductType> _types;
    
    public TypeRepository(IConfiguration configuration)
    {
        var client = new MongoDB.Driver.MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
        var db = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));
        _types = db.GetCollection<ProductType>(configuration.GetValue<string>("DatabaseSettings:TypeCollectionName"));
    }
    
    public async Task<IEnumerable<ProductType>> GetAllTypesAsync()
    {
        return await _types.Find(_ => true).ToListAsync();
    }

    public async Task<ProductType> GetTypeByIdAsync(string id)
    {
        return await _types.Find<ProductType>(type => type.Id == id).FirstOrDefaultAsync();
    }
}