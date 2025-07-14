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
public class StoreBasketCommandHandler 
    : ICommandHandler<StoreBasketCommand, StoreBasketResult>
{
    public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
    {
        ShoppingCart cart= command.Cart;
        //TODO : store basket in database (use Marten upsert - if exist = update,if not 
        //TODO : update cache
        return new StoreBasketResult("swn");
    }
}
