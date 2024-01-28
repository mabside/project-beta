using System.Runtime.CompilerServices;
using Byhands.Domains;
using Byhands.Entities.Errors;
using Byhands.Models.Bases;

namespace Byhands.Entities.Validators;

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