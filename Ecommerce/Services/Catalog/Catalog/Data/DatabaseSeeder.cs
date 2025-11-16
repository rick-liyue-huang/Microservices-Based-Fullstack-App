using System.Text.Json;
using Catalog.Entities;
using MongoDB.Driver;

namespace Catalog.Data;

public class DatabaseSeeder
{
    public static async Task SeedAsync(IConfiguration configuration)
    {
        var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
        var db = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));
        var products = db.GetCollection<Product>(configuration.GetValue<string>("DatabaseSettings:ProductCollectionName"));
        var brands = db.GetCollection<ProductBrand>(configuration.GetValue<string>("DatabaseSettings:BrandCollectionName"));
        var types = db.GetCollection<ProductType>(configuration.GetValue<string>("DatabaseSettings:TypeCollectionName"));
        
        var seedBasePath = Path.Combine("Data", "SeedData");
        
        // Seed Brands
        List<ProductBrand> brandList = new List<ProductBrand>();
        if (await brands.CountDocumentsAsync(_ => true) == 0)
        {
            var brandData = await File.ReadAllTextAsync(Path.Combine(seedBasePath, "brands.json"));
            brandList = JsonSerializer.Deserialize<List<ProductBrand>>(brandData)!;
            await brands.InsertManyAsync(brandList);
        }
        else
        {
            brandList = await brands.Find(_ => true).ToListAsync();
        }
        
        // Seed Types
        List<ProductType> typeList = new();
        if (await types.CountDocumentsAsync(_ => true) == 0)
        {
            var typeData = await File.ReadAllTextAsync(Path.Combine(seedBasePath, "types.json"));
            typeList = JsonSerializer.Deserialize<List<ProductType>>(typeData)!;
            await types.InsertManyAsync(typeList);
        }
        else
        {
            typeList = await types.Find(_ => true).ToListAsync();
        }
        
        // Seed Products
        if (await products.CountDocumentsAsync(_ => true) == 0)
        {
            var productData = await File.ReadAllTextAsync(Path.Combine(seedBasePath, "products.json"));
            var productList = JsonSerializer.Deserialize<List<Product>>(productData)!;

            // Map Brand and Type references
            foreach (var product in productList)
            {
                // Reset id to ensure new id is generated
                product.Id = null;
                if (product.CreatedDate == default)
                {
                    product.CreatedDate = DateTime.UtcNow;
                }
            }
            await products.InsertManyAsync(productList);
        }
    }
}