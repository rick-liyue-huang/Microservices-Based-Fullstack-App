using Basket.Queries;
using Basket.Repositories;
using Basket.Responses;
using Basket.Mappers;
using MediatR;

namespace Basket.Handlers;

public class GetBasketByUserNameHandler : IRequestHandler<GetBasketByUserNameQuery, ShoppingCartResponse>
{
    private readonly IBasketRepository _basketRepository;
    public GetBasketByUserNameHandler(IBasketRepository repository)
    {
        _basketRepository = repository;
    }
    
    public async Task<ShoppingCartResponse> Handle(GetBasketByUserNameQuery request, CancellationToken cancellationToken)
    {
        var shoppingCart = await _basketRepository.GetBasket(request.UserName);
        if (shoppingCart == null)
        {
            return new ShoppingCartResponse(request.UserName)
            {
                Items = new List<ShoppingCartItemResponse>()
            };
        }
        return shoppingCart.ToShoppingCartResponse();
    }
}