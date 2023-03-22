using System.Reflection;
using CA.Domain.Common.Attributes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CA.Infrastructure.Persistence.Audit;

public static class AuditExtensions
{
    internal static bool ShouldBeAudited(this EntityEntry entry)
    {
        return entry.State != EntityState.Detached && entry.State != EntityState.Unchanged &&
               entry.State is EntityState.Added or EntityState.Modified or EntityState.Deleted &&
               entry.Entity is not AuditEntity &&
               entry.IsAuditable();
    }

    public static bool HasChangedOwnedEntities(this EntityEntry entry) =>
        entry.References.Any(r =>
            r.TargetEntry != null &&
            r.TargetEntry.Metadata.IsOwned() &&
            (r.TargetEntry.State == EntityState.Added || r.TargetEntry.State == EntityState.Modified));

    internal static bool IsAuditable(this EntityEntry entityEntry)
    {
        var hasAuditAttribute =
            (AuditableAttribute?)Attribute.GetCustomAttribute(entityEntry.Entity.GetType(),
                typeof(AuditableAttribute)) != null;

        // return entityEntry
        //     .Entity
        //     .GetType()
        //     .IsSubclassOf(typeof(AuditableEntityBase)) || hasAuditAttribute;
        return hasAuditAttribute;
    }

    internal static bool IsAuditable(this PropertyEntry propertyEntry)
    {
        Type entityType = propertyEntry.EntityEntry.Entity.GetType();
        PropertyInfo? propertyInfo = entityType.GetProperty(propertyEntry.Metadata.Name);
        bool disableAuditAttribute =
            propertyInfo != null && Attribute.IsDefined(propertyInfo, typeof(NotAuditableAttribute));

        return IsAuditable(propertyEntry.EntityEntry) && !disableAuditAttribute;
    }
}