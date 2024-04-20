using Byhands.Domain;

namespace Byhands.Abstractions;

public interface IBusinessRule
{
    string Message { get; }

    string ErrorCode { get; }

    bool IsBroken();
}

public interface IBusinessRule<T> where T : BaseEntity<Guid>
{
    bool IsBroken();
    string Message { get; }
}
