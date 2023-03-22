using System.ComponentModel.DataAnnotations.Schema;

namespace CA.Domain.Common;

public abstract class EntityBase: EntityBase<int>
{
}
public abstract class EntityBase <TId>: IEquatable<EntityBase<TId>>
{

    protected EntityBase() { }

    protected EntityBase(TId id)
    {
        Id = id;
    }

    public TId? Id { get;protected set; }

    private List<DomainEventBase> _domainEvents = new ();
    [NotMapped]
    public IEnumerable<DomainEventBase> DomainEvents => _domainEvents.AsReadOnly();

    protected void RegisterDomainEvent(DomainEventBase domainEvent) => _domainEvents.Add(domainEvent);
    
    public void RemoveDomainEvent(DomainEventBase domainEvent)
    {
        _domainEvents.Remove(domainEvent);
    }
    public void ClearDomainEvents() => _domainEvents.Clear();

    public override bool Equals(object? obj)
    {
        return Id != null && obj is EntityBase<TId> entity && Id.Equals(entity.Id);
    }

    public bool Equals(EntityBase<TId>? other)
    {
        return Equals((object?)other);
    }

    public static bool operator ==(EntityBase<TId>? left, EntityBase<TId>? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(EntityBase<TId> left, EntityBase<TId> right)
    {
        return !Equals(left, right);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}
