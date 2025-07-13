using Marten.Schema;

namespace Catalog.API.Data;

public class CatalgInitalData : IInitialData
{
    public async Task Populate(IDocumentStore store, CancellationToken cancellation)
    {
        using var session = store.LightweightSession();
        if (await session.Query<Product>().AnyAsync())
        {
            return;
        }
        session.Store<Product>(GetPreConfiguredProduct());
        await session.SaveChangesAsync();
    }

    private static IEnumerable<Product> GetPreConfiguredProduct() => new List<Product>()
    {
        new Product
    {
        Id = Guid.Parse("a1a1f6b0-6c91-4a12-8c1a-111111111111"),
        Name = "Wireless Mouse",
        Category = new List<string> { "Electronics", "Accessories" },
        Description = "Ergonomic wireless mouse with long battery life.",
        ImageFile = "mouse.jpg",
        Price = 29.99M
    },
    new Product
    {
        Id = Guid.Parse("b2b2f6b0-6c91-4a12-8c1a-222222222222"),
        Name = "Gaming Keyboard",
        Category = new List<string> { "Electronics", "Gaming" },
        Description = "Mechanical RGB keyboard with fast response time.",
        ImageFile = "keyboard.jpg",
        Price = 89.99M
    },
    new Product
    {
        Id = Guid.Parse("c3c3f6b0-6c91-4a12-8c1a-333333333333"),
        Name = "Bluetooth Speaker",
        Category = new List<string> { "Electronics", "Audio" },
        Description = "Portable speaker with crystal clear sound.",
        ImageFile = "speaker.jpg",
        Price = 49.99M
    },
    new Product
    {
        Id = Guid.Parse("d4d4f6b0-6c91-4a12-8c1a-444444444444"),
        Name = "USB-C Hub",
        Category = new List<string> { "Accessories", "Laptops" },
        Description = "7-in-1 USB-C hub for Mac and Windows.",
        ImageFile = "hub.jpg",
        Price = 39.99M
    },
    new Product
    {
        Id = Guid.Parse("e5e5f6b0-6c91-4a12-8c1a-555555555555"),
        Name = "Smart Watch",
        Category = new List<string> { "Electronics", "Wearables" },
        Description = "Fitness tracker with heart rate monitor and GPS.",
        ImageFile = "watch.jpg",
        Price = 129.99M
    },
    new Product
    {
        Id = Guid.Parse("f6f6f6b0-6c91-4a12-8c1a-666666666666"),
        Name = "Noise Cancelling Headphones",
        Category = new List<string> { "Electronics", "Audio" },
        Description = "Over-ear headphones with active noise cancellation.",
        ImageFile = "headphones.jpg",
        Price = 149.99M
    },
    new Product
    {
        Id = Guid.Parse("a7a7f6b0-6c91-4a12-8c1a-777777777777"),
        Name = "4K Monitor",
        Category = new List<string> { "Electronics", "Display" },
        Description = "Ultra HD monitor with vivid color and clarity.",
        ImageFile = "monitor.jpg",
        Price = 279.99M
    },
    new Product
    {
        Id = Guid.Parse("b8b8f6b0-6c91-4a12-8c1a-888888888888"),
        Name = "Laptop Stand",
        Category = new List<string> { "Accessories", "Office" },
        Description = "Adjustable aluminum stand for laptops up to 17 inches.",
        ImageFile = "stand.jpg",
        Price = 24.99M
    },
    new Product
    {
        Id = Guid.Parse("c9c9f6b0-6c91-4a12-8c1a-999999999999"),
        Name = "External SSD 1TB",
        Category = new List<string> { "Storage", "Electronics" },
        Description = "High-speed solid state drive for backups and file transfer.",
        ImageFile = "ssd.jpg",
        Price = 99.99M
    },
    new Product
    {
        Id = Guid.Parse("d0d0f6b0-6c91-4a12-8c1a-000000000000"),
        Name = "Wireless Charger",
        Category = new List<string> { "Electronics", "Mobile" },
        Description = "Fast wireless charging pad for smartphones and earbuds.",
        ImageFile = "charger.jpg",
        Price = 19.99M
    }
    };
}
