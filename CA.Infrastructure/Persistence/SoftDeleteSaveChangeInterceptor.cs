using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using CA.Domain.Common.Interfaces;

namespace CA.Infrastructure.Persistence;

public class SoftDeleteSaveChangeInterceptor: SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        ApplySoftDeletePolicy(eventData.Context);
        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        ApplySoftDeletePolicy(eventData.Context);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
    public void ApplySoftDeletePolicy(DbContext? context)
    {
        if (context == null) return;
        
        IEnumerable<EntityEntry> entities =
            context.ChangeTracker.Entries()
                .Where(t => t is { Entity: ISoftDeletableEntity, State: EntityState.Deleted }).ToList();

        var entityEntries = entities as EntityEntry[] ?? entities.ToArray();
        if (entityEntries.Any())
        {
            foreach(EntityEntry entry in entityEntries)
            {
                ISoftDeletableEntity entity = (ISoftDeletableEntity)entry.Entity;
                entity.IsDeleted = true;
                entry.State = EntityState.Modified;
            }
        }
    }

}