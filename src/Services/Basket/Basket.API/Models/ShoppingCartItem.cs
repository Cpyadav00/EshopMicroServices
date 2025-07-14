namespace Basket.API.Models;

public class ShoppingCartItem
{
    public Guid ProductId { get; set; } = default!;       // Unique identifier for the product
    public string ProductName { get; set; } = default!;     // Name of the product
    public int Quantity { get; set; } = default!;           // Available quantity in stock
    public string Color { get; set; } = default!;           // Color of the product
    public decimal Price { get; set; } = default!;          // Price of the product
}
