using Microsoft.Azure.Cosmos;

namespace api.DI;

public class CosmosDbSettings
{
    public required string ConnectionString { get; init; }
    public required string DatabaseName { get; init; }
}

public static class CosmosDbExtensions
{
    public static void ConfigureCosmosDB(this WebApplicationBuilder builder)
    {
        var cosmosSettings =
            builder.Configuration.GetSection("CosmosDb").Get<CosmosDbSettings>()
            ?? throw new InvalidOperationException("CosmosDb configuration is missing or invalid.");

        builder.Services.AddSingleton(sp =>
        {
            var cosmosClient = new CosmosClient(cosmosSettings.ConnectionString);
            return cosmosClient.GetContainer(cosmosSettings.DatabaseName, "products");
        });
    }
}
