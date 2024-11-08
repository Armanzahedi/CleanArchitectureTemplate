using System.Reflection;
using CA.Domain.Common.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CA.Infrastructure.Persistence.Audit;

public static class AuditExtensions
{
    private static readonly ConcurrentDictionary<Type, bool> AuditableTypeCache = new();
    private static readonly ConcurrentDictionary<(Type, string), bool> NotAuditablePropertyCache = new();

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
            r.TargetEntry.State is EntityState.Added or EntityState.Modified);

    private static bool IsAuditable(this EntityEntry entityEntry)
    {
        Type entityType = entityEntry.Entity.GetType();
        return AuditableTypeCache.GetOrAdd(entityType, type => Attribute.IsDefined(type, typeof(AuditableAttribute)));
    }

    internal static bool IsAuditable(this PropertyEntry propertyEntry)
    {
        var entityType = propertyEntry.EntityEntry.Entity.GetType();
        var propertyName = propertyEntry.Metadata.Name;

        var isPropertyNotAuditable = NotAuditablePropertyCache.GetOrAdd((entityType, propertyName), key =>
        {
            var propertyInfo = key.Item1.GetProperty(key.Item2);
            return propertyInfo != null && Attribute.IsDefined(propertyInfo, typeof(NotAuditableAttribute));
        });

        return IsAuditable(propertyEntry.EntityEntry) && !isPropertyNotAuditable;
    }
}