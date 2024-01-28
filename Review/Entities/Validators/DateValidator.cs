using System.Runtime.CompilerServices;
using Byhands.Domains;
using Byhands.Entities.Errors;
using Byhands.Models.Bases;

namespace Byhands.Entities.Validators;

public sealed class DateValidator : ValueObject<DateTimeOffset>
{
    public DateValidator(DateTimeOffset value) : base(value) { }

    public static Result<DateValidator> Create(
        DateTimeOffset value,
        [CallerArgumentExpression(nameof(value))] string parameterName = default!)
    {
        if (value < DateTimeOffset.UtcNow.Date)
            return new DomainValidationError(ErrorValidators.ValueIsInvalid(parameterName).Message);

        return new DateValidator(value);
    }
}