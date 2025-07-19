namespace Ordering.Infrastructure.Data.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasConversion(
            orderId => orderId.Value,
            dbId => OrderId.Of(dbId));


        builder.HasOne<Customer>()
            .WithMany()
            .HasForeignKey(c => c.CustomerId);

        builder.HasMany(o=>o.OrderItems)
            .WithOne()
            .HasForeignKey(c => c.OrderId);

        builder.ComplexProperty(c => c.OrderName, nameBuilder =>
        {
            nameBuilder.Property(n => n.Value)
            .HasColumnName(nameof(Order.OrderName))
            .HasMaxLength(100)
            .IsRequired();
        });

        builder.ComplexProperty(o => o.ShippingAddress, addressBuilder =>
        {
            addressBuilder.Property(a => a.FirstName).HasMaxLength(50).IsRequired();
            addressBuilder.Property(a => a.LastName).HasMaxLength(50).IsRequired();
            addressBuilder.Property(a => a.EmailAddress).HasMaxLength(50).IsRequired();
            addressBuilder.Property(a => a.AddressLine).HasMaxLength(180).IsRequired();
            addressBuilder.Property(a => a.Country).HasMaxLength(50).IsRequired();
            addressBuilder.Property(a => a.State).HasMaxLength(50).IsRequired();
            addressBuilder.Property(a => a.ZipCode).HasMaxLength(5).IsRequired();
        });
        builder.ComplexProperty(o => o.BillingAddress, addressBuilder =>
        {
            addressBuilder.Property(a => a.FirstName).HasMaxLength(50).IsRequired();
            addressBuilder.Property(a => a.LastName).HasMaxLength(50).IsRequired();
            addressBuilder.Property(a => a.EmailAddress).HasMaxLength(50).IsRequired();
            addressBuilder.Property(a => a.AddressLine).HasMaxLength(180).IsRequired();
            addressBuilder.Property(a => a.Country).HasMaxLength(50).IsRequired();
            addressBuilder.Property(a => a.State).HasMaxLength(50).IsRequired();
            addressBuilder.Property(a => a.ZipCode).HasMaxLength(5).IsRequired();
        });

        builder.ComplexProperty(o => o.Payment, payment =>
        {
            payment.Property(a => a.CardName).HasMaxLength(50).IsRequired();
            payment.Property(a => a.CardNumber).HasMaxLength(24).IsRequired();
            payment.Property(a => a.Expiration).HasMaxLength(10).IsRequired();
            payment.Property(a => a.CVV).HasMaxLength(3).IsRequired();
            payment.Property(a => a.PaymentMethod).IsRequired();

        });

        builder.Property(o => o.Status)
            .HasDefaultValue(OrderStatus.Draft)
            .HasConversion(s => s
            .ToString(), dbStatus => (OrderStatus)Enum.Parse(typeof(OrderStatus), dbStatus));

        //builder.Property(o => o.TotalPrice);
        builder.Ignore(o => o.TotalPrice);
        //builder.Entity<Order>().Ignore(o => o.TotalPrice);

    }
}
