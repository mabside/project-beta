using Byhands.Models.Bases;

namespace Byhands.Entities.Errors;

public record NullError : Error
{
    protected NullError(Error original) : base(original)
    {
    }

    public NullError(string Message)
            : base(Message, "999", IsTransient: false)
    {
    }
}
