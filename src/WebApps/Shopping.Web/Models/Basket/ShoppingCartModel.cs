namespace Shopping.Web.Models.Basket;

public class ShoppingCartModel
{
    public string Username { get; set; } = default!;                            // Username of the shopper
    public List<ShoppingCartItemModel> Items { get; set; } = new();                     // List of shopping cart items
    public decimal TotalPrice => Items.Sum(item => item.Price * item.Quantity);
}

public class ShoppingCartItemModel
{
    public Guid ProductId { get; set; } = default!;       // Unique identifier for the product
    public string ProductName { get; set; } = default!;     // Name of the product
    public int Quantity { get; set; } = default!;           // Available quantity in stock
    public string Color { get; set; } = default!;           // Color of the product
    public decimal Price { get; set; } = default!;          // Price of the product
}

public record GetBasketResponse(ShoppingCartModel Cart);
public record StoreBasketRequest(ShoppingCartModel Cart);
public record StoreBasketResponse(string UserName);
public record DeleteBasketResponse(bool IsSuccess);