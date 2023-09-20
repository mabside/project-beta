using System.Runtime.CompilerServices;
using Review.Domains;
using Review.Entities.Errors;
using Review.Models.Bases;

namespace Review.Entities.Validators;

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
