using Catalog.Entities;
using Catalog.Specifications;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Catalog.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly IMongoCollection<Product> _products;
    private readonly IMongoCollection<ProductBrand> _brands;
    private readonly IMongoCollection<ProductType> _types;
    
    public ProductRepository(IConfiguration configuration)
    {
        var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
        var db = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));
        _products = db.GetCollection<Product>(configuration.GetValue<string>("DatabaseSettings:ProductCollectionName"));
    }
    
    public async Task<IEnumerable<Product>> GetAllProductAsync()
    {
        return await _products.Find(_ => true).ToListAsync();
    }

    public async Task<Pagination<Product>> GetAllProductAsync(CatalogSpecParams specParams)
    {
        var builder = Builders<Product>.Filter;
        var filter = builder.Empty;

        if (!string.IsNullOrEmpty(specParams.Search))
        {
            filter &= builder.Where(p => p.Name.ToLower().Contains(specParams.Search.ToLower()));
        }

        if (!string.IsNullOrEmpty(specParams.BrandId))
        {
            filter &= builder.Eq(p => p.Brand.Id, specParams.BrandId);
        }

        if (!string.IsNullOrEmpty(specParams.TypeId))
        {
            filter &= builder.Eq(p => p.Type.Id, specParams.TypeId);
        }

        var totalItems = await _products.CountDocumentsAsync(filter);

        var data = await ApplyDataFilter(specParams, filter);

        return new Pagination<Product>(
            specParams.PageIndex,
            specParams.PageSize,
            (int) totalItems,
            data);
    }

    private async Task<IReadOnlyCollection<Product>> ApplyDataFilter(CatalogSpecParams specParams, FilterDefinition<Product> filter)
    {
        var sortDeFn = Builders<Product>.Sort.Ascending("Name");
        if (!string.IsNullOrEmpty(specParams.Sort))
        {
            sortDeFn = specParams.Sort switch
            {
                "priceAsc" => Builders<Product>.Sort.Ascending(p => p.Price),
                "priceDesc" => Builders<Product>.Sort.Descending(p => p.Price),
                _ => Builders<Product>.Sort.Ascending(p => p.Name)
            };
            
        }
        return await _products.Find(filter)
            .Sort(sortDeFn).Skip(specParams.PageSize * (specParams.PageIndex - 1))
            .Limit(specParams.PageSize)
            .ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetAllProductByNameAsync(string name)
    {
        var filter = Builders<Product>
            .Filter.Regex(p => p.Name, new BsonRegularExpression($".*{name}", "i"));
        return await _products.Find(filter).ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetAllProductByBrandAsync(string categoryName)
    {
        return await _products
            .Find(p => p.Brand.Name.ToLower() == categoryName.ToLower())
            .ToListAsync();
    }

    public async Task<Product> GetProductByIdAsync(string productId)
    {
        return await _products.Find<Product>(product => product.Id == productId).FirstOrDefaultAsync();
    }

    public async Task<Product> CreateProductAsync(Product product)
    {
        await _products.InsertOneAsync(product);
        return product;
    }

    public async Task<bool> UpdateProductAsync(Product product)
    {
        var updatedProduct = await _products.ReplaceOneAsync(p => p.Id == product.Id, product);
        return updatedProduct.IsAcknowledged && updatedProduct.ModifiedCount > 0;
    }

    public async Task<bool> DeleteProductAsync(string productId)
    {
        var deletedProduct = await _products.DeleteOneAsync(p => p.Id == productId);
        return deletedProduct.IsAcknowledged && deletedProduct.DeletedCount > 0;
    }

    public async Task<ProductBrand> GetBrandByBrandIdAsync(string brandId)
    {
        return await _brands.Find(b => b.Id == brandId).FirstOrDefaultAsync();
    }

    public async Task<ProductType> GetTypeByTypeIdAsync(string typeId)
    {
        return await _types.Find(t => t.Id == typeId).FirstOrDefaultAsync();
    }
}