namespace Byhands.Abstractions.Entities;

public interface IIdentifiableEntity<Tid>
{
    Tid Id { get; }
}
