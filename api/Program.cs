using api.DI;
using api.Features.Products.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.ConfigureCosmosDB();
builder.Services.ConfigureProducts();

var app = builder.Build();

app.MapEndpoints();

app.Run();
