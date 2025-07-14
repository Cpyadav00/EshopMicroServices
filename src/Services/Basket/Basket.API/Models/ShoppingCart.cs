namespace Basket.API.Models;

public class ShoppingCart
{
    public string Username { get; set; } = default!;                            // Username of the shopper
    public List<ShoppingCartItem> Items { get; set; } = new();                     // List of shopping cart items
    public decimal TotalPrice => Items.Sum(item => item.Price * item.Quantity);                            // Calculated total price

   public ShoppingCart(string username)
    {
        Username= username;
    }
    //Required for mapping 
    public ShoppingCart() { }
    
}
