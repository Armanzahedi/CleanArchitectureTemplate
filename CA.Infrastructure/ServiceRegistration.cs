using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CA.Application.Common.Interfaces.Persistence;
using CA.Application.Common.Interfaces.Services;
using CA.Infrastructure.Common;
using CA.Infrastructure.Identity;
using CA.Infrastructure.Persistence;
using CA.Infrastructure.Persistence.Audit.Interceptors;

namespace CA.Infrastructure;

public static class ServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,IConfiguration configuration)
    {
        
        services.AddScoped<AuditSaveChangesInterceptor>();
        services.AddScoped<SoftDeleteSaveChangeInterceptor>();
        
        if (configuration.GetValue<bool>("UseInMemoryDatabase"))
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseInMemoryDatabase("AppDb"));
        }
        else
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), builder => builder.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)));
        }
        
        services.AddScoped(typeof(IReadRepository<>), typeof(EfRepository<>));
        services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        
        services.AddTransient<IDateTimeProvider, DateTimeProvider>();
        
        services.AddScoped<AppDbContextInitializer>();
        
        return services;
    }
}