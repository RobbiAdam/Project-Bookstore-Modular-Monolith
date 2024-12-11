using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ordering;
public static class DependencyInjection
{
    public static IServiceCollection AddOrderingModule(this IServiceCollection services, IConfiguration config)
    {
        return services;
    }

    public static IApplicationBuilder UseOrderingModule(this IApplicationBuilder app)
    {
        return app;
    }
}
