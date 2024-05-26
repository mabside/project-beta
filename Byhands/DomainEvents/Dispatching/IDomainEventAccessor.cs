using Byhands.Abstractions.Messaging;

namespace RevAssure.DomainEvents.Dispatching;

public interface IDomainEventAccessor
{
    IReadOnlyCollection<IEvent> GetAllDomainEvents();
    void ClearAllDomainEvents();
}