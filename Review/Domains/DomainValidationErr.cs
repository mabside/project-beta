using Review.Abstractions;
using Review.Models.Bases;

namespace Review.Domains;

public class DomainValidationErr
{
    public IBusinessRule BrokenRule { get; }

    public string ErrorMessage { get; }

    public string ErrorMessageCode { get; }

    public DomainValidationErr(IBusinessRule BrokenRule)
    {
        this.BrokenRule = BrokenRule;
        ErrorMessage = BrokenRule.Message;
        ErrorMessageCode = BrokenRule.ErrorCode;
    }

    public static implicit operator Result(DomainValidationErr obj)
    {
        if (obj.BrokenRule.IsBroken())
            return new Error(obj.ErrorMessage, obj.ErrorMessageCode, false);

        return new Success();
    }
}
