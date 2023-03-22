using System.Reflection;
using CA.Infrastructure.Persistence.Audit;
using CA.Infrastructure.Persistence.Audit.Interceptors;
using MediatR;
using Microsoft.EntityFrameworkCore;
using CA.Domain.Project;
using CA.Domain.Project.Entities;
using CA.Infrastructure.Common;

namespace CA.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    private readonly IMediator _mediator;
    private readonly AuditSaveChangesInterceptor _auditSaveChangesInterceptor;
    private readonly SoftDeleteSaveChangeInterceptor _softDeleteSaveChangeInterceptor;

    public AppDbContext(
        DbContextOptions<AppDbContext> options,
        IMediator mediator,
        AuditSaveChangesInterceptor auditSaveChangesInterceptor,
        SoftDeleteSaveChangeInterceptor softDeleteSaveChangeInterceptor) 
        : base(options)
    {
        _mediator = mediator;
        _auditSaveChangesInterceptor = auditSaveChangesInterceptor;
        _softDeleteSaveChangeInterceptor = softDeleteSaveChangeInterceptor;
    }

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
        optionsBuilder.AddInterceptors(_auditSaveChangesInterceptor);
        optionsBuilder.AddInterceptors(_softDeleteSaveChangeInterceptor);
        base.OnConfiguring(optionsBuilder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _mediator.DispatchDomainEvents(this);
        return await base.SaveChangesAsync(cancellationToken);
    }
}