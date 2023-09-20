using System.Runtime.CompilerServices;
using Review.Domains;
using Review.Entities.Errors;
using Review.Models.Bases;

namespace Review.Entities.Validators;

public sealed class NumberValidator : ValueObject<decimal>
{
    public NumberValidator(decimal value) : base(value) { }

    public static Result<NumberValidator> Create(
        decimal value,
        decimal? minValue = 0,
        [CallerArgumentExpression(nameof(value))] string parameterName = default!)
    {
        if (value <= minValue)
            return new DomainValidationError(ErrorValidators.ValueIsInvalid(parameterName).Message);

        return new NumberValidator(value);
    }
}