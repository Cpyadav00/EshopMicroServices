using BuildingBlocks.Messaging.Events;
using MassTransit;
using MediatR;
using Ordering.Application.Dtos;
using Ordering.Application.Orders.Commands.CreateOrder;

namespace Ordering.Application.Orders.EventHandlers.Integration;

public class BasketCheckoutEventHandler(ILogger<BasketCheckoutEventHandler> logger,ISender sender)
    : IConsumer<BasketCheckOutEvent>
{
    public async Task Consume(ConsumeContext<BasketCheckOutEvent> context)
    {
        logger.LogInformation("Integration Event handled :{IntegrationEvent}", context.GetType().Name);
        var command = MapToCreateOrderCommand(context.Message);
        await sender.Send(command);



    }

    private CreateOrderCommand MapToCreateOrderCommand(BasketCheckOutEvent message)
    {
        var addressDto = new AddressDto(message.FirstName, message.LastName, message.AddressLine, message.EmailAddress, message.Country, message.State, message.ZipCode);
        var paymentDto=new PaymentDto(message.CardName,message.CardNumber, message.Expiration,message.CVV,message.PaymentMethod);
        var orderId=Guid.NewGuid();
        var orderDto = new OrderDto(
            Id: orderId,
            CustomerId: message.CustomerId,
            OrderName: message.UserName,
            ShippingAddress: addressDto,
            BillingAddress: addressDto,
            Payment: paymentDto,
            Status: Ordering.Domain.Enums.OrderStatus.Pending,
            OrderItems: [
                new OrderItemDto(orderId,new Guid("bbbbbbb2-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),2,500),
                new OrderItemDto(orderId,new Guid("aaaaaaa1-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),1,400)

                ]
            );
        return new CreateOrderCommand(orderDto);
    }
}
