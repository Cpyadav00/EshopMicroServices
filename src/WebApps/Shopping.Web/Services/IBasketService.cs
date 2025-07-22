

namespace Shopping.Web.Services;

public interface IBasketService
{
    [Get("/basket-service/basket/{username}")]
    Task<GetBasketResponse> GetBasket(string username);

    [Post("/basket-service/basket")]
    Task<StoreBasketResponse> StoreBasket(StoreBasketRequest request);

    [Delete("/basket-service/basket/{username}")]
    Task<DeleteBasketResponse> DeleteBasket(string username);

    [Post("/basket-service/basket/checkout")]
    Task<CheckoutBasketReponse> CheckoutBasket(CheckoutBasketRequest request);

    public async Task<ShoppingCartModel> LoadUserBasket()
    {
        var userName = "CP";
        ShoppingCartModel basket;
        try
        {
            var getBasketResponse = await GetBasket(userName);
            basket = getBasketResponse.Cart;

        }
        catch (ApiException apiException) when (apiException.StatusCode ==System.Net.HttpStatusCode.NotFound) 
        {

            basket = new ShoppingCartModel
            {
                Username = userName,
                Items = [],

            };

        }
        return basket;
    }
}