using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;

string endpointUrl = "https://localhost:8081";
string primaryKey = "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==";
string databaseId = "toys";
string containerId = "products";

using CosmosClient client = new(endpointUrl, primaryKey, new CosmosClientOptions { AllowBulkExecution = true });

Database database = await client.CreateDatabaseIfNotExistsAsync(databaseId);
Container container = await database.CreateContainerIfNotExistsAsync(containerId, "/id");

var itemsToInsert = new List<dynamic>
{
    new { id = "123", name = "My Little Pony", price = 15.99M },
    new { id = "456", name = "ACME Slot Car Set", price = 149.99M }
};

foreach (var item in itemsToInsert)
{
    await container.UpsertItemAsync(item);
    Console.WriteLine($"Inserted: {item.name}");
}

Console.WriteLine("Data seeding complete.");
