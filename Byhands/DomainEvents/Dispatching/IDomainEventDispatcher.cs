namespace RevAssure.DomainEvents.Dispatching;

public interface IDomainEventDispatcher
{
    Task DispatchEventsAsync(CancellationToken cancellationToken);
}