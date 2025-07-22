
namespace Shopping.Web.Pages;

public class CheckoutModel
    (ICatalogService catalogService, IBasketService basketService, ILogger<CheckoutModel> logger) 
    : PageModel
{
    [BindProperty]
    public BasketCheckoutModel Order { get; set; } = default!;
    public ShoppingCartModel Cart { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync()
    {
        Cart = await basketService.LoadUserBasket();
        return Page();
    }
    public async Task<IActionResult> OnPostAddToCartAsync()
    {
        logger.LogInformation("Checkut button is clicked");
        Cart=await basketService.LoadUserBasket();
        if(!ModelState.IsValid)
        {
            return Page();
        }

        Order.CustomerId = new Guid("11111111-1111-1111-1111-111111111111");
        Order.UserName=Cart.Username; 
        Order.TotalPrice=Cart.TotalPrice;

        await basketService.CheckoutBasket(new CheckoutBasketRequest(Order));
        return RedirectToPage("Confirmation", "OrderSubmitted");
    }
}
