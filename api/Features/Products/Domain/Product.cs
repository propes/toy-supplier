namespace api.Features.Products.Domain;

public class Product
{
    public required string Id { get; init; }
    public required string Name { get; init; }
    public required decimal Price { get; init; }
}
