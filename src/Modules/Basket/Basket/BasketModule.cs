using Shared.Data;
using Shared.Data.Interceptors;

namespace Basket;
public static class BasketModule
{
    public static IServiceCollection AddBasketModule(this IServiceCollection services, IConfiguration config)
    {
        return services
            .AddScoped<IBasketRepository, BasketRepository>()
            .Decorate<IBasketRepository, CachedBasketRepository>()
            .AddDatabase(config);
    }

    private static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration config)
    {
        var connectionString = config.GetConnectionString("Database");

        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
        services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

        services.AddDbContext<BasketDbContext>((sp, options) =>
        {
            options.UseNpgsql(connectionString);
            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
        });

        return services;
    }

    public static IApplicationBuilder UseBasketModule(this IApplicationBuilder app)
    {
        app.UseMigration<BasketDbContext>();
        return app;
    }
}
