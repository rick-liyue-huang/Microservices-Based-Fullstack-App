using Catalog.Entities;
using Catalog.Responses;

namespace Catalog.Mappers;

public static class BrandMapper
{
    public static BrandResponses ToResponse(this ProductBrand brand)
    {
        return new BrandResponses
        {
            Id = brand.Id,
            Name = brand.Name
        };
    }

    public static IList<BrandResponses> ToResponsesList(this IEnumerable<ProductBrand> brands)
    {
        return brands.Select(b => b.ToResponse()).ToList();
    }
}