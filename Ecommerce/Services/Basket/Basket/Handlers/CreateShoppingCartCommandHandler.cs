using Basket.Commands;
using Basket.Mappers;
using Basket.Repositories;
using Basket.Responses;
using MediatR;

namespace Basket.Handlers;

public class CreateShoppingCartCommandHandler : IRequestHandler<CreateShoppingCartCommand, ShoppingCartResponse>
{
    private readonly IBasketRepository _basketRepository;
    public CreateShoppingCartCommandHandler(IBasketRepository basketRepository)
    {
        _basketRepository = basketRepository;
    }
    
    public async Task<ShoppingCartResponse> Handle(CreateShoppingCartCommand request, CancellationToken cancellationToken)
    {
        // convert command to domain entity
        var shoppingCart = request.ToShoppingCartEntity();
        // save to redis
        var updatedCart = await  _basketRepository.UpdateBasket(shoppingCart);
        
        // convert back to response
        return updatedCart.ToShoppingCartResponse();
    }
}