using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Shared.Messaging.Extensions;
public static class MassTransitExtension
{
    public static IServiceCollection AddMassTransitWithAssemblies(
        this IServiceCollection services,IConfiguration configuration, params Assembly[] assemblies)
    {
        return services.AddMassTransit(config =>
        {
            config.SetKebabCaseEndpointNameFormatter();

            config.SetInMemorySagaRepositoryProvider();

            config.AddConsumers(assemblies);
            config.AddSagaStateMachines(assemblies);
            config.AddSagas(assemblies);
            config.AddActivities(assemblies);

            config.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(new Uri(configuration["MessageBroker:Host"]!), host =>
                {
                    host.Username(configuration["MessageBroker:UserName"]!);
                    host.Password(configuration["MessageBroker:Password"]!);
                });
                cfg.ConfigureEndpoints(context);
            });

        });
    }
}
