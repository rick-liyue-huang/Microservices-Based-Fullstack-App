using Catalog.Entities;

namespace Catalog.Repositories;

public interface IBrandRepository
{
    Task<IEnumerable<ProductBrand>> GetAllBrandsAsync();
    Task<ProductBrand> GetBrandByIdAsync(string id);
}