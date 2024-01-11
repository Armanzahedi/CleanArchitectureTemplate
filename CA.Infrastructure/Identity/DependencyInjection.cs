using CA.Application.Common.Interfaces.Services;
using CA.Domain.User;
using CA.Infrastructure.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CA.Infrastructure.Identity;

public static class DependencyInjection
{
    public static IServiceCollection AddIdentity(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddIdentityApiEndpoints<User>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
            })
            .AddEntityFrameworkStores<AppDbContext>();

        services.AddScoped<ICurrentUserService, CurrentUserService>();
        return services;
    }
    public static void AddIdentityEndpoints(this WebApplication app,string prefix)
    {
        app.MapGroup(prefix).MapIdentityApi<User>();
    }
}