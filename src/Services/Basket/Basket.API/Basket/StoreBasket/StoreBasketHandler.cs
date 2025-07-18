using Discount.Grpc;

namespace Basket.API.Basket.StoreBasket;

public record StoreBasketCommand(ShoppingCart Cart):ICommand<StoreBasketResult>;

public record StoreBasketResult(string UserName);

public class StoreBasketTCommandValidator:AbstractValidator<StoreBasketCommand>
{
    public StoreBasketTCommandValidator()
    {
        RuleFor(x => x.Cart).NotNull().WithMessage("Cart cannot be null");
        RuleFor(x => x.Cart.Username).NotEmpty().WithMessage("UserName is required");

    }
}
public class StoreBasketCommandHandler(IBasketRepository repository,DiscountProtoService.DiscountProtoServiceClient discount) 
    : ICommandHandler<StoreBasketCommand, StoreBasketResult>
{
    public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
    {
        await DeductDiscount(command.Cart, cancellationToken);
        //TODO : store basket in database (use Marten upsert - if exist = update,if not 
        //TODO : update cache
        await repository.StoreBasket(command.Cart, cancellationToken);

        return new StoreBasketResult(command.Cart.Username);
    }

    private async Task DeductDiscount(ShoppingCart cart, CancellationToken cancellationToken)
    {
        //TODO : communicate with discount.GRPC and calculate the latest price
        foreach (var item in cart.Items)
        {
            var coupon = await discount.GetDiscountAsync(new GetDiscountRequest { ProductName = item.ProductName }, cancellationToken: cancellationToken);
            item.Price -= coupon.Amount;
        }

    }

}
