
namespace Ordering.Infrastructure.Data.Configurations;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasConversion(
            orderId => orderId.Value,
            dbId => OrderItemId.Of(dbId));

        builder.HasOne<Product>()
            .WithMany()
            .HasForeignKey(c => c.ProductId);
        builder.Property(c => c.Quantity).IsRequired();
        builder.Property(c => c.Price).IsRequired();
    }
}
