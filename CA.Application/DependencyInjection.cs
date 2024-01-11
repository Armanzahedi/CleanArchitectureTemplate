using System.Reflection;
using CA.Application.Common.Behaviors;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CA.Application.Common.Mapping;
using MediatR;


namespace CA.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services,IConfiguration configuration)
    {
        services.AddMappings();
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehavior<,>));
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(PerformanceBehavior<,>));
        });
        return services;
    }
}