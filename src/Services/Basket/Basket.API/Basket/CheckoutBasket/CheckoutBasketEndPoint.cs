namespace Basket.API.Basket.CheckoutBasket;

public record CheckoutBasketRequest(BasketCheckOutDto BasketCheckOutDto);
public record CheckoutBasketReponse(bool IsSuccess);
public class CheckoutBasketEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("basket/checkout",async (CheckoutBasketRequest request,ISender sender)=>
           {
            var command = request.Adapt<CheckoutBasketCommand>();
            var result = await sender.Send(command);
            var response = result.Adapt<CheckoutBasketReponse>();
            return Results.Ok(response);
        })
            .WithName("CheckoutBasket")
            .Produces<CheckoutBasketReponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Checkout Basket")
            .WithDescription("Checkout Basket");
    }
}
