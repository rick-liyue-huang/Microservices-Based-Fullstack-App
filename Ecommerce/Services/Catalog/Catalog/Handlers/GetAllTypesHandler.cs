using Catalog.Mappers;
using Catalog.Queries;
using Catalog.Repositories;
using Catalog.Responses;
using MediatR;

namespace Catalog.Handlers;

public class GetAllTypesHandler : IRequestHandler<GetAllTypesQuery, IList<TypeResponses>>
{
    private readonly ITypeRepository _typeRepository;
    public GetAllTypesHandler(ITypeRepository typeRepository)
    {
        _typeRepository = typeRepository;
    }
    
    public async Task<IList<TypeResponses>> Handle(GetAllTypesQuery request, CancellationToken cancellationToken)
    {
        var typeList = await  _typeRepository.GetAllTypesAsync();
        return typeList.ToTypeResponsesList();
    }
}