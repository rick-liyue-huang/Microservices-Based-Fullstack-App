using Catalog.Entities;
using Catalog.Specifications;

namespace Catalog.Repositories;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllProductAsync();
    Task<Pagination<Product>> GetAllProductAsync(CatalogSpecParams specParams);
    Task<IEnumerable<Product>> GetAllProductByNameAsync(string name);
    Task<IEnumerable<Product>> GetAllProductByBrandAsync(string categoryName);
    Task<Product> GetProductByIdAsync(string productId);
    Task<Product> CreateProductAsync(Product product);
    Task<bool> UpdateProductAsync(Product product);
    Task<bool> DeleteProductAsync(string productId);
    Task<ProductBrand> GetBrandByBrandIdAsync(string brandId);
    Task<ProductType> GetTypeByTypeIdAsync(string typeId);
}