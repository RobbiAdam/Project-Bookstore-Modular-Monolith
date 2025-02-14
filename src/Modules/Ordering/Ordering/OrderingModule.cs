﻿using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Configurations;
using Shared.Data;
using Shared.Data.Interceptors;

namespace Ordering;
public static class OrderingModule
{
    public static IServiceCollection AddOrderingModule(
        this IServiceCollection services, IConfiguration config)
    {
        services.AddDatabase(config);
        services.RegisterMapsterConfiguration();
        return services;
    }

    private static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration config)
    {
        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
        services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

        services.AddDbContext<OrderDbContext>((sp, options) =>
        {
            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
            options.UseNpgsql(config.GetConnectionString("Database"));
        });
        return services;
    }

    public static IApplicationBuilder UseOrderingModule(this IApplicationBuilder app)
    {
        app.UseMigration<OrderDbContext>();
        return app;
    }
}
