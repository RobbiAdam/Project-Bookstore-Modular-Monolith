using Basket;
using Catalog;
using Ordering;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


var app = builder.Build();

builder.Services
    .AddBasketModule(builder.Configuration)
    .AddCatalogModule(builder.Configuration)
    .AddOrderingModule(builder.Configuration);

// Configure the HTTP request pipeline.

app
    .UseBasketModule()
    .UseCatalogModule()
    .UseOrderingModule();

app.Run();
