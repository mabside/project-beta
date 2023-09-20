using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Review.Domains;
using Review.Entities.Errors;
using Review.Models.Bases;

namespace Review.Entities.Validators;
public sealed class OptionalRequiredValidator : ValueObject<string>
{
    public OptionalRequiredValidator(string value) : base(value) { }

    public static Result<OptionalRequiredValidator> Create(
        string? value,
        Func<string, bool> predicate,
        [CallerArgumentExpression(nameof(value))] string parameterName = default!)
    {
        var result = new OptionalRequiredValidator(value ?? string.Empty);

        if (!predicate(value))
            return result;

        if (string.IsNullOrWhiteSpace(value))
            return new DomainValidationError(ErrorValidators.ValueIsRequired(parameterName).Message);

        return new OptionalRequiredValidator(value);
    }

    public static Result<OptionalRequiredValidator> Create(
        string? value,
        Func<string, bool> predicate,
        string regexPattern,
        [CallerArgumentExpression(nameof(value))] string parameterName = default!)
    {
        var result = new OptionalRequiredValidator(value ?? string.Empty);

        if (!predicate(value))
            return result;

        if (!Regex.IsMatch(value, regexPattern, RegexConstants.OPTIONS))
            return new DomainValidationError(ErrorValidators.ValueIsRequired(parameterName).Message);

        return result;
    }
}