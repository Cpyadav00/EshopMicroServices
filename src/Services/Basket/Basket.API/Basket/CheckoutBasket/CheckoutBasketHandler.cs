
using Basket.API.Basket.StoreBasket;
using BuildingBlocks.Messaging.Events;
using MassTransit;

namespace Basket.API.Basket.CheckoutBasket;

public record CheckoutBasketCommand(BasketCheckOutDto BasketCheckOutDto) : ICommand<CheckoutBasketResult>;
public record CheckoutBasketResult(bool IsSuccess);

public class CheckoutBasketCommandValidator : AbstractValidator<CheckoutBasketCommand>
{
    public CheckoutBasketCommandValidator()
    {
        RuleFor(x => x.BasketCheckOutDto).NotNull().WithMessage("BasketCheckOutDto cannot be null");
        RuleFor(x => x.BasketCheckOutDto.UserName).NotEmpty().WithMessage("UserName is required");

    }
}

public class CheckoutBasketCommandHandler(IBasketRepository repository,IPublishEndpoint publishEndpoint)
    : ICommandHandler<CheckoutBasketCommand, CheckoutBasketResult>
{
    public async Task<CheckoutBasketResult> Handle(CheckoutBasketCommand command, CancellationToken cancellationToken)
    {
        var basket = await repository.GetBasket(command.BasketCheckOutDto.UserName, cancellationToken);
        if (basket == null)
        {
            return new CheckoutBasketResult(false);
        }
        var eventMessage = command.BasketCheckOutDto.Adapt<BasketCheckOutEvent>();
        eventMessage.TotalPrice = basket.TotalPrice;

        await publishEndpoint.Publish(eventMessage,cancellationToken);

        await repository.DeleteBasket(command.BasketCheckOutDto.UserName, cancellationToken);
        return new CheckoutBasketResult(true);
    }
}
