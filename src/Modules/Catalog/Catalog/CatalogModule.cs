using Catalog.Data.Seed;
using Shared.Behaviours;
using Shared.Data;
using Shared.Data.Interceptors;

namespace Catalog;
public static class CatalogModule
{
    public static IServiceCollection AddCatalogModule(this IServiceCollection services, IConfiguration config)
    {
        return services
            .AddDatabase(config)
            .AddMediator();
    }

    private static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration config)
    {
        var connectionString = config.GetConnectionString("Database");

        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
        services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

        services.AddDbContext<CatalogDbContext>((sp, options) =>
          {
              options.UseNpgsql(connectionString);
              options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
          });

        services.AddScoped<IDataSeeder, CatalogDataSeed>();
        return services;
    }

    private static IServiceCollection AddMediator(this IServiceCollection services)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            config.AddOpenBehavior(typeof(ValidationBehavior<,>));
            config.AddOpenBehavior(typeof(LoggingBehavior<,>));
        })
            .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());


        return services;
    }

    public static IApplicationBuilder UseCatalogModule(this IApplicationBuilder app)
    {
        app.UseMigration<CatalogDbContext>();
        return app;
    }

}
