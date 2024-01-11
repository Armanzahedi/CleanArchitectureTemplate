using System.Reflection;
using CA.Infrastructure.Persistence.Audit;
using CA.Infrastructure.Persistence.Audit.Interceptors;
using MediatR;
using Microsoft.EntityFrameworkCore;
using CA.Domain.Project;
using CA.Domain.Project.Entities;
using CA.Domain.User;
using CA.Infrastructure.Common;
using CA.Infrastructure.Persistence.Intreceptors;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace CA.Infrastructure.Persistence;

public class AppDbContext(DbContextOptions<AppDbContext> options,
        IMediator mediator,
        AuditSaveChangesInterceptor auditSaveChangesInterceptor,
        SoftDeleteSaveChangeInterceptor softDeleteSaveChangeInterceptor)
    : IdentityDbContext<User>(options)
{
    public DbSet<Project> Projects => Set<Project>();
    public DbSet<TodoItem> TodoItems => Set<TodoItem>();
    public DbSet<AuditEntity> Audits => Set<AuditEntity>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        
        
        base.OnModelCreating(builder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(auditSaveChangesInterceptor);
        optionsBuilder.AddInterceptors(softDeleteSaveChangeInterceptor);
        base.OnConfiguring(optionsBuilder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await mediator.DispatchDomainEvents(this);
        return await base.SaveChangesAsync(cancellationToken);
    }
}