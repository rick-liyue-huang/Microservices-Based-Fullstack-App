namespace Basket.Responses;

public record class ShoppingCartResponse
{
    public string UserName { get; init; }
    public List<ShoppingCartItemResponse> Items { get; init; }
    
    public ShoppingCartResponse()
    {
        UserName = string.Empty;
        Items = new List<ShoppingCartItemResponse>();
    }
    public ShoppingCartResponse(string UserName) : this(UserName, new List<ShoppingCartItemResponse>())
    {
        
    }
    public ShoppingCartResponse(string UserName, List<ShoppingCartItemResponse> Items) 
    {
        UserName = UserName;
        Items = Items ?? new List<ShoppingCartItemResponse>();
    }
    
    public decimal TotalPrice => Items.Sum(item => item.Price * item.Quantity);
}

// todo