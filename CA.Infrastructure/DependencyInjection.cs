using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CA.Application.Common.Interfaces.Persistence;
using CA.Application.Common.Interfaces.Services;
using CA.Infrastructure.Common;
using CA.Infrastructure.Identity;
using CA.Infrastructure.Persistence;
using CA.Infrastructure.Persistence.Audit.Interceptors;
using CA.Infrastructure.Persistence.Intreceptors;
using CA.Infrastructure.Persistence.Repository;

namespace CA.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,IConfiguration configuration)
    {
        services.AddPersistence(configuration);
        services.AddIdentity(configuration);
        services.AddTransient<IDateTimeProvider, DateTimeProvider>();
        return services;
    }
}