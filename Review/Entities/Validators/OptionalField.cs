using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Byhands.Domains;
using Byhands.Entities.Errors;
using Byhands.Models.Bases;

namespace Byhands.Entities.Validators;

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