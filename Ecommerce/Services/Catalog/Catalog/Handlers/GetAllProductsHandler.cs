using Catalog.Mappers;
using Catalog.Mappers;
using Catalog.Queries;
using Catalog.Repositories;
using Catalog.Responses;
using Catalog.Specifications;
using MediatR;

namespace Catalog.Handlers;

public class GetAllProductsHandler : IRequestHandler<GetAllProductsQuery, Pagination<ProductResponse>>
{
    private readonly IProductRepository _productRepository;
    public GetAllProductsHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    
    public async Task<Pagination<ProductResponse>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        var productList = await _productRepository.GetAllProductAsync(request.SpecParams);
        var productResponseList = productList.ToProductResponseList();
        return productResponseList;
    }
}