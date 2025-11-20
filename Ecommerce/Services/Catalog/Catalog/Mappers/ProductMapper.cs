using Catalog.Commands;
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

    public static IEnumerable<ProductResponse> ToProductResponseList(this IEnumerable<Product> products)
    {
        return products.Select(p => p.ToProductResponse());
    }
    
    public static Product ToProductEntity(this CreateProductCommand command, ProductBrand brand, ProductType type)
    {
        return new Product
        {
            Name = command.Name,
            Summary = command.Summary,
            Description = command.Description,
            Price = command.Price,
            ImageFile = command.ImageFile,
            Brand = brand,
            Type = type,
            CreatedDate = DateTimeOffset.Now
        };
    }
    
    public static Product ToUpdateProductEntity(this UpdateProductCommand command, Product product, ProductBrand brand, ProductType type)
    {
        return new Product
        {
            Id = product.Id,
            Name = command.Name,
            Summary = command.Summary,
            Description = command.Description,
            Price = command.Price,
            ImageFile = command.ImageFile,
            Brand = brand,
            Type = type,
            CreatedDate = product.CreatedDate // Preserve the original created date
        };
    }
    
}