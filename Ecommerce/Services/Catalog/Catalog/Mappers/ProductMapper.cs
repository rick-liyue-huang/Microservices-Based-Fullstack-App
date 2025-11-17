using Catalog.Entities;
using Catalog.Responses;
using Catalog.Specifications;

namespace Catalog.Mappers;

public static class ProductMapper
{
    public static ProductResponse ToProductResponse(this Product product)
    {
        if (product == null) return null;
        
        return new ProductResponse
        {
            Id = product.Id,
            Name = product.Name,
            Summary = product.Summary,
            Description = product.Description,
            Price = product.Price,
            ImageFile = product.ImageFile,
            Type = product.Type,
            Brand = product.Brand,
        };
    }
    
    public static Pagination<ProductResponse> ToProductResponseList(this Pagination<Product> products)
    {
        var productResponses = products.Data.Select(p => p.ToProductResponse()).ToList();
        return new Pagination<ProductResponse>(products.PageIndex, products.PageSize, products.Count, productResponses);
    }

    public static IEnumerable<ProductResponse> ToProductByBrandResponseList(this IEnumerable<Product> products)
    {
        return products.Select(p => p.ToProductResponse());
    }
}