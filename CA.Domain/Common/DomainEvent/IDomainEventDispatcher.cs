using CA.Domain.Common.Entity;

namespace CA.Domain.Common.DomainEvent;

public interface IDomainEventDispatcher
{
    Task DispatchAndClearEvents(IEnumerable<EntityBase> entitiesWithEvents);
}