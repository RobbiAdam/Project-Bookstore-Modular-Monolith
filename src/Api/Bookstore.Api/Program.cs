using Keycloak.AuthServices.Authentication;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Host.UseSerilog((context, config) =>
{
    config.ReadFrom.Configuration(context.Configuration);
});

var catalogAssembly = typeof(CatalogModule).Assembly;
var basketAssembly = typeof(BasketModule).Assembly;
var orderingAssembly = typeof(OrderingModule).Assembly;

builder.Services
    .AddCarterWithAssemblies(catalogAssembly, basketAssembly, orderingAssembly)
    .AddMediatRWithAssemblies(basketAssembly, basketAssembly, orderingAssembly);

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
});

builder.Services.AddMassTransitWithAssemblies(
    builder.Configuration,
    catalogAssembly,
    basketAssembly,
    orderingAssembly);

builder.Services.AddKeycloakWebApiAuthentication(builder.Configuration);
builder.Services.AddAuthorization();

builder.Services
    .AddCatalogModule(builder.Configuration)
    .AddBasketModule(builder.Configuration)
    .AddOrderingModule(builder.Configuration);


builder.Services
    .AddExceptionHandler<CustomExceptionHandler>();



var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSerilogRequestLogging();
app.MapCarter();

app.UseAuthentication();
app.UseAuthorization();

app.UseExceptionHandler(options => { });
app
    .UseCatalogModule()
    .UseBasketModule()
    .UseOrderingModule();


app.Run();
