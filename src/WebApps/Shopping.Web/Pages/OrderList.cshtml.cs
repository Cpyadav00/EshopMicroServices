using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Shopping.Web.Pages
{
    public class OrderListModel
        (IOrderingService orderingService, IBasketService basketService, ILogger<ProductListModel> logger) 
        : PageModel
    {
        public IEnumerable<OrderModel> Orders { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            var customerId = new Guid("11111111-1111-1111-1111-111111111111");

            var response = await orderingService.GetOrdersByCustomer(customerId);
            Orders= response.Orders;
            return Page();

        }
    }
}
