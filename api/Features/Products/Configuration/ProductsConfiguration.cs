using api.Features.Products.Endpoints;
using api.Features.Products.Repositories;
using api.Shared;

namespace api.Features.Products.Configuration;

public static class ProductsConfiguration
{
    public static void ConfigureProducts(this IServiceCollection services)
    {
        services.AddScoped<IEndpoint, GetProductsEndpoint>();
        services.AddScoped<GetProductsHandler>();
    }
}
