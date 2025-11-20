using Catalog.Commands;
using Catalog.Repositories;
using MediatR;

namespace Catalog.Handlers;

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, bool>
{
    private readonly IProductRepository _productRepository;
    public DeleteProductCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    
    public Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        return _productRepository.DeleteProductAsync(request.Id);
    }
}