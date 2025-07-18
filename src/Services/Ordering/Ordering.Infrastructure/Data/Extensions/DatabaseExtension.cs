﻿using Ordering.Infrastructure.Data.Extensions;

public static class DatabaseExtension
{
    public static async Task InitialiseDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        context.Database.MigrateAsync().GetAwaiter().GetResult();
        await SeedAsync(context);
    }

    private static async Task SeedCustomerAsync(ApplicationDbContext context)
    {
        if (!await context.Customers.AnyAsync())
        {
            await context.Customers.AddRangeAsync(InitalData.Customers);
            await context.SaveChangesAsync();
        }
    }

    private static async Task SeedProductAsync(ApplicationDbContext context)
    {
        if (!await context.Products.AnyAsync())
        {
            await context.Products.AddRangeAsync(InitalData.Products);
            await context.SaveChangesAsync();
        }
    }

    private static async Task SeedOrderandItemsAsync(ApplicationDbContext context)
    {
        if (!await context.Orders.AnyAsync())
        {
            await context.Orders.AddRangeAsync(InitalData.OrdersWithItems);
            await context.SaveChangesAsync();
        }
    }

    private static async Task SeedAsync(ApplicationDbContext context)
    {
        await SeedCustomerAsync(context);      // ✅ First
        await SeedProductAsync(context);       // ✅ Second
        await SeedOrderandItemsAsync(context); // ✅ Last
    }
}
