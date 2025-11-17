using Catalog.Queries;
using Catalog.Repositories;
using Catalog.Responses;
using MediatR;
using Catalog.Mappers;

namespace Catalog.Handlers;

public class GetProductsByBrandHandler : IRequestHandler<GetProductsByBrandQuery, IList<ProductResponse>>
{
    private readonly IProductRepository _productRepository;
    public GetProductsByBrandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    
    public async Task<IList<ProductResponse>> Handle(GetProductsByBrandQuery request, CancellationToken cancellationToken)
    {
        var productList = await _productRepository.GetAllProductByBrandAsync(request.BrandName);
        return productList.ToProductByBrandResponseList().ToList();
    }
}