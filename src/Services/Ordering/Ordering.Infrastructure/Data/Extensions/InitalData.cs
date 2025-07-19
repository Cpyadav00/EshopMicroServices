namespace Ordering.Infrastructure.Data.Extensions;

internal class InitalData
{
    public static IEnumerable<Customer> Customers => new List<Customer>
    {
        Customer.Create(CustomerId.Of(new Guid("11111111-1111-1111-1111-111111111111")), "Jane", "jane.smith@example.com"),
        Customer.Create(CustomerId.Of(new Guid("22222222-2222-2222-2222-222222222222")), "John", "john.doe@example.com")
    };

    public static IEnumerable<Product> Products => new List<Product>
    {
        Product.Create(ProductId.Of(new Guid("aaaaaaa1-aaaa-aaaa-aaaa-aaaaaaaaaaaa")), "Wireless Mouse", 100),
        Product.Create(ProductId.Of(new Guid("bbbbbbb2-bbbb-bbbb-bbbb-bbbbbbbbbbbb")), "Mechanical Keyboard", 50)
    };

    public static IEnumerable<Order> OrdersWithItems
    {
        get
        {
            var address1 = Address.Of("Jane", "Smith", "jane.smith@example.com", "123 Main St", "USA", "NY", "10001");
            var address2 = Address.Of("John", "Doe", "john.doe@example.com", "456 Market St", "USA", "CA", "90001");

            var payment1 = Payment.Of("Jane Smith", "4111111111111111", "12/27", "123", 1);
            var payment2 = Payment.Of("John Doe", "4222222222222222", "11/28", "456", 2);

            var orderId1 = OrderId.Of(new Guid("33333333-3333-3333-3333-333333333333"));
            var orderId2 = OrderId.Of(new Guid("44444444-4444-4444-4444-444444444444"));

            var order1 = Order.Create(
                orderId1,
                CustomerId.Of(new Guid("11111111-1111-1111-1111-111111111111")), // Jane
                OrderName.Of("Order 1"),
                shippingAddress: address1,
                billingAddress: address1,
                payment1
            );

            order1.Add(ProductId.Of(new Guid("aaaaaaa1-aaaa-aaaa-aaaa-aaaaaaaaaaaa")), 2, 670000);
            order1.Add(ProductId.Of(new Guid("bbbbbbb2-bbbb-bbbb-bbbb-bbbbbbbbbbbb")), 7, 500000);

            var order2 = Order.Create(
                orderId2,
                CustomerId.Of(new Guid("22222222-2222-2222-2222-222222222222")), // John
                OrderName.Of("Order 2"),
                shippingAddress: address2,
                billingAddress: address2,
                payment2
            );

            order2.Add(ProductId.Of(new Guid("aaaaaaa1-aaaa-aaaa-aaaa-aaaaaaaaaaaa")), 2, 50000);
            order2.Add(ProductId.Of(new Guid("bbbbbbb2-bbbb-bbbb-bbbb-bbbbbbbbbbbb")), 7, 70000);

            return new List<Order> { order1, order2 };
        }
    }
}
