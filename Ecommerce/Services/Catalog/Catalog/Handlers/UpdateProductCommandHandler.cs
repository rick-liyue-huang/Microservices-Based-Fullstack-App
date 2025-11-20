using Catalog.Commands;
using Catalog.Repositories;
using Catalog.Mappers;
using MediatR;

namespace Catalog.Handlers;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, bool>
{
    private readonly IProductRepository _productRepository;
    public UpdateProductCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    
    public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var existing = await _productRepository.GetProductByIdAsync(request.Id);
        if (existing == null)
        {
            throw new KeyNotFoundException($"Product with ID {request.Id} not found.");
        }
        
        // step 1, fetch Brand and Type from their respective repositories
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
        
        // step 2, Mapper role
        var updatedProduct = request.ToUpdateProductEntity(existing, brand, type);
        
        // step 3, save record to database
        return await _productRepository.UpdateProductAsync(updatedProduct);
        
    }
    
}