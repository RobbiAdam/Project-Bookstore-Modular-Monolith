using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Basket;
public static class DependencyInjection
{
    public static IServiceCollection AddBasketModule(this IServiceCollection services, IConfiguration config)
    {
        return services;
    }

    public static IApplicationBuilder UseBasketModule(this IApplicationBuilder app)
    {
        return app;
    }
}
