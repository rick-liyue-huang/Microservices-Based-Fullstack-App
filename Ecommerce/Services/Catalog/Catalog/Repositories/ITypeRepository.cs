using Catalog.Entities;

namespace Catalog.Repositories;

public interface ITypeRepository
{
    Task<IEnumerable<ProductType>> GetAllTypesAsync();
    Task<ProductType> GetTypeByIdAsync(string id);
}