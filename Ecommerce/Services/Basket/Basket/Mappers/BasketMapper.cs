using Basket.Commands;
using Basket.Entities;
using Basket.Responses;

namespace Basket.Mappers;

public static class BasketMapper
{
    public static ShoppingCartResponse ToShoppingCartResponse(this ShoppingCart cart)
    {
        return new ShoppingCartResponse
        {
            UserName = cart.UserName,
            Items = cart.Items.Select(item => new ShoppingCartItemResponse
            {
                Quantity = item.Quantity,
                ImageFile = item.ImageFile,
                Price = item.Price,
                ProductId = item.ProductId,
                ProductName = item.ProductName
            }).ToList()
        };
    }
    
    public static ShoppingCart ToShoppingCartEntity(this CreateShoppingCartCommand command)
    {
        return new ShoppingCart
        {
            UserName = command.UserName,
            Items = command.Items.Select(item => new ShoppingCartItem
            {
                Quantity = item.Quantity,
                ImageFile = item.ImageFile,
                Price = item.Price,
                ProductId = item.ProductId,
                ProductName = item.ProductName
            }).ToList()
        };
    }
}