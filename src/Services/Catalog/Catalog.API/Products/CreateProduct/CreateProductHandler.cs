namespace Catalog.API.Products.CreateProduct;

public record CreateProductCommand(string Name,List<string> Category,string Description,string ImageFile,decimal prices)
    : ICommand<CreateProductResult>;
public record CreateProductResult(Guid Id);

internal class CreateProductCommandHandler(IDocumentSession session) 
    : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        //Bussiness logic to create a product
        var product = new Product 
        { 
        Name=command.Name,
        Category=command.Category,  
        Description=command.Description,
        ImageFile=command.ImageFile,
        Price=command.prices
        };


        //Save to database
        session.Store(product);
        await session.SaveChangesAsync(cancellationToken);


        //return CreatedProductResult result 

        return new CreateProductResult(product.Id);

    }
}
