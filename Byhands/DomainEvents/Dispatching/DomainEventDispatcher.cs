using DotNetCore.CAP;

namespace RevAssure.DomainEvents.Dispatching;

public class DomainEventDispatcher : IDomainEventDispatcher
{
    private readonly ICapPublisher _capPublisher;
    private readonly IDomainEventAccessor _domainEventAccessor;

    public DomainEventDispatcher(IDomainEventAccessor domainEventAccessor, ICapPublisher capPublisher)
    {
        _domainEventAccessor = domainEventAccessor;
        _capPublisher = capPublisher;
    }

    public async Task DispatchEventsAsync(CancellationToken cancellationToken)
    {
        var domainEvents = _domainEventAccessor.GetAllDomainEvents();

        _domainEventAccessor.ClearAllDomainEvents();

        foreach (var domainEvent in domainEvents)
        {
            await _capPublisher.PublishAsync(domainEvent.EventType, domainEvent, cancellationToken: cancellationToken);
        }
    }
}