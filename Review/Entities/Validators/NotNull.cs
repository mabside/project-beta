using System.Runtime.CompilerServices;
using Review.Domains;
using Review.Entities.Errors;
using Review.Models.Bases;

namespace Review.Entities.Validators;

public sealed class NotNull : ValueObject<object>
{
    public NotNull(object value) : base(value) { }

    public static Result<NotNull> Create(
        object? value,
        [CallerArgumentExpression(nameof(value))] string parameterName = default!)
    {
        if (value is null)
            return new DomainValidationError(ErrorValidators.ValueIsRequired(parameterName).Message);

        return new NotNull(value);
    }
}
