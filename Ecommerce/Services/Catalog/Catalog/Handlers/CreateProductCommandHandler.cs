using Catalog.Commands;
using Catalog.Mappers;
using Catalog.Repositories;
using Catalog.Responses;
using MediatR;

namespace Catalog.Handlers;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductResponse>
{
    private readonly IProductRepository _productRepository;
    public CreateProductCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    
    public async Task<ProductResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        // Fetch Brand and Type from their respective repositories
        var brand = await _productRepository.GetBrandByBrandIdAsync(request.BrandId);
        var type = await _productRepository.GetTypeByTypeIdAsync(request.TypeId);
        
        if (brand == null)
        {
            throw new ApplicationException($"Brand with ID {request.BrandId} not found.");
        }
        if (type == null)
        {
            throw new ApplicationException($"Type with ID {request.TypeId} not found.");
        }
        // Match to entity
        var newProductEntity = request.ToProductEntity(brand, type);
        var newProduct = await _productRepository.CreateProductAsync(newProductEntity);
        return newProduct.ToProductResponse();
    }
}