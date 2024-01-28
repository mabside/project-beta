using System.Runtime.CompilerServices;
using Byhands.Domains;
using Byhands.Entities.Errors;
using Byhands.Models.Bases;

namespace Byhands.Entities.Validators;

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
