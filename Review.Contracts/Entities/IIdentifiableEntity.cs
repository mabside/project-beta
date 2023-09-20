namespace Review.Contracts.Entities;

public interface IIdentifiableEntity<Tid>
{
    Tid Id { get; }
}
