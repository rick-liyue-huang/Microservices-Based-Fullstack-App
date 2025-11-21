using Basket.Entities;

namespace Basket.Repositories;

public interface IBasketRepository
{
    Task<ShoppingCart> GetBasket(string userName);
    Task<ShoppingCart> UpdateBasket(ShoppingCart cart);
    Task DeleteBasket(string userName);
}