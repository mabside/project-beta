using System.Runtime.CompilerServices;
using Review.Domains;
using Review.Entities.Errors;
using Review.Models.Bases;

namespace Review.Entities.Validators;

public sealed class OptionalField : ValueObject<string>
{
    public OptionalField(string value) : base(value) { }

    public static Result<OptionalField> Create(
        string? value,
        [CallerArgumentExpression(nameof(value))] string parameterName = default!)
    {
        if (value == null)
            return new OptionalField(string.Empty);

        if (string.IsNullOrWhiteSpace(value))
            return new DomainValidationError(ErrorValidators.ValueIsRequired(parameterName).Message);

        return new OptionalField(value);
    }

    public static Result<OptionalField> Create(
        string? value,
        string regexPattern,
        [CallerArgumentExpression(nameof(value))] string parameterName = default!)
    {
        if (value == null)
            return new OptionalField(string.Empty);

        if (string.IsNullOrWhiteSpace(value))
            return new DomainValidationError(ErrorValidators.ValueIsRequired(parameterName).Message);

        if (!Regex.IsMatch(value, regexPattern, RegexConstants.OPTIONS))
            return new DomainValidationError(ErrorValidators.ValueIsRequired(parameterName).Message);

        return new OptionalField(value);
    }
}