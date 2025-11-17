using Catalog.Responses;
using Catalog.Specifications;
using MediatR;

namespace Catalog.Queries;

public record GetAllProductsQuery(CatalogSpecParams SpecParams) : IRequest<Pagination<ProductResponse>>
{
    
}