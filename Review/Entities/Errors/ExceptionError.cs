using Review.Models.Bases;

namespace Review.Entities.Errors;

public record ExceptionError : Error
{
    public Exception Exception { get; }

    public ExceptionError(
        string Message,
        string ErrorCode,
        bool IsTransient,
        Error? Inner = null)
        : base(Message, ErrorCode, IsTransient, Inner)
    {
        //base.(Message, ErrorCode, IsTransient, Inner);
    }
}
