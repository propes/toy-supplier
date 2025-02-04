using api.Features.Products.Repositories;
using api.Shared;
using Microsoft.Azure.Cosmos;

namespace api.Features.Products.Endpoints;

public class GetProductsEndpoint(GetProductsHandler handler) : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet(
            "api/products",
            async (
                Container container,
                int page = 1,
                int pageSize = 10,
                string? sortBy = null,
                string? sortDirection = null,
                string? sku = null,
                string? name = null,
                decimal? price = null
            ) =>
            {
                var query = new GetProducts
                {
                    Page = page,
                    PageSize = pageSize,
                    SortBy = sortBy,
                    SortDirection = sortDirection,
                    Sku = sku,
                    Name = name,
                    Price = price,
                };

                var products = await handler.Handle(query);

                return Results.Json(products);
            }
        );
    }
}
