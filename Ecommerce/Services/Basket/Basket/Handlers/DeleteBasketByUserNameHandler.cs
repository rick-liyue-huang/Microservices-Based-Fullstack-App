using Basket.Commands;
using Basket.Repositories;
using MediatR;

namespace Basket.Handlers;

public class DeleteBasketByUserNameHandler : IRequestHandler<DeleteBasketByUserNameCommand, Unit>
{
    private readonly IBasketRepository _basketRepository;
    public DeleteBasketByUserNameHandler(IBasketRepository repository)
    {
        _basketRepository = repository;
    }
    
    public async Task<Unit> Handle(DeleteBasketByUserNameCommand request, CancellationToken cancellationToken)
    {
        await _basketRepository.DeleteBasket(request.UserName);
        return Unit.Value;
    }
}