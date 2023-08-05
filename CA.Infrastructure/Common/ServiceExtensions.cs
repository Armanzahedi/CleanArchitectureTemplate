using System.Reflection;
using CA.Application.Common.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace CA.Infrastructure.Common;

public static class ServiceExtensions
{
    public static void AddServicesFromAssembly(this IServiceCollection services, Assembly assembly)
    {
        var serviceTypes = assembly.GetTypes()
            .Where(type => type is { IsClass: true, IsAbstract: false })
            .Where(type => type.GetInterfaces().Any(IsServiceInterface))
            .ToList();

        foreach (var serviceType in serviceTypes)
        {
            var implementedInterfaces = serviceType.GetInterfaces();
            foreach (var implementedInterface in implementedInterfaces)
            {
                if (IsServiceInterface(implementedInterface) == false)
                    AddService(services, implementedInterface,serviceType, GetServiceLifetime(implementedInterface));
                else
                    AddService(services, serviceType, GetServiceLifetime(implementedInterface));

            }
        }
    }
    private static bool IsServiceInterface(Type interfaceType)
    {
        return interfaceType == typeof(ITransientService) ||
               interfaceType == typeof(IScopedService) ||
               interfaceType == typeof(ISingletonService);
    }
    private static ServiceLifetime GetServiceLifetime(Type implementedInterface)
    {
        if (implementedInterface.GetInterfaces().Contains(typeof(ITransientService)))
            return ServiceLifetime.Transient;

        if (implementedInterface.GetInterfaces().Contains(typeof(IScopedService)))
            return ServiceLifetime.Scoped;

        if (implementedInterface.GetInterfaces().Contains(typeof(ISingletonService)))
            return ServiceLifetime.Singleton;

        return ServiceLifetime.Transient;
    }

    private static IServiceCollection AddService(this IServiceCollection services, Type serviceType, Type implementationType, ServiceLifetime lifetime) =>
        lifetime switch
        {
            ServiceLifetime.Transient => services.AddTransient(serviceType, implementationType),
            ServiceLifetime.Scoped => services.AddScoped(serviceType, implementationType),
            ServiceLifetime.Singleton => services.AddSingleton(serviceType, implementationType),
            _ => throw new ArgumentException("Invalid lifeTime", nameof(lifetime))
        };
    private static IServiceCollection AddService(this IServiceCollection services, Type serviceType,ServiceLifetime lifetime) =>
        lifetime switch
        {
            ServiceLifetime.Transient => services.AddTransient(serviceType),
            ServiceLifetime.Scoped => services.AddScoped(serviceType),
            ServiceLifetime.Singleton => services.AddSingleton(serviceType),
            _ => throw new ArgumentException("Invalid lifeTime", nameof(lifetime))
        };
}