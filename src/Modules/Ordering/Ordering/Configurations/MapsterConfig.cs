using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Ordering.Configurations;
public static class MapsterConfig
{
    public static void RegisterMapsterConfiguration(this IServiceCollection services)
    {
        TypeAdapterConfig<Order, OrderDto>.NewConfig()
            .Map(dest => dest.Payment.Cvv, src => src.Payment.CVV);

        TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly());
    }
}
