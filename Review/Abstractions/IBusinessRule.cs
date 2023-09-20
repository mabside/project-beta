using Review.Domain;

namespace Review.Abstractions;

public interface IBusinessRule
{
    string Message { get; }

    bool IsBroken();
}

public interface IBusinessRule<T> where T : BaseEntity<Guid>
{
    bool IsBroken();
    string Message { get; }
}
