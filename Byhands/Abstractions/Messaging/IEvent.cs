namespace Byhands.Abstractions.Messaging;

public interface IEvent
{
    Guid Id { get; }
    DateTime OccuredOn { get; }
    string EventType { get; }
}
