using System.Runtime.CompilerServices;
using Review.Domains;
using Review.Entities.Errors;
using Review.Models.Bases;

namespace Review.Entities.Validators;

public sealed class EnumValidator<TEnum> : ValueObject<string> where TEnum : struct
{
    public EnumValidator(string value) : base(value)
    {
    }

    public static Result<EnumValidator<TEnum>> Create(string value,
        [CallerArgumentExpression(nameof(value))] string parameterName = default!)
    {
        if (string.IsNullOrWhiteSpace(value))
            return new DomainValidationError(ErrorValidators
                .ValueIsRequired(parameterName).Message);

        if (!Enum.TryParse<TEnum>(value, true, out _))
        {
            return new DomainValidationError(ErrorValidators
                .ValueIsNotValidEnum(parameterName, typeof(TEnum).Name).Message);
        }

        return new EnumValidator<TEnum>(value);
    }
}
