using Catalog.Entities;
using Catalog.Responses;

namespace Catalog.Mappers;

public static class TypeMapper
{
    public static TypeResponses ToTypeResponses(this ProductType type)
    {
        return new TypeResponses
        {
            Id = type.Id,
            Name = type.Name
        };
    }
    
    public static IList<TypeResponses> ToTypeResponsesList(this IEnumerable<ProductType> types)
    {
        return types.Select(t => t.ToTypeResponses()).ToList();
    }
}