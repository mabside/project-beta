using System.Runtime.CompilerServices;
using Byhands.Domains;
using Byhands.Entities.Errors;
using Byhands.Models.Bases;

namespace Byhands.Entities.Validators;

public sealed class AmountValidator : ValueObject<decimal>
{
    public AmountValidator(decimal value) : base(value) { }

    public static Result<AmountValidator> Create(
        decimal value,
        [CallerArgumentExpression(nameof(value))] string parameterName = default!)
    {
        if (value <= 0)
            return new DomainValidationError(ErrorValidators.ValueIsInvalid(parameterName).Message);

        return new AmountValidator(value);
    }
}
