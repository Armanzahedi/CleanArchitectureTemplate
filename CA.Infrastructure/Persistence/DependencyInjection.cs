using CA.Application.Common.Interfaces.Persistence;
using CA.Infrastructure.Persistence.Audit.Interceptors;
using CA.Infrastructure.Persistence.Intreceptors;
using CA.Infrastructure.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CA.Infrastructure.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services,IConfiguration configuration)
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
        
        services.AddScoped<AppDbContextInitializer>();
        
        return services;
    }
}