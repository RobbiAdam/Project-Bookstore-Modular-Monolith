using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Shared.Behaviours;
using System.Reflection;

namespace Shared.Extensions;
public static class MediatRExtensions
{
    public static IServiceCollection AddMediatRWithAssemblies(
        this IServiceCollection services, params Assembly[] assemblies)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssemblies(assemblies);
            config.AddOpenBehavior(typeof(ValidationBehavior<,>));
            config.AddOpenBehavior(typeof(LoggingBehavior<,>));
        })
            .AddValidatorsFromAssemblies(assemblies);
        
        return services;
    }
}
