using Byhands.Models.Bases;

namespace Byhands.Entities.Errors;

public record UserNotFoundError : Error
{
    protected UserNotFoundError(Error original) : base(original)
    {
    }

    public UserNotFoundError(string Message = "User not found")
            : base(Message, "", IsTransient: false)
    {
    }
}
