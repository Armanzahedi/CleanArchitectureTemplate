using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CA.Domain.Common.Interfaces;

namespace CA.Infrastructure.Persistence;

public abstract class BaseEntityTypeConfiguration<T> : IEntityTypeConfiguration<T> where T : class, ISoftDeletableEntity
{
    public virtual void Configure(EntityTypeBuilder<T> builder)
    {
        if (typeof(T).GetInterfaces().Any(i => i == typeof(ISoftDeletableEntity)))
        {
            builder.HasQueryFilter(t => t.IsDeleted == false);
        }
    }
}