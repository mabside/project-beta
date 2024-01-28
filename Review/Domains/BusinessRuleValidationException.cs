using Byhands.Abstractions;
using Byhands.Domain;

namespace Byhands.Domains;

public class BusinessRuleValidationException : Exception
{
    public IBusinessRule BrokenRule { get; }

    public string Details { get; }

    public BusinessRuleValidationException(IBusinessRule brokenRule)
        : base(brokenRule.Message)
    {
        BrokenRule = brokenRule;
        this.Details = brokenRule.Message;
    }

    public override string ToString()
    {
        return $"{BrokenRule.GetType().FullName}: {BrokenRule.Message}";
    }

    public BusinessRuleValidationException()
    {
    }

    public BusinessRuleValidationException(string message) : base(message)
    {
    }

    public BusinessRuleValidationException(string message, Exception innerException) : base(message, innerException)
    {
    }
}

public class BusinessRuleValidationException<T> : Exception where T : BaseEntity<Guid>
{
    public IBusinessRule<T> BrokenRule { get; }

    public string Details { get; }

    public BusinessRuleValidationException(IBusinessRule<T> brokenRule)
        : base(brokenRule.Message)
    {
        BrokenRule = brokenRule;
        this.Details = brokenRule.Message;
    }

    public override string ToString()
    {
        return $"{BrokenRule.GetType().FullName}: {BrokenRule.Message}";
    }

    public BusinessRuleValidationException()
    {
    }

    public BusinessRuleValidationException(string message) : base(message)
    {
    }

    public BusinessRuleValidationException(string message, Exception innerException) : base(message, innerException)
    {
    }
}