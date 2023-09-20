using System.Runtime.CompilerServices;
using Review.Domains;
using Review.Entities.Errors;
using Review.Models.Bases;

namespace Review.Entities.Validators;

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