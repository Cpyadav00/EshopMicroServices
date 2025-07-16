using Discount.Grpc.Models;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Data;

public class DiscountContext:DbContext
{
    public DbSet<Coupon> Coupons { get; set; } = default!;

    public DiscountContext(DbContextOptions<DiscountContext> options):base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Coupon>().HasData
    (
        new Coupon
        {
            Id = 1,
            ProductName = "Laptop",
            Description = "Flat discount on all laptops",
            Amount = 5000
        },
        new Coupon
        {
            Id = 2,
            ProductName = "Mobile",
            Description = "Discount on selected smartphones",
            Amount = 2000
        },
        new Coupon
        {
            Id = 3,
            ProductName = "Headphones",
            Description = "Limited time headphone offer",
            Amount = 500
        },
        new Coupon
        {
            Id = 4,
            ProductName = "Keyboard",
            Description = "10% off on mechanical keyboards",
            Amount = 300
        },
        new Coupon
        {
            Id = 5,
            ProductName = "Monitor",
            Description = "Festival offer on monitors",
            Amount = 1500
        }
    );
    }
}
