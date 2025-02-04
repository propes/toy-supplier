using api.Features.Products.Domain;
using api.Shared;
using Microsoft.Azure.Cosmos;

namespace api.Features.Products.Repositories;

public class GetProducts
{
    public int Page { get; init; } = 1;
    public int PageSize { get; init; } = 10;
    public string? SortBy { get; init; } = null;
    public string? SortDirection { get; init; } = null;
    public string? Sku { get; init; } = null;
    public string? Name { get; init; } = null;
    public decimal? Price { get; init; } = null;
}

public sealed class GetProductsHandler(Container container)
{
    public async Task<PaginatedResult<Product>> Handle(GetProducts query)
    {
        EnsureInputsAreValid(query);

        int totalCount = await GetTotalCount(query.Sku, query.Name, query.Price);
        if (totalCount == 0)
        {
            return new PaginatedResult<Product>([], totalCount, query.Page, query.PageSize);
        }

        var queryDefinition = BuildPaginatedQuery(
            query.SortBy,
            query.SortDirection,
            query.Sku,
            query.Name,
            query.Price,
            query.Page,
            query.PageSize
        );
        var products = await FetchResults(queryDefinition);

        return new PaginatedResult<Product>(products, totalCount, query.Page, query.PageSize);
    }

    private static void EnsureInputsAreValid(GetProducts query)
    {
        if (query.Page < 1)
            throw new ArgumentException("Page must be greater than or equal to 1", nameof(query));

        if (query.PageSize < 1)
            throw new ArgumentException(
                "Page size must be greater than or equal to 1",
                nameof(query)
            );
    }

    private async Task<int> GetTotalCount(string? sku, string? name, decimal? price)
    {
        var countQueryText =
            @"
            SELECT VALUE COUNT(1) FROM c
            WHERE (NOT IS_DEFINED(@sku) OR c.id = @sku)
            AND (NOT IS_DEFINED(@name) OR CONTAINS(LOWER(c.name), LOWER(@name)))
            AND (NOT IS_DEFINED(@price) OR c.price = @price)
        ";

        var countQuery = new QueryDefinition(countQueryText);
        if (!string.IsNullOrWhiteSpace(sku))
            countQuery = countQuery.WithParameter("@sku", sku);
        if (!string.IsNullOrWhiteSpace(name))
            countQuery = countQuery.WithParameter("@name", name.ToLower());
        if (price.HasValue)
            countQuery = countQuery.WithParameter("@price", price);

        var iterator = container.GetItemQueryIterator<int>(countQuery);
        if (iterator.HasMoreResults)
        {
            var response = await iterator.ReadNextAsync();
            return response.First(); // CosmosDB returns a single count result
        }

        return 0;
    }

    private static QueryDefinition BuildPaginatedQuery(
        string? sortBy,
        string? sortDirection,
        string? sku,
        string? name,
        decimal? price,
        int page,
        int pageSize
    )
    {
        string orderByField = string.IsNullOrWhiteSpace(sortBy) ? "c.id" : $"c.{sortBy}";
        string orderDirection = sortDirection?.ToLower() == "desc" ? "DESC" : "ASC";

        var queryText =
            $@"
            SELECT * FROM c 
            WHERE (NOT IS_DEFINED(@sku) OR c.id = @sku) 
            AND (NOT IS_DEFINED(@name) OR CONTAINS(LOWER(c.name), LOWER(@name))) 
            AND (NOT IS_DEFINED(@price) OR c.price = @price)
            ORDER BY {orderByField} {orderDirection}
            OFFSET @offset LIMIT @pageSize
        ";

        var queryDefinition = new QueryDefinition(queryText)
            .WithParameter("@offset", (page - 1) * pageSize)
            .WithParameter("@pageSize", pageSize);

        if (!string.IsNullOrWhiteSpace(sku))
            queryDefinition = queryDefinition.WithParameter("@sku", sku);
        if (!string.IsNullOrWhiteSpace(name))
            queryDefinition = queryDefinition.WithParameter("@name", name.ToLower());
        if (price.HasValue)
            queryDefinition = queryDefinition.WithParameter("@price", price);

        return queryDefinition;
    }

    private async Task<IReadOnlyList<Product>> FetchResults(QueryDefinition queryDefinition)
    {
        var results = new List<Product>();

        var iterator = container.GetItemQueryIterator<Product>(queryDefinition);
        while (iterator.HasMoreResults)
        {
            var response = await iterator.ReadNextAsync();
            results.AddRange(response);
        }
        return results.AsReadOnly();
    }
}
