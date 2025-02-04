using api.Shared;

namespace api.DI;

public static class EndpointExtensions
{
    public static void MapEndpoints(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;
        var endpoints = services.GetServices<IEndpoint>();
        foreach (var endpoint in endpoints)
        {
            endpoint.MapEndpoint(app);
        }
    }
}
