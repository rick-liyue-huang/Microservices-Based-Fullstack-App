using Catalog.Mappers;
using Catalog.Queries;
using Catalog.Repositories;
using Catalog.Responses;
using MediatR;

namespace Catalog.Handlers;

public class GetAllBrandsHandler : IRequestHandler<GetAllBrandsQuery, IList<BrandResponses>>
{
    private readonly IBrandRepository _brandRepository;
    public GetAllBrandsHandler(IBrandRepository brandRepository)
    {
        _brandRepository = brandRepository;
    }
    
    public async Task<IList<BrandResponses>> Handle(GetAllBrandsQuery request, CancellationToken cancellationToken)
    {
        var brandList = await _brandRepository.GetAllBrandsAsync();
        return brandList.ToResponsesList();
    }
}