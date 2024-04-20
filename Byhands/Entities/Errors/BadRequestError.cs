using Byhands.Models.Bases;

namespace Byhands.Entities.Errors;

public record BadRequestError : Error
{
    protected BadRequestError(Error original) : base(original)
    {
    }

    public BadRequestError(string Message)
            : base(Message, "400", IsTransient: false)
    {
    }
}
