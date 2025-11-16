using Catalog.Entities;
using MongoDB.Driver;

namespace Catalog.Repositories;

public class BrandRepository : IBrandRepository
{
    private readonly IMongoCollection<ProductBrand> _brands;
    
    public BrandRepository(IConfiguration configuration)
    {
        var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
        var db = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));
        _brands = db.GetCollection<ProductBrand>(configuration.GetValue<string>("DatabaseSettings:BrandCollectionName"));
    }
    
    public async Task<IEnumerable<ProductBrand>> GetAllBrandsAsync()
    {
        return await _brands.Find(_ => true).ToListAsync();
    }

    public async Task<ProductBrand> GetBrandByIdAsync(string id)
    {
        return await _brands.Find<ProductBrand>(brand => brand.Id == id).FirstOrDefaultAsync();
    }
}