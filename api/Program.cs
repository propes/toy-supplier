using System.Text.Json;
using api.Domain;
using Microsoft.Azure.Cosmos;

var builder = WebApplication.CreateBuilder(args);

// Configure CosmosDB connection
var cosmosSettings = builder.Configuration.GetSection("CosmosDb");
var connectionString = cosmosSettings["ConnectionString"];
var databaseName = cosmosSettings["DatabaseName"];

builder.Services.AddSingleton(sp =>
{
    var cosmosClient = new CosmosClient(connectionString);
    return cosmosClient.GetContainer(databaseName, "products");
});

var app = builder.Build();

app.MapGet(
    "/products",
    async (HttpContext context, Container container) =>
    {
        // Retrieve query parameters
        var query = context.Request.Query;
        var page = int.TryParse(query["page"], out var p) ? Math.Max(p, 1) : 1;
        var pageSize = int.TryParse(query["pageSize"], out var ps) ? Math.Max(ps, 1) : 10;
        string? sortBy = query["sortBy"];
        string? sortDirection = query["sortDirection"];
        string? sku = query["sku"];
        string? name = query["name"];
        decimal? price = decimal.TryParse(query["price"], out var pr) ? pr : null;

        // Construct the query definition with filtering
        var queryDefinition = new QueryDefinition(
            @"
            SELECT * FROM c 
            WHERE (NOT IS_DEFINED(@sku) OR c.id = @sku) 
            AND (NOT IS_DEFINED(@name) OR c.name = @name) 
            AND (NOT IS_DEFINED(@price) OR c.price = @price)
        "
        );

        if (!string.IsNullOrWhiteSpace(sku))
            queryDefinition = queryDefinition.WithParameter("@sku", sku);

        if (!string.IsNullOrWhiteSpace(name))
            queryDefinition = queryDefinition.WithParameter("@name", name);

        if (price.HasValue)
            queryDefinition = queryDefinition.WithParameter("@price", price);

        var iterator = container.GetItemQueryIterator<Product>(queryDefinition);

        var results = new List<dynamic>();
        while (iterator.HasMoreResults)
        {
            var response = await iterator.ReadNextAsync();
            results.AddRange(response);
        }

        // Apply sorting if needed
        if (!string.IsNullOrWhiteSpace(sortBy))
        {
            results =
                sortDirection?.ToLower() == "desc"
                    ? results
                        .OrderByDescending(r => r.GetType().GetProperty(sortBy)?.GetValue(r))
                        .ToList()
                    : results.OrderBy(r => r.GetType().GetProperty(sortBy)?.GetValue(r)).ToList();
        }

        // Apply pagination
        var pagedResults = results.Skip((page - 1) * pageSize).Take(pageSize);

        context.Response.ContentType = "application/json";
        await context.Response.WriteAsync(JsonSerializer.Serialize(pagedResults));
    }
);

app.Run();
